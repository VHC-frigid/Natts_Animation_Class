using UnityEngine;

public class Computer : MonoBehaviour, ILockingInteract
{
    public Transform cameraLockTransform;
    public float cameraLockFOV;
    public void OnEndLockInteract()
    {
        if (CameraLocking.ins.currentLock == cameraLockTransform)
            CameraLocking.ins.currentLock = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void StartLockInteract()
    {
        var cameraLocking = CameraLocking.ins;
        LockingInteraction.ins.StartLock(transform);
        cameraLocking.currentLock = cameraLockTransform;
        cameraLocking.currentFOV = cameraLockFOV;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
