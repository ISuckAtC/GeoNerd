using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DialogueType { Default, Button}
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
    public GameObject extraButtons;
    DialogueType currentDType = DialogueType.Default;

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


    public void StartDialogue(Dialogue dialogue, DialogueType type)
    {
        chatBox.SetActive(true);
        sentences.Clear();
        myNPCName.text = dialogue.name;
        currentDType = type;
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


        StopAllCoroutines();
        StartCoroutine("AnimateDialogue");
      
    }


    IEnumerator AnimateDialogue()
    {

        char[] myChars = displayingText.text.ToCharArray();
        displayingText.text = "";


        for(int x = 0; x < myChars.Length; x++)
        {
            Debug.Log("adding letter");
            displayingText.text = displayingText.text + myChars[x];
            yield return null;
        }
    }
    private bool EndDialogue()
    {
        if(sentences.Count <= 0)
        {
            Debug.Log("No more sentences");
            switch (currentDType)
            {
                case DialogueType.Default:
                    CloseAndResetDialogueBox();
                    break;
                case DialogueType.Button:
                    extraButtons.SetActive(true);
                    break;
            }

            //Disable things
            return true;
        }

        return false;
    }



    public void CloseAndResetDialogueBox()
    {
        extraButtons.SetActive(false);
        chatBox.SetActive(false);
    }
}