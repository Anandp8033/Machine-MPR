using UnityEngine;

[CreateAssetMenu(fileName = "MaterialsSO", menuName = "RotatorScriptableObjects/MaterialsSO", order = 1)]
public class MaterialsSO : ScriptableObject
{
    public Material originalMaterial;
    public Material transparentHighlightMaterial;

}
