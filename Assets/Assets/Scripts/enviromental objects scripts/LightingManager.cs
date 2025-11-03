using System;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light _directionalLight;
    [SerializeField] private Light[] fakeBounceLightSources;
    [SerializeField] private LightingPreset _preset;
    //Variables
    //[SerializeField, Range(0, 24)] private float _timeOfDay;
    [SerializeField, Range(0, 24)] public float _timeOfDay;

    private void Update()
    {
        if (_preset == null)
            return;

        if (Application.isPlaying)
        {
            _timeOfDay += Time.deltaTime * 24f/360f;
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
            for (int i = 0; i < fakeBounceLightSources.Length; i++)
            {
                fakeBounceLightSources[i].color = _preset.DirectionalColour.Evaluate(timePercent) * 0.5f;
            }
            _directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 190f, 0));
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
