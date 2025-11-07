using UnityEngine;
using UnityEngine.UI;

public class R_MPR__CrossSectionStep_Process : StepAssemblyProcessBase
{
    public Material crossSectionTransparentMaterial;
    public MeshRenderer crossSectionMeshRenderer;
    private Material[] appliedMaterials;
    public Slider animationSlider;
    public Button nextButton;
    private UIManager _UIManager;

    void Start()
    {
        nextButton.onClick.AddListener(() =>
        {
            // Invoke the step complete event when the next button is clicked
            InvokeStepComplete();
        });

        appliedMaterials = new Material[crossSectionMeshRenderer.materials.Length];

        for (int i = 0; i < appliedMaterials.Length; i++)
        {
            appliedMaterials[i] = new Material(crossSectionTransparentMaterial);
        }

        crossSectionMeshRenderer.materials = appliedMaterials;

        animationSlider.interactable = true;
    }


    public override void Initialize(UIManager uiManager)
    {
        _UIManager = uiManager;
        _UIManager.step_num_text.text = "<size=10>STEP:8</size>";
        _UIManager.step_Heading_Text.text = "Move the slider to see Cross-Section part.";    
        _UIManager.step_SubHeading_Text.text = "";
        animationSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    /// <summary>
    /// Method 2: Update material properties based on slider value
    /// </summary>
    private void OnSliderValueChanged(float value)
    {
        UpdateMaterialProperties(value);
    }

    private void UpdateMaterialProperties(float value)
    {

        foreach (var mat in appliedMaterials)
        {
            mat.shader = Shader.Find("Standard");

            Color color = mat.GetColor("_Color");
            color.a = Mathf.Lerp(1f, 0f, value);
            mat.SetColor("_Color", color);

            mat.SetFloat("_Metallic", Mathf.Lerp(1f, 0f, value));
            mat.SetFloat("_Glossiness", Mathf.Lerp(0.6f, 0f, value));

            if (value > 0)
                SetMaterialRenderingMode(mat, RenderingMode.Transparent);
            else
                SetMaterialRenderingMode(mat, RenderingMode.Opaque);
        }
    }

    // Helper enum and method for rendering mode
    public enum RenderingMode { Opaque, Transparent }


    //private void SetMaterialRenderingMode(Material material, RenderingMode mode)
    //{
    //    switch (mode)
    //    {
    //        case RenderingMode.Opaque:
    //            material.SetFloat("_Mode", 0); // Force Standard Shader mode
    //            material.SetOverrideTag("RenderType", "Opaque");
    //            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
    //            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
    //            material.SetInt("_ZWrite", 1);
    //            material.DisableKeyword("_ALPHATEST_ON");
    //            material.DisableKeyword("_ALPHABLEND_ON");
    //            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //            material.renderQueue = -1;
    //            break;

    //        case RenderingMode.Transparent:
    //            material.SetFloat("_Mode", 3);
    //            material.SetOverrideTag("RenderType", "Transparent");
    //            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
    //            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    //            material.SetInt("_ZWrite", 0);
    //            material.DisableKeyword("_ALPHATEST_ON");
    //            material.EnableKeyword("_ALPHABLEND_ON");
    //            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //            material.renderQueue = 3000;
    //            break;
    //    }
    //}


    private void SetMaterialRenderingMode(Material material, RenderingMode mode)
    {
        switch (mode)
        {
            case RenderingMode.Opaque:
                //change material to opaque
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetFloat("_Mode", 0);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHABLEND_ON");
                material.renderQueue = -1;
                break;

            case RenderingMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetFloat("_Mode", 3);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.EnableKeyword("_ALPHABLEND_ON");
                material.renderQueue = 3000;
                break;
        }
    }
}