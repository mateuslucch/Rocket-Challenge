using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class RocketData : MonoBehaviour
{

    GameObject noseRocket;
    FirstStageHandler firstStage;
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] Vector3 rocketStartPosition;
    //[SerializeField] CinemachineVirtualCamera rocketCamera;
    [SerializeField] CinemachineFreeLook rocketCamera;
    [SerializeField] UiHandler uiHandler;
    [SerializeField] Transform launchPlatform;
    [SerializeField] WindHandler windHandler;

    [Header("Start Values")]
    [SerializeField] Vector3 firstStageStartThrust;
    [SerializeField] Vector3 noseStartThrust;
    [SerializeField] float noseFuelStart;
    [SerializeField] float firstStageFuelStart;

    Rigidbody noseRgBody;
    GameObject instantiatedRocket;
    float height, maxHeight = 0;
    Vector3 speed;
    bool launched = false;

    private void Start()
    {
        if (windHandler == null)
        {
            windHandler = FindObjectOfType<WindHandler>();
        }
        if (uiHandler == null)
        {
            uiHandler = FindObjectOfType<UiHandler>();
            uiHandler.StartInputValues(firstStageStartThrust, noseStartThrust, firstStageFuelStart, noseFuelStart);
        }
        if (instantiatedRocket == null)
        {            
            GetRocketBodie();
        }
        rocketStartPosition = new Vector3(
            instantiatedRocket.transform.position.x,
            instantiatedRocket.transform.position.y,
            instantiatedRocket.transform.position.z);

    }

    private void Update()
    {
        if (instantiatedRocket != null)
        {
            speed = noseRgBody.velocity;
            uiHandler.SpeedText(speed);

            height = noseRocket.transform.position.y - launchPlatform.position.y;
            uiHandler.HeightText(height);

            if (height > maxHeight)
            {
                maxHeight = height;
                uiHandler.MaxHeightText(height);
            }
        }
        // change fuel text to nose, or have one for first stage and other for nose
        uiHandler.FirstStageFuelText(firstStage.Fuel());
        uiHandler.NoseFuelText(noseRocket.GetComponent<NoseHandler>().Fuel());
    }   

    public void ResetData()
    {
        // reset data
        maxHeight = 0;
        launched = false;

        // destroy old        
        Destroy(FindObjectOfType<RocketParent>().gameObject);

        // create new
        Instantiate(rocketPrefab, rocketStartPosition, Quaternion.identity);        
        GetRocketBodie();

        rocketCamera.Follow = noseRocket.transform;

    }

    private void GetRocketBodie()
    {
        instantiatedRocket = FindObjectOfType<RocketParent>().gameObject;
        // get nose body and rigid body
        noseRocket = instantiatedRocket.GetComponentInChildren<NoseHandler>().gameObject;
        noseRgBody = noseRocket.GetComponent<Rigidbody>();
        // get first stage body
        firstStage = instantiatedRocket.GetComponentInChildren<FirstStageHandler>();
        // assing new rocket to wind
        windHandler.RecatchRocketBodies();
        FollowNose();
    }

    public void LaunchRocket()
    {
        if (launched == false)
        {
            // send input data to nose
            noseRocket.GetComponent<NoseHandler>().NoseData(uiHandler.NoseFuel(), uiHandler.NoseInputThrust());
            // send input data to 1st stage and launch            
            firstStage.Launch(uiHandler.FirstStageFuel(), uiHandler.FirstStageInputThrust());

            // "lock" launch button
            launched = true;
        }
    }

    public void FollowNose()
    {
        rocketCamera.Follow = noseRocket.transform;
        rocketCamera.LookAt = noseRocket.transform;        
    }

    public void FollowFirstStage()
    {
        rocketCamera.Follow = firstStage.transform;
        rocketCamera.LookAt = firstStage.transform;
    }
}
