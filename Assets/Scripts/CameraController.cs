using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook rocketCamera;
    [SerializeField] float rotateFactor = 0.1f;
    bool isPressed;
    int direction;

    private void Start()
    {
        if (rocketCamera == null)
        {
            rocketCamera = FindObjectOfType<CinemachineFreeLook>();
        }
    }

    public void Update()
    {
        if (isPressed)
        {
            rocketCamera.m_XAxis.Value += direction * rotateFactor;
        }
    }

    public void OnPointerDown(int direction)
    {
        this.direction = direction;
        isPressed = true;
    }
    public void OnPointerUp()
    {
        isPressed = false;
    }
}
