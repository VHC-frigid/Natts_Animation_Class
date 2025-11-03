using UnityEngine;
using TMPro;
using NUnit.Framework.Internal;

public class Stationmanager : MonoBehaviour
{
    public float waterQuality;
    public float soilQuality;
    LightingManager lightingManager;

    [SerializeField] private TextMeshProUGUI CampStationText;
    [SerializeField] public TextMeshProUGUI DockStationText;


    public void Awake()
    {
        lightingManager = GameObject.Find("Game Manager").GetComponent<LightingManager>();
    }

    public void Update()
    {
        // when a time of day happens trigger code to make new codes for the day
        if (lightingManager._timeOfDay == 180f)
        {
            //Generate new codes for the day
            CampStationText.text = "test";
            DockStationText.text = "test";

        }
    }
}
