using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookMenu : MonoBehaviour
{

    [SerializeField] private bool folder = true; //Is case file object active
    [SerializeField] private string sceneName;




    [Header("Panels")]
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject loadPanel;

    private GameObject currentRightPanel = null;

    [Header("SettingsPanels")]
    [SerializeField] GameObject generalSettingsPanel;
    [SerializeField] GameObject controlsSettingsPanel;


    public ScrollRect loadScroller;

    public GameObject saveFilePrefab;

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


    public void LoadLoadMenu()
    {
        if (currentRightPanel) currentRightPanel.SetActive(false);
        currentRightPanel = loadPanel;
        if (currentRightPanel) currentRightPanel.SetActive(true);
        string[] saveFiles = System.IO.Directory.GetFiles("./saves/");
        for (int i = 0; i < saveFiles.Length; ++i)
        {
            GameObject saveFile = Instantiate(saveFilePrefab, Vector3.zero, Quaternion.identity);
            RectTransform rect = saveFile.transform as RectTransform;
            rect.parent = loadScroller.viewport.GetChild(0);
            rect.localPosition = new Vector3(0, -20 * i, 0);
            rect.localScale = Vector3.one;
            rect.sizeDelta = new Vector2(0, 20);
        }

        loadScroller.Rebuild(CanvasUpdate.Layout);
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

            case "LoadPanel":
                LoadLoadMenu();
                break;

            default:
                Debug.Log("name of the panel incorrect");
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
                Debug.Log("name of the panel incorrect");
                break;


        }
    }


    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

    public void LoadScene()
    {
        GameManager.Instance.LoadScene("Norway");
    }
}
