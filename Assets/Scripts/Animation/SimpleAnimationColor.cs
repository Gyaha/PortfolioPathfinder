using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimationColor : SimpleAnimation
{
    [SerializeField] private Color baseColor = Color.white;
    [SerializeField] private Color targetColor = Color.white;
    [SerializeField] MeshRenderer meshRenderer = null;

    private Material materialCopy = null;

    public override void AnimationInit()
    {
        materialCopy = new Material(meshRenderer.sharedMaterial);
        materialCopy.name = materialCopy.name + " (SimpleAnimation)";
        meshRenderer.sharedMaterial = materialCopy;
    }

    public override void AnimationTrigger(float animationCurve)
    {
        materialCopy.color = Color.Lerp(baseColor, targetColor, animationCurve);
    }

    private void OnDestroy()
    {
        Destroy(materialCopy);
    }
}
