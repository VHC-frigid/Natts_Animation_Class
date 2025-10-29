using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class FireFlicker : MonoBehaviour
{
    public float flickerStrength;
    public int interval;
    public int currentInterval;
    private int count;
    private Vector3 flicker;

    public float timeToFull;
    private float timeCurrent;
    private float startupMult;

    private new Light light;
    public float lightIntensity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponent<Light>();
        //lightIntensity = light.intensity;
        count = 0;
        currentInterval = interval;
        flicker = Vector3.zero;
        timeCurrent = 0f;
        startupMult = 0f;
    }

    private void OnEnable()
    {
        timeCurrent = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeCurrent += Time.fixedDeltaTime;
        startupMult = Mathf.InverseLerp(0f, timeToFull, timeCurrent);
        //print("startupMult: " + startupMult);
        count++;
        if (count % currentInterval == Random.Range(2, interval))
        {
            flicker = new Vector3(OffsetByStrength(), OffsetByStrength(), OffsetByStrength());
            light.intensity = lightIntensity * Random.Range(0.9f, 1f) * startupMult;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, flicker, Vector3.Distance(transform.localPosition, flicker) / 2f);
    }

    private float OffsetByStrength()
    {
        return Mathf.Pow(Random.Range(0, flickerStrength), 2f) * (Random.value > 0.5f ? 1 : -1);
    }
}
