using UnityEngine;
using TMPro;

public class Station : MonoBehaviour
{
    public int stationOutput;
    public float timeOfDayActivation;
    public bool activatedToday;
    public int day;
    public TMP_Text stationText;

    private void Update()
    {
        var timeOfDay = LightingManager.ins.timeOfDay;

        if (day != LightingManager.ins.day)
        {
            activatedToday = false;
            day = LightingManager.ins.day;
        }

        if (timeOfDay >= timeOfDayActivation && !activatedToday)
        {
            activatedToday=true;
            stationOutput = Random.Range(1000, 10000);
            stationText.text = stationOutput.ToString();
        }
        
    }

}
