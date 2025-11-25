using System.Collections;
using UnityEngine;

public class LockingInteraction : MonoBehaviour
{
    public static LockingInteraction ins;

    private bool isCooldown = false;

    private void Awake()
    {
        ins = this;
    }

    public Transform lockingObject;
    public bool locked;

    public void StartLock(Transform newLockingObject)
    {
        if (lockingObject != null)
        {
            if (lockingObject.TryGetComponent<ILockingInteract>(out var currentLock))
            {
                currentLock.OnEndLockInteract();
            }
        }
        lockingObject = newLockingObject;
        locked = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isCooldown)
        {
            StartCoroutine(NotSoFast());
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

    IEnumerator NotSoFast()
    {
        Debug.Log("waiting");
        isCooldown = true;
        yield return new WaitForSeconds(1);
        isCooldown = false;
        Debug.Log("Waited");
    }
}


public interface ILockingInteract
{
    public void StartLockInteract();
    public void OnEndLockInteract();
}
