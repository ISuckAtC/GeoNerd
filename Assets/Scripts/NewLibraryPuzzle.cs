using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewLibraryPuzzle : MonoBehaviour
{
    public SpeechBubble speechBubble1;
    public SpeechBubble speechBubble2;
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject universal;
    public UISlideItemBar itemSlideBar;

    public Transform currentlySelected;
    [Header("Put the slots and words here in matching order")]
    public Transform[] wordSlots;
    public LibraryBook[] words;
    public float snapLeniency;
    public GameObject winButton;
    public GameObject exitButton;

    private IEnumerator enumerator;

    // Start is called before the first frame update
    void Start()
    {
        speechBubble1.Run((object obj) => {
            speechBubble1.transform.parent.GetComponent<UnityEngine.UI.Button>().enabled = true;
        });
        enumerator = LibrarySequence().GetEnumerator();
        for (int i = 0; i < words.Length; ++i) 
        {
            words[i].overhead = this;
        }
    }

    public void MoveNext()
    {
        enumerator.MoveNext();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            MoveNext();
           
        }
    }

    public IEnumerable LibrarySequence()
    {
        part1.SetActive(false);
        universal.SetActive(true);
        part2.SetActive(true);
        Debug.Log("Part2");
        yield return null;

        part2.SetActive(false);
        part3.SetActive(true);
        universal.SetActive(true);
        speechBubble2.Run((object obj) => {
            speechBubble2.transform.parent.GetComponent<UnityEngine.UI.Button>().enabled = true;
        });
        GameManager.GameData.Flags[Flag.OSLO_LIBRARYDONE] = true;
        Debug.Log("Part3");

        yield return null;

        GameManager.GameData.Flags[Flag.OSLO_LIBRARYDONE] = true;
        
        StartCoroutine(ShowItemAquired());
    }
    public IEnumerator ShowItemAquired()
    {
        itemSlideBar.Select();
        yield return new WaitForSeconds(0.5f);
        exitButton.SetActive(true);
        universal.GetComponent<UniversalMenu>().UpdateUI();
        yield return new WaitForSeconds(0.5f);
        itemSlideBar.Select();
    }

    public void Exit()
    {
        GameManager.Instance.LoadScene("Oslo");
    }
}
