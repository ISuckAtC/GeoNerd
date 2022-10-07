using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadingScreen : MonoBehaviour
{
    private string sceneToLoad;
    private bool newSceneLoaded;
    private GameObject newCanvas;
    public void StartLoading(string sceneToLoad, Scene previous)
    {
        GameManager.GameData.SaveData();
        this.sceneToLoad = sceneToLoad;
        SceneManager.UnloadSceneAsync(previous);
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }

    

    private void Update()
    {
        if (newSceneLoaded && Input.anyKeyDown)
        {
            newCanvas.SetActive(true);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        newCanvas = scene.GetRootGameObjects().First(x => x.name == "Canvas");
        if (newCanvas)
        {
            newCanvas.SetActive(false);
        }
        SceneManager.sceneLoaded -= SceneLoaded;
        newSceneLoaded = true;
    }
}
