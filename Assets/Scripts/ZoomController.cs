using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    [SerializeField] private Camera perspective = null;
    [SerializeField] private Camera orthographic = null;

    [SerializeField] private float Levels = 1;

    [SerializeField] private Vector2 perspectiveBar = new Vector2(0, 1);
    [SerializeField] private Vector2 orthographicBar = new Vector2(0, 1);

    private float currentLevel = 0.5F;

    private void Update()
    {
        currentLevel -= Input.mouseScrollDelta.y / Levels;

        currentLevel = Mathf.Clamp01(currentLevel);

        perspective.fieldOfView = Mathf.Lerp(perspectiveBar.x, perspectiveBar.y, currentLevel);
        orthographic.orthographicSize = Mathf.Lerp(orthographicBar.x, orthographicBar.y, currentLevel);
    }
}