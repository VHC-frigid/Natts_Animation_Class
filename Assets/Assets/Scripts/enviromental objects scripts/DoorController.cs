using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator door;

    private void OnTriggerEnter(Collider other)
    {
        door.SetBool("DoorOpen", true);
    }

    private void OnTriggerExit(Collider other)
    {
        door.SetBool("DoorOpen", false);
    }
}
