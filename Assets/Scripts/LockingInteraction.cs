using UnityEngine;

public class LockingInteraction : MonoBehaviour
{
    public static LockingInteraction ins;
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
        if (locked && Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
            if (lockingObject != null)//in case it gets destroyed or some shit
            {
                if (lockingObject.TryGetComponent<ILockingInteract>(out var currentLock))
                {
                    currentLock.OnEndLockInteract();
                }
            }
            lockingObject = null;
            locked = false;
        }
    }
}
public interface ILockingInteract
{
    public void StartLockInteract();
    public void OnEndLockInteract();
}
