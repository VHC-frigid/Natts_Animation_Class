using UnityEngine;

public class QuickDoor : MonoBehaviour
{
    public Animator doorC;


    private void OnTriggerEnter(Collider other)
    {
        doorC.SetBool("Door_Open?", true);
    }

    private void OnTriggerExit(Collider other)
    {
        doorC.SetBool("Door_Open?", false);
    }
}
