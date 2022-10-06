using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameData GameData;
    public static Dictionary<Flag, bool> Flags;


    public int currentTask = 0;
    
    public MapManager currentMapManager;

    [HideInInspector]
    public string nextScene;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<GameManager>();
                GameData = new GameData();
                GameData.LoadData();
                Flags = GameData.Flags;
            }
            return _instance;
        }
    }



    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        scene.GetRootGameObjects().First(x => x.name == "LOADER").GetComponent<LoadingScreen>().StartLoading(nextScene, SceneManager.GetSceneAt(0));
    }

    public void Task()
    {
        currentTask++;
    }
}