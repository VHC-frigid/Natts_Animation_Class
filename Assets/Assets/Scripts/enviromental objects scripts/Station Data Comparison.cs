using UnityEngine;
using TMPro;

public class StationDataComparison : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stationText;
    [SerializeField] private TextMeshProUGUI computerText;

    public GameObject player;

    string Clean(string input)
    {
        return input.Replace("\u200B", "").Trim();
    }

    public void Compare()
    {
        string comp = Clean(computerText.text);
        string stat = Clean(stationText.text);

        if (comp == stat)
        {
            player.GetComponent<MoneyHandler>().CorrectDataReward();
        }
        else
        {
            //Debug.Log("not the right code"); 
        }
        
    }
}
