using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalMenu : MonoBehaviour
{
    public Image magGlass;
    public Image panasonic;
    public Image tnt;
    public GameObject flash;

    public Sprite magGlassAquired;
    public Sprite panasonicAquired;
    public Sprite tntAquired;



    static UniversalMenu instance = null;
    static public UniversalMenu GetInstance()
    {
        return instance;
    }
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        UpdateUI();
    }

    public void UpdateUI()
    {
        if (GameManager.GameData.Flags[Flag.OSLO_LIBRARYDONE]) magGlass.sprite = magGlassAquired;
        if (GameManager.GameData.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE]) panasonic.sprite = panasonicAquired;
        if (GameManager.GameData.Flags[Flag.OSLO_CASTLEPUZZLECOMPLETE]) tnt.sprite = tntAquired;
    }

    public IEnumerator ActivateCamera()
    {
        Debug.Log("se llama 2");
        flash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
    }
}
