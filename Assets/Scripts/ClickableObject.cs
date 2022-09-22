using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Texture2D cursor;
    // Start is called before the first frame update


    //private void OnMouseEnter()
    //{
    //    Debug.Log("Enters");
    //    Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    //}

    //private void OnMouseExit()
    //{
    //    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }



    //private void Update()
    //{
    //    if(EventSystem.current.IsPointerOverGameObject())
    //        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    //    else
    //        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    //}
}
