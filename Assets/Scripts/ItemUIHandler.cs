using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


public class ItemUIHandler : MonoBehaviour/*, IDropHandler*/
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
        //canvasGroup.blocksRaycasts = false;
        GetComponent<Image>().raycastTarget = false;
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position, canvas.worldCamera,  out position );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void OnGrabRelease(BaseEventData data)
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, LayerMask.NameToLayer("DropableObject")))
        //{
        //    Debug.Log(hit.transform.name);
        //    Debug.Log("hit");
        //}
        //Debug.Log("Raycast tirado");
        transform.DOMove(startingPosition, 1f);
        GetComponent<Image>().raycastTarget = true;

        //canvasGroup.blocksRaycasts = true;
    }

    //public void OnDrop(BaseEventData data)
    //{
    //    Debug.Log("BaseEventDrop");
    //    //if (objectToEnable) objectToEnable.SetActive(true);
    //    Destroy(gameObject);

    //}

    //public void OnDrop(PointerEventData data)
    //{
    //    Debug.Log("PointerEventDrop");
    //    //if (objectToEnable) objectToEnable.SetActive(true);
    //    //Destroy(data.pointerDrag.gameObject);
    //}


}
