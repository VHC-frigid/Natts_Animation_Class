using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    public Camera FPScamera;
    public float interactRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interaction();
        }
    }

    public void interaction()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPScamera.transform.position, FPScamera.transform.forward, out hit, interactRange))
        {
            //Debug.Log(hit.transform.name);

            Door_Switch doorswitch = hit.transform.GetComponent<Door_Switch>();
            
            if (doorswitch != null)
            {
                doorswitch.ToggleMetalDoor();
            }
        }
    }
}
