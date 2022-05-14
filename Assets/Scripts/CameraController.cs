using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera rocketCamera;
    Camera mainCamera;
    

    private void Update() {
        transform.Translate(Vector3.right * Time.deltaTime);
    }
}
