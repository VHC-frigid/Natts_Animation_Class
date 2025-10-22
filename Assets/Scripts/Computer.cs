using UnityEngine;

public class Computer : MonoBehaviour, ILockingInteract
{
    public Transform cameraLockTransform;
    public float cameraLockFOV;
    public void OnEndLockInteract()
    {
        if (CameraLocking.ins.currentLock == cameraLockTransform)
            CameraLocking.ins.currentLock = null;
    }
    public void StartLockInteract()
    {
        var cameraLocking = CameraLocking.ins;
        LockingInteraction.ins.StartLock(transform);
        cameraLocking.currentLock = cameraLockTransform;
        cameraLocking.currentFOV = cameraLockFOV;
    }
}
