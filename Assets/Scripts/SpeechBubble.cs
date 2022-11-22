using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public bool runOnStart;
    public string text;
    public float characterCD;
    public float spaceCD;
    public float periodCD;
    [HideInInspector] public bool running;
    private float currentCD;
    private TMPro.TextMeshProUGUI textComponent;
    private string currentText = "";
    private int index;
    private int textLength;
    private System.Action<object> nextAction;
    private object actionParameter;

    public void Start()
    {
        if (runOnStart)
        {
            Run();
        }
    }

    public void Run(System.Action<object> action = null, object param = null)
    {
        index = 0;
        currentText = "";
        actionParameter = param;
        nextAction = action;
        textLength = text.Length;
        textComponent = GetComponent<TMPro.TextMeshProUGUI>();
        running = true;
    }

    public void Update()
    {
        if (running)
        {
            if (currentCD <= 0)
            {
                if (index == textLength)
                {
                    running = false;
                    if (nextAction != null) nextAction(actionParameter);
                    return;
                }
                textComponent.text = currentText;

                if (text[index] == ' ')
                {
                    currentCD = spaceCD;
                }
                else if (text[index] == '.' || text[index] == ',' || text[index] == '!' || text[index] == '?')
                {
                    currentCD = periodCD;
                }
                else
                {
                    currentCD = characterCD;
                }

                currentText += text[index++];
            }
            else
            {
                currentCD -= Time.deltaTime;
            }
        }
    }
}
