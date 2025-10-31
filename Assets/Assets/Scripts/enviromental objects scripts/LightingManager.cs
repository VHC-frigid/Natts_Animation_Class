using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light _directionalLight;
    [SerializeField] private LightingPreset _preset;
    //Variables
    [SerializeField, Range(0, 24)] private float _timeOfDay;

    private void Update()
    {
        if (_preset == null)
            return;

        if (Application.isPlaying)
        {
            _timeOfDay += Time.deltaTime;
            _timeOfDay %= 24; //clamp between 0-24
            UpdateLighting(_timeOfDay / 24f);
        }
        else
        {
            UpdateLighting(_timeOfDay/ 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        UnityEngine.RenderSettings.ambientLight = _preset.AmbientColour.Evaluate(timePercent);
        UnityEngine.RenderSettings.fogColor = _preset.FogColour.Evaluate(timePercent);

        if (_directionalLight != null)
        {
            _directionalLight.color = _preset.DirectionalColour.Evaluate(timePercent);
            _directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (_directionalLight != null)
            return;

        if (UnityEngine.RenderSettings.sun != null)
        {
            _directionalLight = UnityEngine.RenderSettings.sun;
        }
        else
        {
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == UnityEngine.LightType.Directional)
                {
                    _directionalLight = light;
                    return;
                }
            }
        }
    } 
}
