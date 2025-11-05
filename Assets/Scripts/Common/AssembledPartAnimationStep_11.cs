using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
public class AssembledPartAnimationStep_11 : AssembledPartAnimationBase
{
    public List<GameObject> _parts;
    public List<GameObject> _targetPoints;
    private List<Vector3> _startPositions = new List<Vector3>();
    private List<Vector3> _startRotation = new List<Vector3>();
    private bool _isExploded = false;

    private void Start()
    {
        StartExplodeAnimation(null);
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

        //move parts array item to target items having 0.5 sec delay between each move 
        for (int i = 0; i < _parts.Count; i++)
        {
            var part = _parts[i];
            var targetPoint = _targetPoints[i];
            sequence.Append(part.transform.DOLocalMove(targetPoint.transform.localPosition, 1f).SetEase(Ease.InOutSine));
            sequence.AppendInterval(0.5f); // Delay between each move
        }
        //and move back to start position have 0.5 sec delay between each move
        for (int i = _parts.Count-1; i > -1; i--)
        {
            var part = _parts[i];
            var startPos = _startPositions[i];
            sequence.Append(part.transform.DOLocalMove(startPos, 1f).SetEase(Ease.InOutSine));
            sequence.AppendInterval(0.5f); // Delay between each move
        }


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