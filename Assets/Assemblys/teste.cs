using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

     private RectTransform _transform;
     private Canvas _canvas;
     private CanvasGroup _canvasGroup;

     

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 0.5f;
        _canvasGroup.blocksRaycasts = false;
        
        
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        _transform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Apertou!");
    }
}