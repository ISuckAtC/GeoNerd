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

    public void StartLoading(string sceneToLoad, Scene previous)
    {
        int index = loadingScreenSceneNames.IndexOf(sceneToLoad);
        if (index > -1)
        {
            background.sprite = loadingScreens[index];
        }

        InvokeRepeating("LoadIndicator", 0f, loadIndicatorInterval);

        GameManager.GameData.SaveData();
        this.sceneToLoad = sceneToLoad;
        SceneManager.UnloadSceneAsync(previous);

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
        if (newSceneLoaded && Input.anyKeyDown)
        {
            foreach (GameObject canvas in newCanvas) canvas.SetActive(true);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        doneLoading = true;
        loadIndicator.sprite = loadCompleteIndicator;

        newCanvas = FindObjectsInScene(scene, "Canvas", true).ToArray();
        foreach (GameObject canvas in newCanvas) canvas.SetActive(false);
        SceneManager.sceneLoaded -= SceneLoaded;
        newSceneLoaded = true;
    }

    public static List<GameObject> FindObjectsInScene(Scene scene, string name, bool onlyActive)
    {
        List<GameObject> objects = new List<GameObject>();

        GameObject[] rootObjects = scene.GetRootGameObjects();

        for (int i = 0; i < rootObjects.Length; ++i) FindObjectsRecursive(rootObjects[i].transform, name, objects, onlyActive);

        return objects;
    }

    private static void FindObjectsRecursive(Transform transform, string name, List<GameObject> storage, bool onlyActive)
    {
        if (transform.name == name && (onlyActive ? transform.gameObject.activeSelf : true)) storage.Add(transform.gameObject);
        for (int i = 0; i < transform.childCount; ++i) FindObjectsRecursive(transform.GetChild(i), name, storage, onlyActive);
    }
}
