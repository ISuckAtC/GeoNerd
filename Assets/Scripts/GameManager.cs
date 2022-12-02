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
    public bool online = false;
    public string onlineId = "P75jc4rRh";
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

                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) 
                {
                    Debug.Log("NO INTERNET CONNECTION, SETTING ONLINE TO FALSE");
                    _instance.online = false;
                }
                else if (_instance.online)
                {
                    if (!System.Threading.Tasks.Task.Run(() => Network.ServerAlive()).Result) 
                    {
                        Debug.Log("CANNOT REACH SERVER, SETTING ONLINE TO FALSE");
                        _instance.online = false;
                    }
                }
                
                if (_instance.online)
                {
                    Debug.Log("instance is online, id is " + _instance.onlineId.Length);
                    if (_instance.onlineId.Length == 0)
                    {
                        Debug.Log("id is empty");
                        System.Threading.Tasks.Task.Run(() => GameData.OnlineCreateNewUser("DEV")).Wait();
                    }
                    else
                    {
                        GameData.playerId = _instance.onlineId;
                        Debug.Log("About to load online data");
                        System.Threading.Tasks.Task.Run(() => GameData.OnlineLoadData()).Wait();
                    }
                }
                else
                {
                    GameData.LoadData("TESTPLAYER");
                }
                Flags = GameData.Flags;
            }
            return _instance;
        }
    }

    public static void EnsureInstance()
    {
        bool enabled = GameManager.Instance.enabled;
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

    public void QuickRestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        FMOD.Studio.EventInstance play;
        try
        {
            play = FMODUnity.RuntimeManager.CreateInstance(reference);
        }
        catch (FMODUnity.EventNotFoundException e)
        {
            Debug.Log("Caught FMODUnity.EventNotFoundException\n\n" + e.Message + "\n" + e.StackTrace + "\n\n\nIf you see this message, thank Henrik for not making the entire code die");
            return new FMOD.Studio.EventInstance();
        }

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