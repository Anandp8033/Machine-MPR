
using System;
using UnityEngine;

public abstract class AssembledPartAnimationBase : MonoBehaviour
{
    public abstract void StartExplodeAnimation(Action onComplete);
    public abstract void CollapseAnimation(Action onComplete);
}
