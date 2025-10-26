using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow_Script : MonoBehaviour, IDragHandler
{

    [SerializeField] private RectTransform panelTranform;
    [SerializeField] private Canvas canvas;

    public void OnDrag(PointerEventData eventData)
    {
        panelTranform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
