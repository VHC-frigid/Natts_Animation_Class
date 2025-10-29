using UnityEngine;

public class Computer : MonoBehaviour, ILockingInteract
{
    public Transform cameraLockTransform;
    public float cameraLockFOV;
    //[SerializeField] private GameObject PCScreen;
    public void OnEndLockInteract()
    {
        if (CameraLocking.ins.currentLock == cameraLockTransform)
            CameraLocking.ins.currentLock = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //PCScreen.SetActive(false);
    }
    public void StartLockInteract()
    {
        var cameraLocking = CameraLocking.ins;
        LockingInteraction.ins.StartLock(transform);
        cameraLocking.currentLock = cameraLockTransform;
        cameraLocking.currentFOV = cameraLockFOV;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //PCScreen.SetActive(true);
    }
}
