using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager = null;
    [SerializeField] private UnityEngine.EventSystems.EventSystem eventSystem = null;
    [SerializeField] private Camera _camera = null;
    [SerializeField] private LayerMask layers = default;
    [SerializeField] private float distance = 100;
    [SerializeField] private MouseSelection mouseSelection = MouseSelection.Wall;

    private Ray targetRay = default;
    private RaycastHit targetHit = default;

    private CellController target = null;
    private CellController targetLast = null;

    private CellController targetOrigen = null;

    void Update()
    {
        if (!eventSystem.IsPointerOverGameObject())
        {
            UpdateTarget();
            UpdateTargetLast();

            if (Input.GetMouseButtonDown(0))
            {
                if (target)
                {
                    switch (mouseSelection)
                    {
                        case MouseSelection.Wall:
                            gridManager.SetWall(target.x, target.y, !target.wall);
                            break;
                        case MouseSelection.Origen:
                            gridManager.SetOrigen(target.x, target.y);
                            break;
                        case MouseSelection.Target:
                            gridManager.SetTarget(target.x, target.y);
                            break;
                        default:
                            Debug.LogError("flim flam");
                            break;
                    }
                }

                targetOrigen = target;
            }

            if (Input.GetMouseButton(0))
            {
                if (target && targetOrigen)
                {
                    switch (mouseSelection)
                    {
                        case MouseSelection.Wall:
                            gridManager.SetWall(target.x, target.y, targetOrigen.wall);
                            break;
                        case MouseSelection.Origen:
                            gridManager.SetOrigen(target.x, target.y);
                            break;
                        case MouseSelection.Target:
                            gridManager.SetTarget(target.x, target.y);
                            break;
                        default:
                            Debug.LogError("flim flam");
                            break;
                    }
                }
            }
        }
        else
        {
            target = null;
            UpdateTargetLast();
        }
    }

    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }

    public void SetSelection(MouseSelection selection)
    {
        mouseSelection = selection;
    }

    private void UpdateTarget()
    {
        targetRay = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(targetRay, out targetHit, distance, layers))
        {
            target = targetHit.transform.GetComponent<CellController>();
        }
        else
        {
            target = null;
        }
    }

    private void UpdateTargetLast()
    {
        if (target != targetLast)
        {
            if (targetLast)
            {
                gridManager.OnHover(targetLast.x, targetLast.y, false);
            }
            if (target)
            {
                gridManager.OnHover(target.x, target.y, true);
            }
        }

        targetLast = target;
    }
}

public enum MouseSelection
{
    Wall,
    Origen,
    Target
}