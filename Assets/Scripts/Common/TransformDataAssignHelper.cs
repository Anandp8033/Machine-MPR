using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TransformDataAssignHelper : MonoBehaviour
{
    public SpawnPointData data;
    public List<Transform> transforms;

    [ContextMenu("Set Data From Transforms")]
    private void SetDataFromTransforms()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && data != null)
        {
            Undo.RecordObject(data, "Set SpawnPointData From Transforms");
            data.SetFromTransform(transforms);
            EditorUtility.SetDirty(data);
        }
#endif
    }

}
