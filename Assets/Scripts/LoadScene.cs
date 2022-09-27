using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("By Key or by method call")]

    public bool byKey;
    public KeyCode key;
    public string sceneName;
    
    
    
    void Update()
    {
        if (byKey)
        {
            if (Input.GetKey(key))
                SceneManager.LoadScene(sceneName);
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
