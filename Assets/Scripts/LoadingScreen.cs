using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private string sceneToLoad;
    private bool newSceneLoaded;
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
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        newSceneLoaded = true;
    }
}
