using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStage : MonoBehaviour
{

    Rigidbody rocketBody;
    [SerializeField] float fuel = 5f;
    [SerializeField] float maxFuel = 5f;
    [SerializeField] bool isFalling = false;
    [SerializeField] GameObject nose;

    bool launch = false;
    [Header("Thrust values")]
    [SerializeField] float thrustX, thrustY, thrustZ;

    void Start()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            Debug.Log("There is no Rigidbody!");
            return;
        }
        rocketBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (fuel > 0)
        {
            // Só impulsiona na direção de y global
            //rocketBody.AddForce(Vector3.up * thrust);

            // Impulsiona na direção relativa
            rocketBody.AddRelativeForce(new Vector3(thrustX, thrustY, thrustZ));
            fuel -= Time.deltaTime;
        }
        else if (fuel < 0)
        {
            // separa estagios            
            Destroy(rocketBody.GetComponent<FixedJoint>());

            nose.GetComponent<NoseHandler>().JointDetached();
        }
        if (rocketBody.velocity.y < 0 && !isFalling)
        {

            isFalling = false;
        }

    }

    public void LaunchRocket()
    {
        fuel = maxFuel;
        isFalling = false;
        launch = true;
        // get data from ui
    }
}
