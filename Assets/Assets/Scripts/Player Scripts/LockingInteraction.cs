using System.Collections;
using UnityEngine;

public class LockingInteraction : MonoBehaviour
{
    public static LockingInteraction ins;

    private bool isCooldown = false;

    //public bool inComputer = 

    public GameObject player;

    private void Awake()
    {
        ins = this;
    }

    public Transform lockingObject;
    public bool locked;

    private bool justLocked;

    public void StartLock(Transform newLockingObject)
    {
        if (locked)
        {
            return;
        }
        if (lockingObject != null)
        {
            if (lockingObject.TryGetComponent<ILockingInteract>(out var currentLock))
            {
                currentLock.OnEndLockInteract();
            }
        }
        lockingObject = newLockingObject;
        locked = true;
        justLocked = true;
    }
    private void Update()
    {
        if (!justLocked && Input.GetKeyDown(KeyCode.E))
        {
            //if (!isCooldown)
            {
                Debug.Log("Registoring E in computer");
                //StartCoroutine(NotSoFast());
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                if (lockingObject != null)//in case it gets destroyed or some shit
                {
                    if (lockingObject.TryGetComponent<ILockingInteract>(out var currentLock))
                    {
                        currentLock.OnEndLockInteract();
                    }
                }
                lockingObject = null;
                locked = false;
                Debug.Log("Quitting computer");
            }

        }
        justLocked = false;
    }

    IEnumerator NotSoFast()
    {
        Debug.Log("waiting");
        isCooldown = true;
        yield return new WaitForSeconds(1f);
        //player.GetComponent<Player_Interaction>().enabled = true;
        isCooldown = false;
        Debug.Log("Waited");
    }
}


public interface ILockingInteract
{
    public void StartLockInteract();
    public void OnEndLockInteract();
}
