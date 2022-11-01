using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteCurrentScene : MonoBehaviour
{
    public string textPattern;
    public char patternChar = '$';
    void Awake()
    {
        int patternIndex = textPattern.IndexOf(patternChar);
        string completedString;
        if (patternChar > -1)
        {
            completedString = textPattern.Substring(0, patternIndex) + gameObject.scene.name.ToUpper() + textPattern.Substring(patternIndex + 1);
        } 
        else completedString = gameObject.scene.name.ToUpper();

        TMPro.TextMeshProUGUI tmpgui;
        TMPro.TextMeshPro tmpro;
        UnityEngine.UI.Text text;

        if (TryGetComponent<TMPro.TextMeshProUGUI>(out tmpgui))
        {
            tmpgui.text = completedString;
        }
        if (TryGetComponent<TMPro.TextMeshPro>(out tmpro))
        {
            tmpro.text = completedString;
        }
        if (TryGetComponent<UnityEngine.UI.Text>(out text))
        {
            text.text = completedString;
        }
    }
}
