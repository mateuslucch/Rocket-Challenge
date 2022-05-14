using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{

    [SerializeField] float dragValue = 20f;

    private void OnEnable()
    {
        gameObject.GetComponent<Rigidbody>().drag = dragValue;
    }

}
