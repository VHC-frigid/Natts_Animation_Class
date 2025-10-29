using Unity.Mathematics;
using UnityEngine;

public class CameraLocking : MonoBehaviour
{
    //[SerializeField] private Camera _camera;
    public static CameraLocking ins;
    private void Awake()
    {
        ins = this;
    }
    [HideInInspector] public Transform currentLock;
    [HideInInspector] public float currentFOV;
    [HideInInspector] public float defaultFOV;
    Camera mainCamera;
    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        defaultFOV = mainCamera.fieldOfView;
        currentFOV = defaultFOV;
    }
    private void Update()
    {
        if (currentLock == null)
            return;
        transform.position = math.lerp(transform.position, currentLock.position, 0.2f);
        transform.rotation = math.slerp(transform.rotation, currentLock.rotation, 0.2f);
        mainCamera.fieldOfView = math.lerp(mainCamera.fieldOfView, currentFOV, 0.08f);
    }
}
