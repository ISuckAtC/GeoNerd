using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    [SerializeField] Texture2D cursor;
    [SerializeField] DialogueType type = DialogueType.Default;
    [SerializeField] GameObject enableObject = null; 
    public Dialogue dialogue;
    private Outline outline;
    private void Start()
    {
        outline = GetComponent<Outline>();
    }
    public void TriggerDialogue()
    {
        DialogueManager.GetInstance().StartDialogue(dialogue, type);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TriggerDialogue();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse enter");
        if (outline) outline.enabled = true;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exit");
        if (outline) outline.enabled = false;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
