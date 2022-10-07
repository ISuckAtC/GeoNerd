using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlePuzzle : MonoBehaviour
{
    public GameObject startButton, table, endButton, king, reward, initialBackground, finalBackground;
    public Transform currentlySelected;
    [Header("Put the slots and words here in matching order")]
    public Transform[] partSlots;
    public CastlePuzzleParts[] parts;
    public float snapLeniency;
    //public Texture2D featherCursor;
    //public Texture2D normalCursor;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < parts.Length; ++i) 
        {
            parts[i].overhead = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPuzzle()
    {
        startButton.SetActive(false);
        king.SetActive(false);
        table.SetActive(true);
        //Cursor.SetCursor(featherCursor, new Vector2(0, 1900f), CursorMode.Auto);
    }

    public void End()
    {
        GameManager.Flags[Flag.OSLO_CASTLEPUZZLECOMPLETE] = true;
        GameManager.Instance.LoadScene("Oslo");
    }

    public void CheckPuzzle()
    {
        bool win = true;
        for (int i = 0; i < parts.Length; ++i)
        {
            if (parts[i].currentOrder != i) win = false;
        }

        if (win)
        {
            // you win, do stuff
            Debug.Log("WIN");
            table.SetActive(false);
            endButton.SetActive(true);
            king.SetActive(true);
            reward.SetActive(true);
            initialBackground.SetActive(false);
            finalBackground.SetActive(true);
            //Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
