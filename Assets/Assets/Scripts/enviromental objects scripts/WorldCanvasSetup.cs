using UnityEngine;

public class WorldCanvasSetup : MonoBehaviour
{
    void Start()
    {
        var mainCamera = GameObject.FindWithTag("MainCamera");
        if(mainCamera != null)
        {
            if (mainCamera.TryGetComponent(out Camera camera))
            {
                var canvas = GetComponent<Canvas>();
                canvas.worldCamera = camera;
            }
            else
            {
                Debug.LogError("WorldCanvasSetup Error: MainCamera doesn't have Camera component on it!");
            }
        }
        else
        {
            Debug.LogError("WorldCanvasSetup Error: MainCamera doesn't exist!");
        }
    }
}
