using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStageHandler : MonoBehaviour
{
    Rigidbody firstStageBody;
    [SerializeField] float dragForce = 5f;

    private void OnEnable()
    {
        firstStageBody = GetComponent<Rigidbody>();
        //gameObject.GetComponent<Rigidbody>().AddRelativeForce(gameObject.GetComponent<Rigidbody>().velocity);
    }
    private void Update() {
        if(firstStageBody.velocity.y < 0)
        {
            firstStageBody.drag = dragForce;
        }
    }
}
