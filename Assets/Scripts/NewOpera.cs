using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using System.Linq;

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

    public RectTransform placementBounds;
    public float minRandomCloseness;

    void Awake()
    {
        for (int i = 0; i < words.Length; ++i) 
        {
            int overflow = 0;
            do
            {
                if (overflow++ > 50) Debug.LogError ("lol");
                words[i].transform.position = new Vector3(
                    Random.Range(placementBounds.position.x, placementBounds.position.x + placementBounds.sizeDelta.x),
                    Random.Range(placementBounds.position.y, placementBounds.position.y + placementBounds.sizeDelta.y),
                    words[i].transform.position.z);
            }
            while(words.Take(i).Any(x => Vector3.Distance(words[i].transform.position, x.transform.position) <= minRandomCloseness));
        }
    }
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
            if (i == realAmount - 1) break;
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
        firstBubble.Run((object obj) => {
            firstBubble.transform.parent.GetComponent<UnityEngine.UI.Button>().enabled = true;
        });
        yield return null;

        part1.SetActive(false);
        part2.SetActive(true);
        yield return null;

        part2.SetActive(false);
        part3.SetActive(true);
        secondBubble.Run((object obj) => {
            secondBubble.transform.parent.GetComponent<UnityEngine.UI.Button>().enabled = true;
        });
        yield return null;

        GameManager.GameData.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE] = true;
        GameManager.Instance.LoadScene("Oslo");
    }
}
