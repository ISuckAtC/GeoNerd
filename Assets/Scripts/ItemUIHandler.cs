using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class ItemUIHandler : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    Vector2 startingPosition;

    private void Start()
    {
        Invoke("GetStartingPosition", 0.01f);
       
    }


    private void GetStartingPosition()
    {
        startingPosition = transform.position; 
    }
    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform )canvas.transform, pointerData.position, canvas.worldCamera,  out position );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void OnGrabRelease(BaseEventData data)
    {
        transform.DOMove(startingPosition, 1f);
    }

    
}
