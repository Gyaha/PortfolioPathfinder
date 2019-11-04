using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimation : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve animationCurve = new AnimationCurve();

    [SerializeField]
    private float animationTime = 1;

    private bool animationForward = true;
    private bool animationEnd = true;
    private float animationTimer = 0;

    public void Trigger(bool forward)
    {
        animationForward = forward;
        animationEnd = false;
        enabled = true;
    }

    public virtual void AnimationInit() { }

    public virtual void AnimationTrigger(float animationCurve) { }

    private void Awake()
    {
        AnimationInit();
        AnimationTrigger(0);
        enabled = false;
    }

    private void Update()
    {
        if (animationEnd)
        {
            enabled = false;
            return;
        }

        if (animationForward)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= animationTime)
            {
                animationTimer = animationTime;
                animationEnd = true;
            }
        }
        else
        {
            animationTimer -= Time.deltaTime;
            if (animationTimer <= 0)
            {
                animationTimer = 0;
                animationEnd = true;
            }
        }

        AnimationTrigger(animationCurve.Evaluate(animationTimer / animationTime));
    }
}
