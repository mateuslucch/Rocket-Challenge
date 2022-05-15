using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    [Header("Updated Text Box")]
    [SerializeField] TextMeshProUGUI speedText, maxHeightText, heightText, firstStageFuelText, noseFuelText;

    [Header("Data Rocket Input Box")]
    [SerializeField] TMP_InputField firstStageFuelInput;
    [SerializeField] TMP_InputField noseFuelInput;

    [SerializeField] TMP_InputField firstStageXThrustInput;
    [SerializeField] TMP_InputField firstStageYThrustInput;
    [SerializeField] TMP_InputField firstStageZThrustInput;

    [SerializeField] TMP_InputField noseXThrustInput;
    [SerializeField] TMP_InputField noseYThrustInput;
    [SerializeField] TMP_InputField noseZThrustInput;

    [Header("Rocket Controller Object")]
    [SerializeField] RocketData rocketData;

    [Header("Wind Handler")]
    [SerializeField] WindHandler windHandler;
    [SerializeField] TMP_InputField windXDirection;
    [SerializeField] TMP_InputField windZDirection;

    private void Start()
    {
        if (rocketData == null) { rocketData = FindObjectOfType<RocketData>(); }
        if (windHandler == null) { windHandler = FindObjectOfType<WindHandler>(); }

        windXDirection.text = "0";
        windZDirection.text = "0";
        windXDirection.onValueChanged.AddListener(delegate { ChangeWindDirection(); });
        windZDirection.onValueChanged.AddListener(delegate { ChangeWindDirection(); });
       
    }

    // text update
    public void SpeedText(Vector3 speed)
    {
        speedText.text = $"Velocidade: x:{(int)speed.x},y:{(int)speed.y},z:{(int)speed.z}";
    }

    public void MaxHeight(float maxHeight)
    {
        maxHeightText.text = $"Altura Máxima: {maxHeight.ToString("F2")}";
    }

    public void HeightText(float height)
    {
        heightText.text = $"Altura: {height.ToString("F2")}";
    }

    public void FirstStageFuelText(float fuel)
    {
        firstStageFuelText.text = $"1º Estágio: {(int)fuel}";
    }

    public void NoseFuelText(float fuel)
    {
        noseFuelText.text = $"Nariz: {(int)fuel}";
    }

    // buttons
    public void LaunchRocket() { rocketData.LaunchRocket(); }

    public void ResetData() { rocketData.ResetData(); }

    public void NoseCamera() { rocketData.FollowNose(); }

    public void FirstStageCamera() { rocketData.FollowFirstStage(); }

    // input start values
    public void StartInputValues(Vector3 firstStageThrustStart, Vector3 noseThrustStart, float firstStageFuelStart, float noseFuelStart)
    {
        firstStageFuelInput.text = firstStageFuelStart.ToString();
        firstStageXThrustInput.text = firstStageThrustStart.x.ToString();
        firstStageYThrustInput.text = firstStageThrustStart.y.ToString();
        firstStageZThrustInput.text = firstStageThrustStart.z.ToString();

        noseFuelInput.text = noseFuelStart.ToString();
        noseXThrustInput.text = noseThrustStart.x.ToString();
        noseYThrustInput.text = noseThrustStart.y.ToString();
        noseZThrustInput.text = noseThrustStart.z.ToString();
    }

    // input return
    public int FirstStageFuel() { return int.Parse(firstStageFuelInput.text); }

    public int NoseFuel() { return int.Parse(noseFuelInput.text); }

    public Vector3 FirstStageInputThrust()
    {
        Vector3 rocketThrust = new Vector3(
            int.Parse(firstStageXThrustInput.text),
            int.Parse(firstStageYThrustInput.text),
            int.Parse(firstStageZThrustInput.text));
        return rocketThrust;
    }

    public Vector3 NoseInputThrust()
    {
        Vector3 rocketThrust = new Vector3(
            int.Parse(noseXThrustInput.text),
            int.Parse(noseYThrustInput.text),
            int.Parse(noseZThrustInput.text));
        return rocketThrust;
    }

    private void ChangeWindDirection()
    {
        windHandler.WindValuesChange(int.Parse(windXDirection.text), int.Parse(windZDirection.text));
    }
}
