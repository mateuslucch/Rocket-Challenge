using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteTrigger : MonoBehaviour
{
    [SerializeField] bool maxHeight;    
    [SerializeField] float dragValue = 20f;

    private void OnEnable()
    {
        gameObject.GetComponent<Rigidbody>().drag = dragValue;
    }
       
}
