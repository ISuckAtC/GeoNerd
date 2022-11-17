using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    enum ItemNeeded {MagnifyingGlass, Camera, TNT}

    [SerializeField] ItemNeeded itemNeeded;
    Texture2D[] cursors;

    private void Start()
    {
        cursors = GameObject.Find("CursorsManager").GetComponent<CursorsManager>().GetCursors();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse enter");
        Cursor.SetCursor(cursors[(int)itemNeeded], Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exit");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }



    
}
