using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DropableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public GameObject objectToEnable;
    public GameObject objectToDisable;
    public Item.ItemType unlockableWith = Item.ItemType.NONE;

    public PopUpForestBoxData[] restartData;

    
    public FMODUnity.EventReference tntExplosionSound;

    private Outline outline;
    
    public bool isLoadingScene = false;
    
    void Start()
    {
        outline = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse enter");
        if (outline) outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exit");
        if (outline) outline.enabled = false;

    }

    

    public void OnDrop(BaseEventData data)
    {
        Debug.Log("BaseEventDrop");
        PointerEventData pointerData = (PointerEventData)data;
        Item item = pointerData.pointerDrag.gameObject.GetComponent<Item>();
        if (item.itemType == unlockableWith)
        {
            if (isLoadingScene) 

            if (item.itemType == Item.ItemType.Camera)
            {
                
            }
            else if (item.itemType == Item.ItemType.TNT)
            {
                GameManager.FMODPlayStatic(tntExplosionSound, Vector3.zero, Vector3.zero);
            }

            if (objectToEnable) objectToEnable.SetActive(true);
            if (objectToDisable) objectToDisable.SetActive(false);





            if (item.itemType != Item.ItemType.MagnifyingGlass)
                Destroy(gameObject);
            else
                foreach(PopUpForestBoxData forestData in restartData)
                    if(forestData.item == item.itemType) ForestManager.Instance.ChangeAndShowPopUpData(forestData);

        }
        else
        {
            foreach (PopUpForestBoxData forestData in restartData)
                if (forestData.item == item.itemType)
                    if (forestData.restart)
                        ForestManager.Instance.GoToStartpoint(forestData);
                    else
                        ForestManager.Instance.ChangeAndShowPopUpData(forestData);
        }
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
