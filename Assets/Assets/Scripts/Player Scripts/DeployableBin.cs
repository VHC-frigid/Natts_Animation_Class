using UnityEngine;

public class DeployableBin : MonoBehaviour, IHold
{
    [HideInInspector] public MoneyHandler mH;
    public int trashAmount;

    public GameObject grabObject;
    public GameObject holdObject;
    Rigidbody rb;
    Collider[] colliders;
    public LayerMask excludeLayerMask;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        OnStopHold(); // just incase we forget to do this manually or so we dont have to
    }
    public void OnHold()
    {
        rb.isKinematic = true;
        holdObject.SetActive(true);
        grabObject.SetActive(false);
        foreach (Collider collider in colliders)
        {
            collider.excludeLayers = excludeLayerMask;
        }
    }
    public void OnStopHold()
    {
        rb.isKinematic = false;
        holdObject.SetActive(false);
        grabObject.SetActive(true);
        foreach (Collider collider in colliders)
        {
            collider.excludeLayers = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(mH == null)
            mH = FindFirstObjectByType<MoneyHandler>();

        if (other.gameObject.CompareTag("Trash"))
        {
            if(other.gameObject.TryGetComponent<Trash>(out var trash))
            {
                mH.points += trash.value;
            }
            else
            {
                mH.points += 1;
            }
            Destroy(other.gameObject);
        }
    }
}
