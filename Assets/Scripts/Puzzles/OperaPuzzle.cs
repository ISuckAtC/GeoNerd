using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperaPuzzle : MonoBehaviour
{
    public Transform currentlySelected;
    [Header("Put the slots and words here in matching order")]
    public Transform[] wordSlots;
    public OperaPuzzleWord[] words;
    public float snapLeniency;
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
        }
    }
}
