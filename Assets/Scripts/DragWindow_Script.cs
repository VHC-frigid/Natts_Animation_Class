using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow_Script : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("AHH");
    }
}
