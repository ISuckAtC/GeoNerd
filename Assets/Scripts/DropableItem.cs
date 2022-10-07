using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DropableItem : MonoBehaviour/*, IDropHandler*/
{
    // Start is called before the first frame update
    public GameObject objectToEnable;
    public Item.ItemType unlockableWith = Item.ItemType.NONE;
    public bool isLoadingScene = false;
    public string sceneToLoad = "";
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
        PointerEventData pointerData = (PointerEventData)data;
        if(pointerData.pointerDrag.gameObject.GetComponent<Item>().itemType == unlockableWith)
        {
            if (isLoadingScene) UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);


            if (objectToEnable) objectToEnable.SetActive(true);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Destroy(gameObject);
        }
        

    }
}
