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
        if (Input.GetKeyDown(KeyCode.F))
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
