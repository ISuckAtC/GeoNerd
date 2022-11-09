using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    [SerializeField] Texture2D cursor;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        DialogueManager.GetInstance().StartDialogue(dialogue);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TriggerDialogue();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse enter");
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exit");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
