using UnityEngine;
using UnityEditor;

public class CreateBulletPrefab : MonoBehaviour
{
    [MenuItem("Tools/Create Bullet Prefab")]
    public static void CreateBulletPrefabAsset()
    {
        // 씬에서 Bullet 오브젝트 찾기
        GameObject bulletObject = GameObject.Find("Bullet");
        
        if (bulletObject != null)
        {
            // Prefabs 폴더가 없으면 생성
            if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            }
            
            // 프리팹 생성
            string prefabPath = "Assets/Prefabs/Bullet.prefab";
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(bulletObject, prefabPath);
            
            if (prefab != null)
            {
                Debug.Log("Bullet prefab created successfully at: " + prefabPath);
                
                // 씬에서 오브젝트 삭제 (프리팹만 남김)
                DestroyImmediate(bulletObject);
            }
            else
            {
                Debug.LogError("Failed to create Bullet prefab");
            }
        }
        else
        {
            Debug.LogError("Bullet GameObject not found in scene");
        }
    }
}