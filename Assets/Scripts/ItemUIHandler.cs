using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;



public class ItemUIHandler : MonoBehaviour, IBeginDragHandler/*, IDropHandler*/
{
    [SerializeField]
    private Canvas canvas;
    public Transform startingPosition;
    public float draggingScale = 1.25f;
    public float timeToScale = 1f;
    private Vector3 initialScale;
    public bool isInteractable = false;

    private void Start()
    {
        Invoke("StoreScale", 0.5f);     
    }

    public void OnBeginDrag(PointerEventData w)
    {
        if (!isInteractable) return;

        transform.DOScale(initialScale * draggingScale, timeToScale);

    }
   

    private void StoreScale()
    {
        initialScale = transform.localScale;
    }
    public void DragHandler(BaseEventData data)
    {
        if (!isInteractable) return;

        //canvasGroup.blocksRaycasts = false;
        GetComponent<Image>().raycastTarget = false;
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }


    public void OnGrabRelease(BaseEventData data)
    {
        if (!isInteractable) return;

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, LayerMask.NameToLayer("DropableObject")))
        //{
        //    Debug.Log(hit.transform.name);
        //    Debug.Log("hit");
        //}
        //Debug.Log("Raycast tirado");
        //GetComponent<RectTransform>().DOMove(startingPosition, 1f);
        Debug.Log("TNT position: " + startingPosition);
        transform.DOMove(startingPosition.position, 1f);

        GetComponent<Image>().raycastTarget = true;
        transform.DOScale(initialScale, timeToScale);


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
