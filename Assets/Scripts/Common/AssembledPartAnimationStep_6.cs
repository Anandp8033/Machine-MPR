using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
public class AssembledPartAnimationStep_6 : AssembledPartAnimationBase
{
    public List<GameObject> _parts;
    public List<GameObject> _targetPoints;
    private List<Vector3> _startPositions = new List<Vector3>();
    private List<Vector3> _startRotation = new List<Vector3>();
    private bool _isExploded = false;

    private void Start()
    {
        //StartExplodeAnimation(null);
    }
    public void StorStartPos()
    {
        foreach (var part in _parts)
        {
            _startPositions.Add(part.transform.localPosition);
            _startRotation.Add(part.transform.eulerAngles);
        }
    }

    //using do tween tranfrom obj rotate to -30 in Y axis, then then move _parts 0 to 3 index to target points of 0 to 3 index
    public override void StartExplodeAnimation(Action onComplete)
    {


        StorStartPos();
        Sequence sequence = DOTween.Sequence();

        // Step 1: Rotate to -30 Y
        sequence.Append(transform.DORotate(new Vector3(0, -30, 0), 1f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(1f); // Delay after rotation

        // Step 2: Move parts 0-3 to target (localPosition)
        for (int i = 0; i < 4; i++)
        {
            var part = _parts[i];
            var targetPoint = _targetPoints[i];
            sequence.Join(part.transform.DOLocalMove(targetPoint.transform.localPosition, 1f).SetEase(Ease.InOutSine));
        }
        sequence.AppendInterval(1.5f); // Delay after movement

        // Step 3: Move parts 0-3 back and reset rotation
        for (int i = 0; i < 4; i++)
        {
            var part = _parts[i];
            var startPos = _startPositions[i];
            sequence.Join(part.transform.DOLocalMove(startPos, 1f).SetEase(Ease.InOutSine));
        }
        sequence.AppendInterval(1f); // Delay after movement
        sequence.Join(transform.DORotate(Vector3.zero, 1f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(1f); // Delay after movement

        // Step 4: Rotate to -90 X, -30 Y
        sequence.Append(transform.DORotate(new Vector3(-90, -30, 0), 1f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(1f); // Delay after rotation

        // Step 5: Move parts 4-6 to target (localPosition)
        for (int i = 4; i < 7; i++)
        {
            var part = _parts[i];
            var targetPoint = _targetPoints[i];
            sequence.Join(part.transform.DOLocalMove(targetPoint.transform.localPosition, 1f).SetEase(Ease.InOutSine));
        }
        sequence.AppendInterval(1f); // Delay after movement

        // Step 6: Move parts 4-6 back and reset rotation
        for (int i = 4; i < 7; i++)
        {
            var part = _parts[i];
            var startPos = _startPositions[i];
            sequence.Join(part.transform.DOLocalMove(startPos, 1f).SetEase(Ease.InOutSine));
        }
        sequence.AppendInterval(1f); // Delay after movement
        sequence.Join(transform.DORotate(Vector3.zero, 1f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(1f); // Delay after movement

        // Step 7: Rotate to 30 Y
        sequence.Append(transform.DORotate(new Vector3(0, 30, 0), 1f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(1f); // Delay after rotation

        // Step 8: Move last part to target (localPosition)
        sequence.Join(_parts[^1].transform.DOLocalMove(_targetPoints[^1].transform.localPosition, 1f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(1f); // Delay after movement

        // Step 9: Move last part back and reset rotation
        sequence.Join(_parts[^1].transform.DOLocalMove(_startPositions[^1], 1f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(1f); // Delay after movement
        sequence.Join(transform.DORotate(Vector3.zero, 1f).SetEase(Ease.InOutSine));

        // Final callback
        sequence.OnComplete(() => {
            DOVirtual.DelayedCall(0.5f, () => onComplete?.Invoke());
        });
    }

    public override void CollapseAnimation(Action onComplete)
    {
        Debug.Log("completedddd  "+onComplete);
    }
}