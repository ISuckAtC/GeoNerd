using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameData GameData;
    public static Dictionary<Flag, bool> Flags
    {
        get
        {
            // Ensures the instance is initialized
            if (!Instance)
            {
                var a = Instance;
            }
            return GameManager.GameData.Flags;
        }
        set { }
    }

    public static List<FMOD.Studio.EventInstance> fmodInstances = new List<FMOD.Studio.EventInstance>();

    public int currentTask = 0;

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

    public void Start()
    {
        // force instancing hack
        bool enabled = Instance.enabled;
    }

    public void QuickLoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == "NorwayMap")
        {
            GameManager.GameData.overWorldPosition = GameObject.Find("Player").transform.position;
            GameManager.GameData.SaveData();
        }
        // testing
        Flags[Flag.TESTFLAG] = true;


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

    public static FMOD.Studio.EventInstance FMODPlayStatic(FMODUnity.EventReference reference, Vector3 position, Vector3 velocity, float volume = 1f, bool registerForDelete = true, bool autoRelease = true)
    {
        FMOD.Studio.EventInstance play = FMODUnity.RuntimeManager.CreateInstance(reference);

        FMOD.ATTRIBUTES_3D attributes;

        attributes.position = FMODUnity.RuntimeUtils.ToFMODVector(position);
        attributes.velocity = FMODUnity.RuntimeUtils.ToFMODVector(velocity);
        attributes.forward = FMODUnity.RuntimeUtils.ToFMODVector(Vector3.forward);
        attributes.up = FMODUnity.RuntimeUtils.ToFMODVector(Vector3.up);

        play.set3DAttributes(attributes);

        float v;
        play.getVolume(out v);
        Debug.Log("Volume: " + v);

        FMOD.RESULT res = play.setVolume(volume);

        if (registerForDelete) fmodInstances.Add(play);

        play.start();
        if (autoRelease) play.release();

        return play;
    }
}