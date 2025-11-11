using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class Hand : MonoBehaviour
{
    public float forceAmount = 500f;
    public float reach = 162f;
    public float maxForce = 13f;
    public float yeetForce = 420f;
    private Rigidbody grabbedObject;
    IGrabbable grabbable;
    private Rigidbody heldObject;

    private bool readyToGrab = true;
    public float grabCooldown = 0.1f;

    [SerializeField] private float _selectionDistance;
    [SerializeField, Range(0, 1)] private float _holdHeight;
    [SerializeField] private float _minFalloff = 3f;
    [SerializeField] private float _maxFalloff = 7f;

    Vector3 _offset;
    Vector3 _orignalPosition;

    private Camera _camera;

    [Header("Mouse Scroll Hold Distance Settings")]
    float selectionDistanceShift;
    public float selectionDistanceSensitivity;

    Rigidbody playerRigidbody;
    HandState handState;

    [Header("Hold Settings")]
    [Range(0f, 1f)] public float screenHoldX;
    [Range(0f, 1f)] public float screenHoldY;
    public float holdZDistance;
    public float holdObjectMoveSpeed;
    private void Start()
    {
        _camera = Camera.main ? Camera.main : FindFirstObjectByType<Camera>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void TryGrab()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reach))
        {
            grabbedObject = hit.rigidbody;
            handState = HandState.Grabbing;
        }
    }
    void Update()
    {
        switch (handState)
        {
            case HandState.Empty: // we can only grab something if hand is empty
                selectionDistanceShift = 0f;
                if (readyToGrab && Input.GetMouseButtonDown(0))
                {
                    TryGrab();
                    StartCoroutine(CoolDown());
                }
                break;
            case HandState.Grabbing:
                if (Input.GetMouseButtonUp(0))
                {
                    LetGoOfGrabbedObject();
                    break;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    if (grabbedObject != null)
                        grabbedObject.AddForce(_camera.transform.forward * yeetForce, ForceMode.Impulse);
                    LetGoOfGrabbedObject();
                    break;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    HoldGrabbedObject();
                    break;
                }
                if (grabbedObject != null && grabbedObject.TryGetComponent(out grabbable))
                {
                    grabbable.OnGrabbed();
                }
                selectionDistanceShift += Input.mouseScrollDelta.y * selectionDistanceSensitivity;
                selectionDistanceShift = math.clamp(selectionDistanceShift, -_selectionDistance + 0.325f, 0f);
                break;
            case HandState.Holding:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StopHoldingObject();
                    break;
                }
                if (heldObject != null && heldObject.TryGetComponent(out IHold hold))
                {
                    hold.OnHold();
                }
                heldObject.transform.forward += (_camera.transform.forward - heldObject.transform.forward) * Time.deltaTime * holdObjectMoveSpeed;
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (handState)
        {
            case HandState.Empty:
                break;
            case HandState.Grabbing:
                if (grabbedObject == null || grabbedObject.isKinematic)
                    break;
                Vector3 mouseObjectDelta = _camera.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * _holdHeight, _selectionDistance + selectionDistanceShift));
                float distance = (grabbedObject.centerOfMass - transform.position).magnitude;
                grabbedObject.linearVelocity = (mouseObjectDelta - grabbedObject.transform.position) * (forceAmount * Time.fixedDeltaTime);
                grabbedObject.linearVelocity += playerRigidbody.linearVelocity;
                if (grabbedObject.linearVelocity.magnitude > maxForce)
                {
                    grabbedObject.linearVelocity = grabbedObject.linearVelocity.normalized * maxForce;
                }
                break;
            case HandState.Holding:
                Vector3 holdPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width * screenHoldX, Screen.height * screenHoldY, holdZDistance));
                heldObject.transform.position += (holdPoint - heldObject.transform.position) * Time.fixedDeltaTime * holdObjectMoveSpeed;
                heldObject.transform.position += playerRigidbody.linearVelocity * Time.fixedDeltaTime;
                break;
        }
    }
    //
    public virtual IEnumerator CoolDown()
    {
        readyToGrab = false;
        yield return new WaitForSeconds(grabCooldown);
        readyToGrab = true;
    }
    public void LetGoOfGrabbedObject()
    {
        if (grabbedObject != null && grabbedObject.TryGetComponent(out grabbable))
        {
            grabbable.OnLetGo();
        }

        grabbedObject = null;
        handState = HandState.Empty;
    }
    public void HoldGrabbedObject()
    {
        if (grabbedObject != null)
        {
            heldObject = grabbedObject;
            grabbedObject = null;

            handState = HandState.Holding;
        }
    }
    public void StopHoldingObject()
    {
        if (heldObject != null && heldObject.TryGetComponent(out IHold hold))
        {
            hold.OnStopHold();

            grabbedObject = heldObject;
            heldObject = null;
            selectionDistanceShift = -_selectionDistance + 0.325f;
            handState = HandState.Grabbing;
        }
    }
}
public enum HandState
{
    Empty, Grabbing, Holding,
}