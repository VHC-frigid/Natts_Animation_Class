using UnityEngine;
using TMPro;

public class StationDataComparison : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stationText;
    [SerializeField] private TextMeshProUGUI computerText;

    [SerializeField] private TextMeshProUGUI pointsText;
    public int points;

    private void Start()
    {
        pointsText.text = points.ToString() + " Points";
    }

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
            CorrectData();
            //Debug.Log("skibidi 67 alert"); 
        }
        else
        {
            Debug.Log("not the right code"); 
        }
        //Debug.Log("Submission");
        //Debug.Log("Computer Text is" + computerText.text);
        //Debug.Log("station Text is" + stationText.text);
    }

    public void CorrectData()
    {
        pointsText.text = points.ToString() + " Points";
        points += 1;
        Debug.Log("Guh");
    }
}
