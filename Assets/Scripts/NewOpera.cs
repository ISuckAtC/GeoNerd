using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewOpera : MonoBehaviour
{
    public SpeechBubble firstBubble;
    public SpeechBubble secondBubble;
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;

    public Transform currentlySelected;
    [Header("Put the slots and words here in matching order")]
    public Transform[] wordSlots;
    public NewOperaPuzzleWord[] words;
    public float snapLeniency;
    public GameObject winButton;

    public bool startSequenceImmediately;

    private IEnumerator enumerator;

    public void Start()
    {
        for (int i = 0; i < words.Length; ++i) 
        {
            words[i].overhead = this;
        }
        enumerator = OperaSequence().GetEnumerator();
        if (startSequenceImmediately)
        {
            MoveNext();
        }
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
            winButton.SetActive(true);
        }
    }

    public void MoveNext()
    {
        enumerator.MoveNext();
    }

    public IEnumerable OperaSequence()
    {
        Debug.Log("started");
        firstBubble.Run(() => {
            firstBubble.transform.parent.GetComponent<UnityEngine.UI.Button>().enabled = true;
        });
        yield return null;

        part1.SetActive(false);
        part2.SetActive(true);
        yield return null;

        part2.SetActive(false);
        part3.SetActive(true);
        secondBubble.Run(() => {
            secondBubble.transform.parent.GetComponent<UnityEngine.UI.Button>().enabled = true;
        });
        yield return null;

        GameManager.GameData.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE] = true;
        GameManager.Instance.LoadScene("Oslo");
    }
}
