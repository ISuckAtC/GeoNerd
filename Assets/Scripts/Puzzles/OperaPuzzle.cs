using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperaPuzzle : MonoBehaviour
{
    public GameObject startButton, scroll, endButton, director;
    public Transform currentlySelected;
    [Header("Put the slots and words here in matching order")]
    public Transform[] wordSlots;
    public OperaPuzzleWord[] words;
    public float snapLeniency;

    public string worldSceneName;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < words.Length; ++i) 
        {
            words[i].overhead = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPuzzle()
    {
        startButton.SetActive(false);
        director.SetActive(false);
        scroll.SetActive(true);
    }

    public void End()
    {
        GameManager.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE] = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(worldSceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void CheckPuzzle()
    {
        bool win = true;
        for (int i = 0; i < words.Length; ++i)
        {
            if (words[i].currentOrder != i) win = false;
        }

        if (win)
        {
            // you win, do stuff
            Debug.Log("WIN");
            scroll.SetActive(false);
            endButton.SetActive(true);
            director.SetActive(true);
        }
    }
}
