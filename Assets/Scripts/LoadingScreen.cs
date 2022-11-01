using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadingScreen : MonoBehaviour
{
    public List<Sprite> loadingScreens;
    public List<string> loadingScreenSceneNames;
    public UnityEngine.UI.Image background;
    [SerializeField]
    private string sceneToLoad;
    private bool newSceneLoaded;
    private GameObject[] newCanvas;

    public float loadDelay = 0f;

    public UnityEngine.UI.Image loadIndicator;
    public float loadIndicatorInterval;
    public Sprite[] loadIndicatorFrames;
    private int loadIndicatorIndex = 0;
    private bool doneLoading;
    public Sprite loadCompleteIndicator;
    public UnityEngine.UI.Button continueButton;
    public TMPro.TextMeshProUGUI nextSceneText;
    public UnityEngine.UI.Slider slider;
    private int index;
    private bool loading;

    public void StartLoading(string sceneToLoad, Scene previous)
    {
        index = loadingScreenSceneNames.IndexOf(sceneToLoad);
        nextSceneText.text = "...ON YOUR WAY TO: " + sceneToLoad.ToUpper();
        continueButton.image.color = new Color32(100, 100, 100, 150);
        if (index > -1)
        {
            background.sprite = loadingScreens[index];

            //continueButton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Loading...";
        }


        //InvokeRepeating("LoadIndicator", 0f, loadIndicatorInterval);

        GameManager.GameData.SaveData();
        this.sceneToLoad = sceneToLoad;
        SceneManager.UnloadSceneAsync(previous);

        while (0 < GameManager.fmodInstances.Count)
        {
            GameManager.fmodInstances[0].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            GameManager.fmodInstances.RemoveAt(0);
        }

        loading = true;

        Invoke("DelayedLoad", loadDelay);
    }

    public void DelayedLoad()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }

    public void LoadIndicator()
    {
        if (doneLoading) return;
        loadIndicator.sprite = loadIndicatorFrames[loadIndicatorIndex];
        if (++loadIndicatorIndex == loadIndicatorFrames.Length)
        {
            loadIndicatorIndex = 0;
        }
    }

    private void Update()
    {
        if (loading)
        {
            slider.value += (Time.deltaTime / loadDelay);
            if (slider.value > 1f) slider.value = 1f;
        }
        if (newSceneLoaded && Input.anyKeyDown)
        {
            foreach (GameObject canvas in newCanvas) canvas.SetActive(true);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }

    public void OnContinueConfirmed()
    {
        if (newSceneLoaded)
        {
            foreach (GameObject canvas in newCanvas) canvas.SetActive(true);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //continueButton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Continue to: " + loadingScreenSceneNames[index];
        continueButton.enabled = true;
        continueButton.image.color = new Color32(255, 255, 255, 255);

        doneLoading = true;
        //loadIndicator.sprite = loadCompleteIndicator;

        newCanvas = FindObjectsInScene(scene, "Canvas", true).ToArray();
        foreach (GameObject canvas in newCanvas) canvas.SetActive(false);
        SceneManager.sceneLoaded -= SceneLoaded;
        newSceneLoaded = true;
    }

    // Uses recursive search to find every object of name in a scene, GameObject.Find is weird when it comes to interscene logic so this ensures you grab the needed gameobjects
    // has the option to only find active ones (as these are usually the only ones you want to touch from the loading screen)
    public static List<GameObject> FindObjectsInScene(Scene scene, string name, bool onlyActive)
    {
        List<GameObject> objects = new List<GameObject>();

        GameObject[] rootObjects = scene.GetRootGameObjects();

        for (int i = 0; i < rootObjects.Length; ++i) FindObjectsRecursive(rootObjects[i].transform, name, objects, onlyActive);

        return objects;
    }

    // Simple recursive search
    private static void FindObjectsRecursive(Transform transform, string name, List<GameObject> storage, bool onlyActive)
    {
        if (transform.name == name && (onlyActive ? transform.gameObject.activeSelf : true)) storage.Add(transform.gameObject);
        for (int i = 0; i < transform.childCount; ++i) FindObjectsRecursive(transform.GetChild(i), name, storage, onlyActive);
    }
}
