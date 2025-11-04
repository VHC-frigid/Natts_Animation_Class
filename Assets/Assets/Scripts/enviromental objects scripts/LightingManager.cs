using System;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light directionalLight;
    [SerializeField] private Light[] fakeBounceLightSources;
    [SerializeField] private LightingPreset preset;
    //Variables
    //[SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField, Range(0, 24)] public float timeOfDay;

    public int day;
    public static LightingManager ins;

    public void Awake()
    {
        ins = this;
    }

    private void Update()
    {
        if (preset == null)
            return;

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * 24f/360f;

            if (timeOfDay >= 24)
            {
                timeOfDay = 0;
                day++;
            }

            UpdateLighting(timeOfDay / 24f);
        }
        else
        {
            UpdateLighting(timeOfDay/ 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        UnityEngine.RenderSettings.ambientLight = preset.AmbientColour.Evaluate(timePercent);
        UnityEngine.RenderSettings.fogColor = preset.FogColour.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = preset.DirectionalColour.Evaluate(timePercent);
            for (int i = 0; i < fakeBounceLightSources.Length; i++)
            {
                fakeBounceLightSources[i].color = preset.DirectionalColour.Evaluate(timePercent) * 0.5f;
            }
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 190f, 0));
        }
    }

    private void OnValidate()
    {
        if (directionalLight != null)
            return;

        if (UnityEngine.RenderSettings.sun != null)
        {
            directionalLight = UnityEngine.RenderSettings.sun;
        }
        else
        {
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == UnityEngine.LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    } 
}
