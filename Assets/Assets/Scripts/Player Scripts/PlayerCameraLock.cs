using UnityEngine;

public class PlayerCameraLock : MonoBehaviour
{
    void Update()
    {
        var cameraLocking = CameraLocking.ins;
        if(cameraLocking.currentLock == null)
        {
            cameraLocking.currentLock = transform;
            cameraLocking.currentFOV = cameraLocking.defaultFOV;
        }
    }
}
