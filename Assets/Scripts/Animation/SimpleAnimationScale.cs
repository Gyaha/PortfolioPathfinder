using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimationScale : SimpleAnimation
{
    [SerializeField] private Vector3 baseScale = new Vector3();
    [SerializeField] private Vector3 targetScale = new Vector3();

    public override void AnimationTrigger(float animationCurve)
    {
        transform.localScale = Vector3.Lerp(baseScale, targetScale, animationCurve);
    }
}
