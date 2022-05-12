using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketThrust : MonoBehaviour
{
    Rigidbody rocketBody;
    [SerializeField] float thrust = 20f;
    //[SerializeField] float startThrust = 20f;
    [SerializeField] float fuel = 5f;
    [SerializeField] float maxFuel = 5f;
    [SerializeField] float height;
    [SerializeField] float maxHeight;
    [SerializeField] bool isFalling = false;
    [SerializeField] GameObject firstStage;
    [SerializeField] GameObject nose;
    [SerializeField] GameObject parachute;    
    bool launch = false;

    void Start()
    {
        firstStage.GetComponent<Rigidbody>().detectCollisions = false;
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
            rocketBody.AddRelativeForce(Vector3.up * thrust);
            fuel -= Time.deltaTime;
        }
        height = transform.position.y;

        if (fuel < 0)
        {
            firstStage.GetComponent<Transform>().parent = null;
            firstStage.GetComponent<Rigidbody>().detectCollisions = true;
            firstStage.GetComponent<Rigidbody>().useGravity = true;
            firstStage.GetComponent<Rigidbody>().isKinematic = false;

        }

        if (rocketBody.velocity.y < 0)
        {
            if (isFalling == false && fuel < 0 && launch)
            {
                maxHeight = height;
                isFalling = true;
                parachute.SetActive(true);
            }
        }
    }

    public void LaunchRocket()
    {
        GetComponent<BoxCollider>().enabled = false;
        fuel = maxFuel;
        //thrust = startThrust;
        isFalling = false;
        launch = true;
        
    }
}
