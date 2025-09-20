using System;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public GameObject Gem;
    public Animator GemAnimator;

    private void OnTriggerEnter(Collider other)
    {
        GemAnimator.SetTrigger("JewelPickup");
        Destroy(Gem,1);
    }
}
