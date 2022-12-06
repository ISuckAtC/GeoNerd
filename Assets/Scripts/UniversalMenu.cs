using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalMenu : MonoBehaviour
{
   
    public GameObject flash;

    public GameObject magGlassAquired;
    public GameObject panasonicAquired;
    public GameObject tntAquired;



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
        magGlassAquired.SetActive(GameManager.GameData.Flags[Flag.OSLO_LIBRARYDONE] ? true : false); 
        panasonicAquired.SetActive(GameManager.GameData.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE] ? true : false);
        tntAquired.SetActive(GameManager.GameData.Flags[Flag.OSLO_CASTLEPUZZLECOMPLETE] ? true : false);
    }

    public IEnumerator ActivateCamera()
    {
        Debug.Log("se llama 2");
        flash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
    }


    public void RestartScene()
    {
        GameManager.Instance.QuickRestartScene();
    }  
    
    public void LoadScene(string name)
    {
        GameManager.Instance.QuickLoadScene(name);
    }
}   
