using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookMenu : MonoBehaviour
{

    [SerializeField] private bool folder = true; //Is case file object active



    [Header("Panels")]
    [SerializeField] GameObject settingsPanel;

    private GameObject currentRightPanel = null;

    [Header("SettingsPanels")]
    [SerializeField] GameObject generalSettingsPanel;
    [SerializeField] GameObject controlsSettingsPanel;



    public bool debugMode = false;
    public GameObject cheatsMenu;

    void Start()
    {

    }

    private void OnEnable()
    {
        if (debugMode)
        {
            cheatsMenu.SetActive(true);
        }
    }
    void Update()
    {
    }

    public void OpenNotebook()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void CloseNotebook()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;

    }


    public void ChangeRightPanel(string panel)
    {
        switch (panel)
        {
            case "SettingsPanel":
                if (currentRightPanel) currentRightPanel.SetActive(false);
                currentRightPanel = settingsPanel;
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
        switch (panel)
        {
            case "GeneralPanel":
                if (settingsPanel.transform.GetChild(settingsPanel.transform.childCount - 1))
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


    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

}
