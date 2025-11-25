using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player_Interaction : MonoBehaviour
{
    Camera mainCamera;
    public float interactRange;
    bool cameraSet = false;

    public UnityEvent computerInteraction;

    private void Update()
    {
        if (!cameraSet && mainCamera == null)
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        if (Input.GetKeyDown(KeyCode.E) && computerInteraction != null)
        {
            computerInteraction.Invoke();
        }
    }

    public void interaction()
    {
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
                Debug.Log("Entering Computer");
            }
        }
    }

}