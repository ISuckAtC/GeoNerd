using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropableItem : MonoBehaviour/*, IDropHandler*/
{
    // Start is called before the first frame update
    public GameObject objectToEnable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnDrop(BaseEventData data)
    {
        Debug.Log("BaseEventDrop");
        if (objectToEnable) objectToEnable.SetActive(true);
        Destroy(gameObject);

    }


    //public void OnDrop(PointerEventData data)
    //{
    //    Debug.Log("PointerEventDrop");
    //    if (objectToEnable) objectToEnable.SetActive(true);
    //    //Destroy(data.pointerDrag.gameObject);
    //}
}
