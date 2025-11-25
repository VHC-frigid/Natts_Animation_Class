using UnityEngine;
using TMPro;

public class Station : MonoBehaviour
{
    public TMP_Text stationText;
    
    public int stationOutput;

    public float timeOfDayActivation;
    public bool newCodeToday;
    public int day;

    [SerializeField] private GameObject sharedStationInputField;

    private void Update()
    {
        var timeOfDay = LightingManager.ins.timeOfDay;

        if (day != LightingManager.ins.day)
        {
            newCodeToday = false;
            day = LightingManager.ins.day;
        }

        if (timeOfDay >= timeOfDayActivation && !newCodeToday)
        {
            newCodeToday = true;
            stationOutput = Random.Range(1000, 10000);
            stationText.text = stationOutput.ToString();
            sharedStationInputField.GetComponent<StationDataComparison>().dataCollectedToday = false;
        }
        
    }

}
