using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseHandler : MonoBehaviour
{
    [SerializeField] float noseFuel;

    [SerializeField] GameObject parachute;
    [SerializeField] ParticleSystem rocketFire;

    [SerializeField] AudioClip rocketSound;

    [Header("Thrust values")]
    [SerializeField] float thrustX = 0f;
    [SerializeField] float thrustY = 0f;
    [SerializeField] float thrustZ = 0f;

    bool jointDetached = false;    
    bool noseFly = false;
    Rigidbody noseRgBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        noseRgBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jointDetached)
        {
            if (noseFuel > 0 && noseFly)
            {
                noseRgBody.AddRelativeForce(new Vector3(thrustX / 10, thrustY / 10, thrustZ));
                noseFuel -= Time.deltaTime;
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(rocketSound);
                }
            }
            else if (noseFuel < 0 && noseFly)
            {
                rocketFire.Stop();
                audioSource.Stop();
            }

        }

        if (noseRgBody.velocity.y < 0 && jointDetached && noseFuel < 0)
        {            
            parachute.GetComponent<Parachute>().OpenParachute();
        }
    }

    public void JointDetached()
    {
        jointDetached = true;        
        // start fly by himself after short time
        StartCoroutine(StartSecondStageThrust());
    }

    private IEnumerator StartSecondStageThrust()
    {
        yield return new WaitForSeconds(1.5f);
        RocketThrust();
    }

    public void RocketThrust()
    {
        rocketFire.Play();        
        noseFly = true;
    }

    public void NoseData(float fuel, Vector3 thrust)
    {
        this.noseFuel = fuel;
        thrustX = thrust.x;
        thrustZ = thrust.y;
        thrustY = thrust.z * (-1);
    }

    public float Fuel()
    {
        return noseFuel;
    }
}
