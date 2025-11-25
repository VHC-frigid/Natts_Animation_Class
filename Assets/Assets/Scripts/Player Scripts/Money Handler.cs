using UnityEngine;
using TMPro;

public class MoneyHandler : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI pointsText;
    public int points;
    public int rewardAmount;

    private void Start()
    {
        pointsText.text = points.ToString() + " Points";
    }

    private void FixedUpdate()
    {
        pointsText.text = points.ToString() + " Points";
    }

    public void CorrectDataReward()
    {
        points += rewardAmount;
        pointsText.text = points.ToString() + " Points";
        //Debug.Log("trying to increase score");
    }

}
