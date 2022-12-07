using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayOnStart : MonoBehaviour
{
    public FMODUnity.EventReference reference;
    public Vector3 velocity;
    public float volume;
    public int persist = 1;
    private FMOD.Studio.EventInstance instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = GameManager.FMODPlayStatic(reference, transform.position, velocity, volume, persist == 0);
        if (persist > 0)
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += Persist;
        }
    }

    void Persist(Scene scene, LoadSceneMode mode)
    {
        if (--persist <= 0)
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
            SceneManager.sceneLoaded -= Persist;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
