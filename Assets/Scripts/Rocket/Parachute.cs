using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    [SerializeField] float verticalDragValue = 2f;
    bool isOpen = false;
    bool isOnGround = false;
    Rigidbody parachuteRgBody;

    private void Start()
    {
        parachuteRgBody = GetComponent<Rigidbody>();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void FixedUpdate()
    {
        if (isOpen && !isOnGround)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(0, -parachuteRgBody.velocity.y * verticalDragValue, 0);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }

    public void OpenParachute()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        parachuteRgBody.mass = 0.5f;
        isOpen = true;
    }

}
