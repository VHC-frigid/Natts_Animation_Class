using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
[ExecuteAlways]
public class PC_RPAsset_asset : MonoBehaviour
{
    public GameObject e;
    public LayerMask l;
    GameObject c;
    SpriteRenderer sr;
    Camera mainCamera;
    GameObject hO;
    float tm;
    LightingManager lm;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        if(c != null)
            Destroy(c);
    }
    void Update()
    {
        if (lm == null)
        {
            lm = FindAnyObjectByType<LightingManager>();
            return;
        }
        if (lm.timeOfDay < 22f && lm.timeOfDay > 3)
        {
            if (Application.isPlaying)
            {
                if (c != null)
                    Destroy(c);
            }
            else
            {
                if (c != null)
                    DestroyImmediate(c);
            }
            return;
        }

        if (c == null)
        {
            c = Instantiate(e,transform);
            sr = c.GetComponent<SpriteRenderer>();
            sr.color = Color.clear;
            c.transform.localScale = Vector3.one * 3f;
        }

        float3 p = float3.zero;
        float3 dir = float3.zero;
        float3 r = float3.zero;
#if UNITY_EDITOR
        SceneView sV = SceneView.lastActiveSceneView;
        if (sV != null)
        {
            p = sV.camera.transform.position;
            dir = sV.camera.transform.forward;
            r = sV.camera.transform.right;
        }
#endif
        if (Application.isPlaying)
        {
            p = mainCamera.transform.position;
            dir = mainCamera.transform.forward;
            r = mainCamera.transform.right;
        }
        var ray = new Ray
        {
            direction = dir,
            origin = p,
        };
        tm += Time.unscaledDeltaTime;
        if (Physics.SphereCast(ray, 2f, out var hitInfo, math.INFINITY, l))
        {
            if (hO != hitInfo.transform.gameObject)
            {
                tm = 0f;
                hO = hitInfo.transform.gameObject;
            }
            float3 targetPos = hitInfo.transform.position;
            targetPos.y = p.y;
            var capsuleCollider = (CapsuleCollider)hitInfo.collider;
            targetPos += dir * (capsuleCollider.radius + 0.3f);
            float3 x = r * (capsuleCollider.radius + 0.12f);
            if (tm < 1f)
            {
                sr.color = Color.white * tm;
            }
            else
            {
                sr.color = Color.white*(1f-math.clamp(tm - 4.25f, 0f, 1f));
            }
            c.transform.position = targetPos + x * math.clamp(1f-tm,0f,1f);
            c.transform.forward = c.transform.position - (Vector3)p;
        }
        if(math.distance(c.transform.position, p) < 8f)
        {
            sr.color = Color.clear;
        }
#if UNITY_EDITOR
        SceneView.RepaintAll();
#endif
    }
    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            if (c != null)
                Destroy(c);
        }
        else
        {
            if (c != null)
                DestroyImmediate(c);
        }
    }
}
