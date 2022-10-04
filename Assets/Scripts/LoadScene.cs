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

    private Scene sc;

    private bool didOffice;
    private bool didOslo;
    
    
    void Update()
    {
        if (byKey)
        {
            if (Input.GetKey(key))
            {
                if (!didOslo && SceneManager.GetActiveScene().name == "OfficeTestRune")
                {
                    didOslo = true;
                    
                    GameManager.Instance.Task();
                }


                if (!didOffice && SceneManager.GetActiveScene().name == "Oslo")
                {
                    didOffice = true;
                    
                    GameManager.Instance.Task();

                }
                
                SceneManager.LoadScene(sceneName);

            }
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
