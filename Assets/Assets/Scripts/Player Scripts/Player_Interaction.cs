using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player_Interaction : MonoBehaviour
{
    Camera mainCamera;
    public float interactRange;
    bool cameraSet = false;

    public GameObject player;

    public UnityEvent computerInteraction;

    private void Update()
    {
        if (!cameraSet && mainCamera == null)
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            interaction();
        }
    }

    public void interaction()
    {
        if (LockingInteraction.ins.locked)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, interactRange))
        {
            //Debug.Log(hit.transform.name);
            if(hit.transform.TryGetComponent(out Door_Switch doorSwitch))
            {
                doorSwitch.ToggleMetalDoor();
            }
            if(hit.transform.TryGetComponent(out ILockingInteract lockingInteract))
            {
                lockingInteract.StartLockInteract();
                //player.GetComponent<Player_Interaction>().enabled = false;
                Debug.Log("Entering Computer");
            }
        }
    }

}