using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RocketData : MonoBehaviour
{

    [SerializeField] GameObject noseRocket;
    [SerializeField] GameObject rocket;
    [SerializeField] Vector3 startThrust;
    [SerializeField] TextMeshProUGUI speedText, maxHeightText, heightText, fuelText;

    Rigidbody noseBody;
    float height, maxHeight = 0;
    Vector3 speed;

    private void Start()
    {
        if (noseRocket == null)
        {
            noseRocket = FindObjectOfType<NoseHandler>().gameObject;

        }
        noseBody = noseRocket.GetComponent<Rigidbody>();

    }

    private void Update()
    {
        // rodar condição se foi lançado
        speed = noseBody.velocity;
        speedText.text = $"Velocidade: x:{(int)speed.x},y:{(int)speed.y},z:{(int)speed.z}";

        height = noseRocket.transform.position.y;
        heightText.text = $"Altura: {(int)height}";
        if (height > maxHeight)
        {
            maxHeight = height;
            maxHeightText.text = $"Altura Máxima: {(int)height}";
        }
    }

    public void ResetData()
    {
        // reset data
        // delete rocket
        // instantiate new rocket (prefab)
    }


}
