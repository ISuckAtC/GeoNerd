using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeScript_v2 : MonoBehaviour
{
    [SerializeField] private bool spotlight; //Is spotlight object active

    [SerializeField] private bool folder = true; //Is case file object active

    public string norwayScene;

    public GameObject spotLight, darkOverlay;

    [Header("Panels")]
    [SerializeField] GameObject playPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject creditsPanel;

    private GameObject currentRightPanel = null;

    [Header("SettingsPanels")]
    [SerializeField] GameObject generalSettingsPanel;
    [SerializeField] GameObject controlsSettingsPanel;






    void Start()
    {
        if (GameManager.Flags[Flag.OSLO_FORESTDONE] && GameManager.Flags[Flag.OSLO_LIBRARYDONE] && GameManager.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE])
        {
            LightToggle();
            GameManager.GameData.DeleteData(GameManager.GameData.playerName);
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
        spotLight.SetActive(spotlight);
        darkOverlay.SetActive(!spotlight);
    }


    public void ChangeRightPanel(string panel)
    {
        switch (panel){
            case "PlayPanel":
                if(currentRightPanel)currentRightPanel.SetActive(false);
                currentRightPanel = playPanel;
                if (currentRightPanel) currentRightPanel.SetActive(true);

                break;
            case "SettingsPanel":
                if (currentRightPanel) currentRightPanel.SetActive(false);
                currentRightPanel = settingsPanel;
                if (currentRightPanel) currentRightPanel.SetActive(true);
                break;
            case "CreditsPanel":
                if (currentRightPanel) currentRightPanel.SetActive(false);
                currentRightPanel = creditsPanel;
                if (currentRightPanel) currentRightPanel.SetActive(true);
                break;
            default:
                Debug.Log("name of the panle incorrect");
                if (currentRightPanel) currentRightPanel.SetActive(false);
                currentRightPanel = null;
                break;


        }
    }
    
    
    public void ChangeSettingstPanel(string panel)
    {
        switch (panel){
            case "GeneralPanel":
                if(settingsPanel.transform.GetChild(settingsPanel.transform.childCount - 1))
                {
                    generalSettingsPanel.transform.SetAsLastSibling();
                }

                break;
            case "ControlsPanel":
                if (settingsPanel.transform.GetChild(settingsPanel.transform.childCount - 1))
                {
                    controlsSettingsPanel.transform.SetAsLastSibling();
                }
                break;
            
            default:
                Debug.Log("name of the panle incorrect");
                break;


        }
    }

   
}
