using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    public static string WorldMapSceneName;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        WorldMapSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
}
