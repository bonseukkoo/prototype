using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 4개 버텍스에 각각 다른 색상을 적용하는 그라디언트 Image 컴포넌트
/// </summary>
[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class VertexGradientImage : MonoBehaviour
{
    [Header("Gradient Settings")]
    [SerializeField] private bool useGradient = true;
    
    [Header("Vertex Colors")]
    [SerializeField] private Color topLeftColor = Color.white;
    [SerializeField] private Color topRightColor = Color.white;
    [SerializeField] private Color bottomLeftColor = Color.white;
    [SerializeField] private Color bottomRightColor = Color.white;
    
    private Image image;
    private Material gradientMaterial;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        CreateGradientMaterial();
    }
    
    private void Start()
    {
        UpdateGradient();
    }
    
    private void OnEnable()
    {
        if (image == null)
            image = GetComponent<Image>();
        UpdateGradient();
    }

    private void Update()
    {
        // Material이 수동으로 변경되었을 때 자동으로 복원
        if (image != null && useGradient && gradientMaterial != null)
        {
            if (image.material != gradientMaterial)
            {
                image.material = gradientMaterial;
            }
        }
        
        // Image 컴포넌트의 Material을 강제로 보호
        if (image != null && useGradient)
        {
            // Material이 null이거나 잘못된 Material이면 복원
            if (image.material == null || (gradientMaterial != null && image.material != gradientMaterial))
            {
                if (gradientMaterial == null)
                    CreateGradientMaterial();
                
                if (gradientMaterial != null)
                {
                    image.material = gradientMaterial;
                    UpdateGradient();
                }
            }
        }
    }

    
    private void CreateGradientMaterial()
    {
        // UI/VertexColor 셰이더를 사용하여 Material 생성
        Shader vertexShader = Shader.Find("UI/VertexColor");
        if (vertexShader != null)
        {
            gradientMaterial = new Material(vertexShader);
            gradientMaterial.name = "VertexGradient_Material_" + GetInstanceID();
            
            // Material의 기본 속성 설정
            gradientMaterial.SetColor("_Color", Color.white);
            gradientMaterial.SetColor("_ColorTopLeft", Color.white);
            gradientMaterial.SetColor("_ColorTopRight", Color.white);
            gradientMaterial.SetColor("_ColorBottomLeft", Color.white);
            gradientMaterial.SetColor("_ColorBottomRight", Color.white);
        }
        else
        {
            Debug.LogError("UI/VertexColor shader not found! Please make sure the shader is compiled.");
        }
    }
    
    private void UpdateGradient()
    {
        if (image == null) return;
        
        if (useGradient && gradientMaterial != null)
        {
            // 그라디언트 Material을 Image에 자동 적용 (읽기 전용으로 설정)
            image.material = gradientMaterial;
            
            // 4개 모서리 색상을 Material에 설정
            gradientMaterial.SetColor("_ColorTopLeft", topLeftColor);
            gradientMaterial.SetColor("_ColorTopRight", topRightColor);
            gradientMaterial.SetColor("_ColorBottomLeft", bottomLeftColor);
            gradientMaterial.SetColor("_ColorBottomRight", bottomRightColor);
            
            // Image의 기본 색상을 Material의 Tint로 설정
            gradientMaterial.SetColor("_Color", image.color);
            
            // Material을 읽기 전용으로 설정하여 수동 변경 방지
            gradientMaterial.hideFlags = HideFlags.NotEditable;
        }
        else
        {
            // 일반 Image 모드 - 기본 Material로 복원
            image.material = null;
        }
    }
    
    public void SetUseGradient(bool use)
    {
        useGradient = use;
        UpdateGradient();
    }
    
    public void SetVertexColors(Color topLeft, Color topRight, Color bottomLeft, Color bottomRight)
    {
        topLeftColor = topLeft;
        topRightColor = topRight;
        bottomLeftColor = bottomLeft;
        bottomRightColor = bottomRight;
        UpdateGradient();
    }
    

    

    
    private void OnDestroy()
    {
        if (gradientMaterial != null)
        {
            if (Application.isPlaying)
                Destroy(gradientMaterial);
            else
                DestroyImmediate(gradientMaterial);
        }
    }
    
    #if UNITY_EDITOR

    #endif



    

    

    
    /// <summary>
    /// Image의 기본 색상이 변경되었을 때 호출
    /// </summary>



    #if UNITY_EDITOR

    #endif


    #if UNITY_EDITOR
    private void OnValidate()
    {
        // 에디터에서 값이 변경될 때마다 즉시 업데이트
        if (image == null)
            image = GetComponent<Image>();
            
        if (gradientMaterial == null)
            CreateGradientMaterial();
            
        UpdateGradient();
        
        // Material이 변경되었을 때 자동으로 복원
        if (image != null && useGradient && gradientMaterial != null)
        {
            if (image.material != gradientMaterial)
            {
                image.material = gradientMaterial;
            }
        }
        
        // Image 컴포넌트의 Material을 강제로 보호
        if (image != null && useGradient)
        {
            if (image.material == null || (gradientMaterial != null && image.material != gradientMaterial))
            {
                if (gradientMaterial == null)
                    CreateGradientMaterial();
                
                if (gradientMaterial != null)
                {
                    image.material = gradientMaterial;
                    UpdateGradient();
                }
            }
        }
    }
    #endif






}