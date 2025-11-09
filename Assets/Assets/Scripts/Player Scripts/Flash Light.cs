using UnityEngine;
using UnityEngine.Events;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> flashLightToggle;
    [SerializeField] private bool flashLightSwitch;

    private void Start()
    {
        flashLightToggle?.Invoke(flashLightSwitch);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Flashlight();
        }
    }

    public void Flashlight()
    {
        flashLightSwitch = !flashLightSwitch;
        flashLightToggle?.Invoke(flashLightSwitch);
    }
}
