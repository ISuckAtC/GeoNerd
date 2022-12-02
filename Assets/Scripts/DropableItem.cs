using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DropableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;
    [SerializeField] bool destroyable = true;
    public Item.ItemType unlockableWith = Item.ItemType.NONE;

    [SerializeField] bool animated = false;

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


    public void EndAnimation()
    {
        foreach (GameObject gO in objectsToEnable)
        {
            gO.SetActive(true);
        }
        foreach (GameObject gO in objectsToDisable)
        {
            gO.SetActive(false);
        }
    }
    public void OnDrop(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Item item = pointerData.pointerDrag.gameObject.GetComponent<Item>();
        if (item.itemType == unlockableWith)
        {
            //if (isLoadingScene)

                if (item.itemType == Item.ItemType.Camera)
                {
                    Debug.Log("se llama 1");
                    StartCoroutine(UniversalMenu.GetInstance().ActivateCamera());
                }
                else if (item.itemType == Item.ItemType.TNT)
                {
                    GameManager.FMODPlayStatic(tntExplosionSound, Vector3.zero, Vector3.zero);
                }

            if (!animated)
            {
                foreach (GameObject gO in objectsToEnable)
                {
                    if(gO)
                        gO.SetActive(true);
                }
                foreach (GameObject gO in objectsToDisable)
                {
                    if (gO)
                        gO.SetActive(false);
                }
            }






            if (item.itemType != Item.ItemType.MagnifyingGlass)
            {


                if (!animated && destroyable)
                    Destroy(gameObject);
                else
                {
                    foreach (Animator an in transform.GetComponentsInChildren<Animator>())
                    {
                        an.SetBool("endTransition", true);
                    }

                    Animator anim = GetComponent<Animator>();
                    if (anim)
                        anim.SetBool("endTransition", true);
                }


            }
            else
                foreach (PopUpForestBoxData forestData in restartData)
                    if (forestData.item == item.itemType) ForestManager.Instance.ChangeAndShowPopUpData(forestData);

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
