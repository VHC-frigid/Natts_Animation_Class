using System;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Animator chest;

    private void OnTriggerEnter()
    {
        chest.SetBool("ChestOpen", true);
    }

    private void OnTriggerExit()
    {
        chest.SetBool("ChestOpen", false);
    }
}
