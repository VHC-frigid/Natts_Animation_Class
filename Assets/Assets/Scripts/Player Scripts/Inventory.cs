using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform hotbar;
    public Transform inventory;
    Hand hand;
    public void Start()
    {
        hand = GetComponent<Hand>();
    }
}
