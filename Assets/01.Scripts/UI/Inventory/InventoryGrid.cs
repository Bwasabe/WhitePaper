using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryGrid : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("드래그");   
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("다운");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("업");
    }
}
