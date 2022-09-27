using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameData GameData;


    public MapManager currentMapManager;
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

                
            }
            return _instance;
        }
    }



    public void LoadScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
        

    }

}