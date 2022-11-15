using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DropableItem : MonoBehaviour/*, IDropHandler*/
{
    // Start is called before the first frame update
    public GameObject objectToEnable;
    public Item.ItemType unlockableWith = Item.ItemType.NONE;

    public PopUpForestBoxData[] restartData;

    public FMODUnity.EventReference cameraFlashSound;
    public FMODUnity.EventReference tntExplosionSound;
    
    
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
        Item item = pointerData.pointerDrag.gameObject.GetComponent<Item>();
        if (item.itemType == unlockableWith)
        {
            if (isLoadingScene) UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);

            if (item.itemType == Item.ItemType.Camera)
            {
                GameManager.FMODPlayStatic(cameraFlashSound, Vector3.zero, Vector3.zero);
            }
            else if (item.itemType == Item.ItemType.TNT)
            {
                GameManager.FMODPlayStatic(tntExplosionSound, Vector3.zero, Vector3.zero);
            }

            if (objectToEnable) objectToEnable.SetActive(true);





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
