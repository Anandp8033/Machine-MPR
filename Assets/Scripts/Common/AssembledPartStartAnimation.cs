using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
public class AssembledPartStartAnimation : AssembledPartAnimationBase
{
    public List<GameObject> _parts;
    public List<GameObject> _targetPoints;
    private List<Vector3> _startPositions = new List<Vector3>();
    private List<Vector3> _startRotation = new List<Vector3>();
    private bool _isExploded = false;


    private void Start()
    {
        //only for testing remove this in final
        transform.GetComponent<AssembledPartStartAnimation>().StartExplodeAnimation(Oncomplete);
    }

    void Oncomplete()
    {
        Debug.Log("Explode Animation Completed");
    }

    public void StorStartPos()
    {
        foreach (var part in _parts)
        {
            _startPositions.Add(part.transform.position);
            _startRotation.Add(part.transform.eulerAngles);
        }
    }

    //using do tween to animate parts to move to target points 
    public override void StartExplodeAnimation(Action onComplete)
    {
        StorStartPos();
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < _parts.Count; i++)
        {
            var part = _parts[i];
            var targetPoint = _targetPoints[i];
            sequence.Join(part.transform.DOMove(targetPoint.transform.position, 1f).SetEase(Ease.InOutSine));
            //sequence.Join(part.transform.DORotateQuaternion(targetPoint.transform.rotation, 1f).SetEase(Ease.InOutSine));
        }
        sequence.OnComplete(() => {DOVirtual.DelayedCall(0.5f, () => CollapseAnimation(onComplete));});
    }

    //reverser animation to start position
    public override void CollapseAnimation(Action onComplete) 
    {
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < _parts.Count; i++)
        {
            var part = _parts[i];
            var startPos = _startPositions[i];
            var startRot = _startRotation[i];
            sequence.Join(part.transform.DOMove(startPos, 1f).SetEase(Ease.InOutSine));
            //sequence.Join(part.transform.DORotateQuaternion(Quaternion.Euler(startRot), 1f).SetEase(Ease.InOutSine));
        }
        sequence.OnComplete(() => {DOVirtual.DelayedCall(0.5f, () => { onComplete?.Invoke(); }); });
    }

    ////for custom open animation
    //public void ExploadeAnimation(Action onComplete)
    //{
    //    StorStartPos();
    //    Sequence sequence = DOTween.Sequence();
    //    for (int i = 0; i < _parts.Count; i++)
    //    {
    //        var part = _parts[i];
    //        var targetPoint = _targetPoints[i];
    //        sequence.Join(part.transform.DOMove(targetPoint.transform.position, 1f).SetEase(Ease.InOutSine));
    //        sequence.Join(part.transform.DORotateQuaternion(targetPoint.transform.rotation, 1f).SetEase(Ease.InOutSine));
    //    }
    //    _isExploded = true;
    //    sequence.OnComplete(() => { DOVirtual.DelayedCall(0.2f, () => { onComplete.Invoke(); }); });
    //}

    //public void CollapseAnimation(Action onComplete)
    //{
    //    if (!_isExploded) return;
    //    Sequence sequence = DOTween.Sequence();
    //    for (int i = 0; i < _parts.Count; i++)
    //    {
    //        var part = _parts[i];
    //        var startPos = _startPositions[i];
    //        var startRot = _startRotation[i];
    //        sequence.Join(part.transform.DOMove(startPos, 1f).SetEase(Ease.InOutSine));
    //        sequence.Join(part.transform.DORotateQuaternion(Quaternion.Euler(startRot), 1f).SetEase(Ease.InOutSine));
    //    }
    //    sequence.OnComplete(() => { DOVirtual.DelayedCall(0.2f, () => { onComplete?.Invoke(); }); });
    //}
}