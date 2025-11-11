using UnityEngine;
using TMPro;

public class MoneyHandler : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI pointsText;
    public int points;

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
        points += 1;
        pointsText.text = points.ToString() + " Points";
        //Debug.Log("trying to increase score");
    }

}
