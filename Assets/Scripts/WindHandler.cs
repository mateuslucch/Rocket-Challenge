using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindHandler : MonoBehaviour
{
    [Range(-1.0f, 1.0f)]
    [SerializeField] float windPushX, windPushY;

    Rigidbody rocketNose;
    Rigidbody rocketFirstStage;

    private void Start()
    {
        RecatchRocketBodies();
    }

    private void FixedUpdate()
    {
        // wind is in x and z!
        if (rocketFirstStage != null && rocketNose != null)
        {
            rocketNose.AddForce(windPushX / 10, 0, windPushY / 10);
            rocketFirstStage.AddForce(windPushX / 10, 0, windPushY / 10);
        }
    }

    public void RecatchRocketBodies()
    {
        rocketNose = FindObjectOfType<NoseHandler>().GetComponent<Rigidbody>();
        rocketFirstStage = FindObjectOfType<NoseHandler>().GetComponent<Rigidbody>();
    }
}
