using System;
using UnityEngine;

public abstract class StepAssemblyProcessBase : MonoBehaviour
{
    public abstract void Initialize(UIManager uiManager);
    public event Action OnStepComplete;

    // Protected method to invoke the event from derived classes
    protected void InvokeStepComplete()
    {
        OnStepComplete?.Invoke();
    }
}