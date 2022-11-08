using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalMenu : MonoBehaviour
{
    public Image magGlass;
    public Image panasonic;
    public Image tnt;

    public Sprite magGlassAquired;
    public Sprite panasonicAquired;
    public Sprite tntAquired;
    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (GameManager.GameData.Flags[Flag.OSLO_LIBRARYDONE]) magGlass.sprite = magGlassAquired;
        if (GameManager.GameData.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE]) panasonic.sprite = panasonicAquired;
        if (GameManager.GameData.Flags[Flag.OSLO_CASTLEPUZZLECOMPLETE]) tnt.sprite = tntAquired;
    }
}
