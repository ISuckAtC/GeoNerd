using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    static DialogueManager instance = null;
    static public DialogueManager GetInstance()
    {
        return instance;
    }

    public GameObject chatBox;
    public Queue<string> sentences;
    public TextMeshProUGUI myNPCName;
    public TextMeshProUGUI displayingText;


    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sentences = new Queue<string>();
    }


    public void StartDialogue(Dialogue dialogue)
    {
        chatBox.SetActive(true);
        sentences.Clear();
        myNPCName.text = dialogue.name;
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentece();
    }


    public void DisplayNextSentece()
    {
        if (EndDialogue())
            return;

        displayingText.text = sentences.Dequeue();
      
    }

    private bool EndDialogue()
    {
        if(sentences.Count <= 0)
        {
            Debug.Log("No more sentences");
            chatBox.SetActive(false);

            //Disable things
            return true;
        }

        return false;
    }
}
