
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public struct SpawnPoint
{
    public Vector3 localPosition;
    public Vector3 localRotation;
}

[CreateAssetMenu(fileName = "SpawnPointData", menuName = "RotatorScriptableObjects/SpawnPointData", order = 1)]
public class SpawnPointData : ScriptableObject
{
    public List<SpawnPoint> localTransform;


    public void SetFromTransform(List<Transform> t)
    {
       for (int i = 0; i < t.Count; i++)
        {
            SpawnPoint sp = new SpawnPoint();
            sp.localPosition = t[i].localPosition;
            sp.localRotation = t[i].localEulerAngles;
            localTransform.Add(sp);
        }
    }

}
