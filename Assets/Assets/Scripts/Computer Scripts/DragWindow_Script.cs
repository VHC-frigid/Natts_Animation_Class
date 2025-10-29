using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
public class DragWindow_Script : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    public RectTransform windowBar;
    public RectTransform windowContent;
    private GraphicRaycaster graphicRaycaster;
    List<RaycastResult> raycastResults = new List<RaycastResult>();
    bool beingDragged;
    Vector3 startDelta;
    Vector3 currentMousePosition;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        if (graphicRaycaster == null)
        {
            Debug.LogError("DragWindow_Script Error: No graphic raycaster found on parent canvas!");
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        graphicRaycaster.Raycast(eventData, raycastResults);
        if (raycastResults.Count > 0)
        {
            var result = raycastResults[raycastResults.Count - 1]; //raycast against desktop background
            currentMousePosition = result.worldPosition;
        }
        raycastResults.Clear();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        graphicRaycaster.Raycast(eventData, raycastResults);
        if (raycastResults.Count > 0)
        {
            var result = raycastResults[0];
            if (result.gameObject == windowContent.gameObject || result.gameObject == windowBar.gameObject)
            {
                rectTransform.SetAsLastSibling();
            }
            if (result.gameObject == windowBar.gameObject)
            {
                beingDragged = true;
                var desktopBackgroundResult = raycastResults[raycastResults.Count - 1];
                startDelta = rectTransform.position - desktopBackgroundResult.worldPosition;
                currentMousePosition = desktopBackgroundResult.worldPosition;
            }
        }
        raycastResults.Clear();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        beingDragged = false;
    }
    private void Update()
    {
        if(beingDragged)
        {
            rectTransform.position = currentMousePosition + startDelta;
        }
    }
}
