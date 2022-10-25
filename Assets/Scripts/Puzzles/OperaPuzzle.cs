using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OperaPuzzle : MonoBehaviour
{
    public GameObject startButton, scroll, endButton, director, cameraReward;
    public Transform currentlySelected;
    [Header("Put the slots and words here in matching order")]
    public Transform[] wordSlots;
    public OperaPuzzleWord[] words;
    public float snapLeniency;
    public Texture2D featherCursor;
    public Texture2D normalCursor;
    public RectTransform placementBounds;
    public float minRandomCloseness;

    void Awake()
    {
        for (int i = 0; i < words.Length; ++i) 
        {
            Debug.Log(placementBounds.sizeDelta);
            int overflow = 0;
            do
            {
                if (overflow++ > 50) throw new System.OverflowException("lol");
                words[i].transform.position = new Vector3(
                    Random.Range(placementBounds.position.x, placementBounds.position.x + placementBounds.sizeDelta.x),
                    Random.Range(placementBounds.position.y, placementBounds.position.y + placementBounds.sizeDelta.y),
                    words[i].transform.position.z);
            }
            while(words.Take(i).Any(x => Vector3.Distance(words[i].transform.position, x.transform.position) <= minRandomCloseness));
        }
    }

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
        Cursor.SetCursor(featherCursor, new Vector2(0, 1900f), CursorMode.Auto);
    }

    public void End()
    {
        GameManager.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE] = true;
        GameManager.Instance.LoadScene("Oslo");
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
            cameraReward.SetActive(true);
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
