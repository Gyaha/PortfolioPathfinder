using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector2 bounds = new Vector2(5, 5);
    [SerializeField] private float border = 0;

    private bool isometric = false;
    private Vector3 velocity;

    void Update()
    {
        Vector3 position = transform.localPosition;

        velocity.x = (Input.GetAxis("Horizontal") * speed) * Time.deltaTime;
        velocity.z = (Input.GetAxis("Vertical") * speed) * Time.deltaTime;

        if (isometric)
        {
            velocity *= 1.4F;

            position.x += velocity.x / 2;
            position.z -= velocity.x / 2;
            position.x += velocity.z / 2;
            position.z += velocity.z / 2;
        }
        else
        {
            position.x += velocity.x;
            position.z += velocity.z;
        }

        position.x = Mathf.Clamp(position.x, -border, bounds.x + border);
        position.z = Mathf.Clamp(position.z, -border, bounds.y + border);

        transform.localPosition = position;
    }

    public void SetIsometric(bool isometric)
    {
        this.isometric = isometric;
    }

    public void SetSize(int width, int height)
    {
        bounds.x = width;
        bounds.y = height;

        transform.localPosition = new Vector3(width / 2, 0, height / 2);
    }
}
