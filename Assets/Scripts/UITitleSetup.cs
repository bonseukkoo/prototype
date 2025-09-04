using UnityEngine;
using TMPro;
using UnityEditor;

public class UITitleSetup : MonoBehaviour
{
    [MenuItem("Tools/Setup Title UI")]
    public static void SetupTitleUI()
    {
        // Canvas 찾기
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas not found!");
            return;
        }
        
        // TitleText 찾기
        TextMeshProUGUI titleText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        if (titleText == null)
        {
            Debug.LogError("TitleText not found!");
            return;
        }
        
        // RectTransform 설정
        RectTransform rectTransform = titleText.GetComponent<RectTransform>();
        
        // 앵커를 상단 중앙으로 설정
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.anchoredPosition = new Vector2(0, -50);
        
        // 텍스트 설정
        titleText.text = "AURA SHIFT";
        titleText.fontSize = 72;
        titleText.alignment = TextAlignmentOptions.Center;
        titleText.color = Color.white;
        
        // 폰트 스타일 설정
        titleText.fontStyle = FontStyles.Bold;
        
        Debug.Log("Title UI setup completed!");
    }
}