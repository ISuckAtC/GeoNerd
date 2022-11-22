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
    public NewOperaPuzzleWord[] words;
    public float snapLeniency;
    public float connectionGap;
    public int realAmount;
    public GameObject winButton;

    public bool startSequenceImmediately;

    private IEnumerator enumerator;

    public void Start()
    {
        for (int i = 0; i < words.Length; ++i) 
        {
            words[i].overhead = this;
            words[i].correctOrder = i;
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
        
        NewOperaPuzzleWord current = words[0];
        for (int i = 0; i < words.Length; ++i)
        {
            if (current.correctOrder != i)
            {
                win = false;
                break;
            }
            if (i == words.Length - 1) break;
            if (current.under == null)
            {
                win = false;
                break;
            }
            current = current.under;
            if (i >= realAmount)
            {
                win = false;
                break;
            }
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
