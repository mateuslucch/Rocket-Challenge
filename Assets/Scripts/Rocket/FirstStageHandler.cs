using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStageHandler : MonoBehaviour
{

    Rigidbody firstStageRgBody;
    [SerializeField] float fuel = 5f;
    [SerializeField] bool isFalling = false;
    [SerializeField] GameObject nose;
    [SerializeField] ParticleSystem rocketFire;
    [SerializeField] AudioClip rocketSound;

    [Header("Thrust values")]
    [SerializeField] float thrustX = 0f;
    [SerializeField] float thrustY = 0f;
    [SerializeField] float thrustZ = 0f;

    bool jointDetached = false;
    AudioSource audioSource;

    void Start()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            Debug.Log("There is no Rigidbody!");
            return;
        }
        firstStageRgBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (fuel > 0)
        {
            //          
            firstStageRgBody.AddRelativeForce(new Vector3(thrustX / 10, thrustY / 10, thrustZ));
            fuel -= Time.deltaTime;
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(rocketSound);
            }
        }
        else if (fuel < 0)
        {
            // separate stages and other process
            if (jointDetached == false)
            {
                fuel = 0;
                audioSource.Stop();
                rocketFire.Stop();                
                nose.GetComponent<NoseHandler>().JointDetached();
                Destroy(firstStageRgBody.GetComponent<FixedJoint>());
                GetComponent<Rigidbody>().drag = 0.5f;
                jointDetached = true;
            }
        }
        if (firstStageRgBody.velocity.y < 0 && !isFalling)
        {
            isFalling = false;
        }

    }

    public void Launch(float fuel, Vector3 thrust)
    {
        rocketFire.Play();

        isFalling = false;
        // get data from rocket data        
        this.fuel = fuel;

        // Rocket models have different coordinates related to global coordinates!!        
        thrustX = thrust.x;
        thrustZ = thrust.y;
        thrustY = thrust.z * (-1);
    }

    public float Fuel()
    {
        return fuel;
    }
}
