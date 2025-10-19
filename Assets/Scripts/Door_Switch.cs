using UnityEngine;

public class Door_Switch : MonoBehaviour
{
    public Animator metalDoor;

    public void ToggleMetalDoor()
    {
        //the ! is setting the bool to what it is currently not set as.
        metalDoor.SetBool("Door_Open?",!metalDoor.GetBool("Door_Open?"));
    }
}
