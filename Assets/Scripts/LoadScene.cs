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

    public GameObject fakeCase, realCase;

    public bool quickload;
    
    void Start()
    {
        if (GameManager.Flags[Flag.OSLO_FORESTDONE] && GameManager.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE] && GameManager.Flags[Flag.OSLO_LIBRARYDONE])
        {
            if (realCase != null)
                realCase.SetActive(false);
            if (fakeCase != null)
                fakeCase.SetActive(true);
        }
    }
    
    
    void Update()
    {
        if (byKey)
        {
            if (Input.GetKeyDown(key))
            {
                Load();
            }
        }
    }

    public void Load()
    {
        if (!quickload)
            GameManager.Instance.LoadScene(sceneName);
        else
            GameManager.Instance.QuickLoadScene(sceneName);
    }
}
