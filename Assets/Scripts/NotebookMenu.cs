using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    public GameObject inputText;
    public GameObject newGamePanel;

    public bool debugMode = false;
    public GameObject cheatsMenu;

    void Start()
    {
        bool enableGameManager = GameManager.Instance;
        if (!System.IO.Directory.Exists("./saves"))
        {
            System.IO.Directory.CreateDirectory("./saves");
        }
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
        int childCount = loadScroller.viewport.GetChild(0).childCount;
        for (int i = 0; i < childCount; ++i)
        {
            Destroy(loadScroller.viewport.GetChild(0).GetChild(i).gameObject);
        }
        string[] saveFiles = System.IO.Directory.GetFiles("./saves/");
        saveFiles = saveFiles.OrderBy(x => GameManager.GameData.LoadSaveInfo(x).Item2).Reverse().ToArray();
        int saveCount = saveFiles.Length;
        for (int i = 0; i < saveCount; ++i)
        {
            GameObject saveFile = Instantiate(saveFilePrefab, saveFilePrefab.transform.position, saveFilePrefab.transform.rotation);
            RectTransform rect = saveFile.transform as RectTransform;
            rect.SetParent(loadScroller.viewport.GetChild(0));
            rect.offsetMin = new Vector2(0, -20 - (20 * i));
            rect.offsetMax = new Vector2(0, -20 * i);
            rect.localScale = Vector3.one;

            Button b = saveFile.GetComponent<Button>();
            string name = saveFiles[i];
            b.onClick.AddListener(() => LoadPlayer(name.Substring(name.LastIndexOf('/') + 1)));

            TMPro.TextMeshProUGUI text = saveFile.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();

            (string, System.DateTime) info = GameManager.GameData.LoadSaveInfo(name);
            text.text = info.Item1 + "\n" + info.Item2;
        }

        loadScroller.content.sizeDelta = new Vector2(94, 20 * saveCount);
        loadScroller.content.position = new Vector3(0, -(20 * saveCount / 2f), 0);
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

    public void LoadPlayer(string name)
    {
        Debug.Log("Loading loading loading = " + name);
        GameManager.GameData.LoadData(name);
        ActuallyStart();
    }

    public void NewGame()
    {
        newGamePanel.SetActive(true);
    }

    public void CloseNewGame()
    {
        newGamePanel.SetActive(false);
    }

    public void StartGame()
    {
        string name = inputText.GetComponent<TMPro.TextMeshProUGUI>().text;
        if (name.Length < 2) return;
        Debug.Log("Loading");
        GameManager.GameData.LoadData(name);//.RunSynchronously();

        ActuallyStart();
    }

    private void ActuallyStart()
    {
        GameManager.Instance.LoadScene("Norway");
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
