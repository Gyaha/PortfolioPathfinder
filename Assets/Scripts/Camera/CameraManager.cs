using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CameraController cameraController = null;
    [SerializeField] private MouseManager mouseManager = null;
    [SerializeField] private Camera perspectiveCamera = null;
    [SerializeField] private Camera isometricCamera = null;

    [SerializeField] private bool isometric = false;

    private void Start()
    {
        SetCameraMode(isometric);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetCameraMode(!isometric);
        }
    }

    public void SetCameraMode(bool isometric)
    {
        this.isometric = isometric;

        if (isometric)
        {
            perspectiveCamera.gameObject.SetActive(false);
            isometricCamera.gameObject.SetActive(true);
            mouseManager.SetCamera(isometricCamera);
            cameraController.SetIsometric(true);
        }
        else
        {
            perspectiveCamera.gameObject.SetActive(true);
            isometricCamera.gameObject.SetActive(false);
            mouseManager.SetCamera(perspectiveCamera);
            cameraController.SetIsometric(false);
        }
    }

    public void SetSize(int width, int height)
    {
        cameraController.SetSize(width, height);
    }

}