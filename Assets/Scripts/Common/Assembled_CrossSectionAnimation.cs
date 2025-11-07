using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Assembled_CrossSectionAnimation : AssembledPartAnimationBase
{
    public Material crossSectionTransparentMaterial;
    public MeshRenderer crossSectionMeshRenderer;
    private Material[] appliedMaterials;
    public Slider animationSlider;

    public float animationDuration = 2f; // Time in seconds for full animation

    private Coroutine animationCoroutine;

    void Start()
    {

        appliedMaterials = new Material[crossSectionMeshRenderer.materials.Length];

        for (int i = 0; i < appliedMaterials.Length; i++)
        {
            appliedMaterials[i] = new Material(crossSectionTransparentMaterial);
        }

        crossSectionMeshRenderer.materials = appliedMaterials;


        animationSlider.interactable = false; // Disable manual interaction
        animationSlider.onValueChanged.AddListener(OnSliderValueChanged);       
    }

    public override void StartExplodeAnimation(Action onComplete)
    {
        AnimateSlider(onComplete);
    }

    public override void CollapseAnimation(Action onComplete)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Method 1: Animate slider from 0 to 1 over time
    /// </summary>
    public void AnimateSlider(Action onComplete)
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(AnimateSliderCoroutine(onComplete));
    }

    private IEnumerator AnimateSliderCoroutine(Action onComplete)
    {
        yield return new WaitForSeconds(0.5f); // Optional delay before starting
        float elapsed = 0f;
        float startValue = animationSlider.value;
        float endValue = 1f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            animationSlider.value = Mathf.Lerp(startValue, endValue, t);
            yield return null;
        }

        animationSlider.value = endValue;
        yield return new WaitForSeconds(2f); // Optional delay after finishing
        onComplete?.Invoke();
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

    private void SetMaterialRenderingMode(Material material, RenderingMode mode)
    {
        switch (mode)
        {
            case RenderingMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHABLEND_ON");
                material.renderQueue = -1;
                break;

            case RenderingMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.EnableKeyword("_ALPHABLEND_ON");
                material.renderQueue = 3000;
                break;
        }
    }
   
}