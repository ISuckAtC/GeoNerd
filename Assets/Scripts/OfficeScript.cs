using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeScript : MonoBehaviour
{
    [SerializeField] private bool spotlight; //Is spotlight object active

    [SerializeField] private bool folder = true; //Is case file object active

    public string norwayScene;

    public GameObject normalFile, completeFile, darkOverlay, folderButton, missionButton;

    

    

    void Start()
    {
        if (GameManager.Flags[Flag.OSLO_FORESTDONE] && GameManager.Flags[Flag.OSLO_LIBRARYDONE] && GameManager.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE])
        {
            completeFile.SetActive(true);
            LightToggle();
            GameManager.GameData.DeleteData(GameManager.GameData.playerName);
            normalFile.SetActive(false);
            
        }
    }

    
    void Update()
    {
        
    }

    public void AcceptMission()
    {
        GameManager.Flags[Flag.OFFICE_ARROW] = false;
        GameManager.Flags[Flag.OSLO_ARROW] = true;
        GameManager.Flags[Flag.FOREST_ARROW] = true;
        GameManager.Instance.LoadScene(norwayScene);
    }

    public void LightToggle() //used in the Canvas button for the lightswitch
    {
        spotlight = !spotlight;
        gameObject.SetActive(spotlight);
        darkOverlay.SetActive(!spotlight);

        folderButton.GetComponent<UnityEngine.UI.Button>().interactable = spotlight;
        missionButton.GetComponent<UnityEngine.UI.Button>().interactable = spotlight;
        
    }

    public void FolderOff() //used in the Canvas button for the folder (very temp)
    {
        folder = false;
        gameObject.SetActive(folder);

    }
}
