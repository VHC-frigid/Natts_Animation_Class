using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow_Script : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform dragRectTransform;
    [SerializeField] private Canvas canvas;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("AHH");
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
