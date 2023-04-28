using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera mainCamera;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            canvas.sortingOrder = 10;
            EventManager.EnableOrbitalCamera();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            var position = eventData.position;
            var ray = mainCamera.ScreenPointToRay(position);
            if (Physics.Raycast(ray, out var hit, 100f, Constants.blockLayerMask))
            {
                hit.rigidbody.gameObject.GetComponent<Block>().DisplayDescription();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        
        EventManager.DisableOrbitalCamera();
        canvas.sortingOrder = -1;
    }
}
