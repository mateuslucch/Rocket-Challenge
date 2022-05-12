using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseHandler : MonoBehaviour
{

    Rigidbody noseObject;

    [SerializeField] float height;
    [SerializeField] float maxHeight;

    [SerializeField] Vector3 noseSpeed;
    [SerializeField] GameObject parachute;    
    bool jointDetached = false;

    // Start is called before the first frame update
    void Start()
    {
        noseObject = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        noseSpeed = noseObject.velocity;
        height = transform.position.y;

        if (noseObject.velocity.y < 0 && jointDetached)
        {
            // get max height       
            parachute.SetActive(true);
        }
    }

    public void JointDetached()
    {
        jointDetached = true;
        gameObject.GetComponent<Rigidbody>().mass = 1;
    }
}
