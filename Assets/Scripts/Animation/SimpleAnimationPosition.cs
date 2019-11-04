using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimationPosition : SimpleAnimation
{
    [SerializeField] private Vector3 basePosition = new Vector3();
    [SerializeField] private Vector3 targetPosition = new Vector3();

    public override void AnimationTrigger(float animationCurve)
    {
        transform.localPosition = Vector3.Lerp(basePosition, targetPosition, animationCurve);
    }
}
