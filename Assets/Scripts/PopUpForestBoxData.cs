using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopUpForestBoxData
{
    public bool restart;
    public Item.ItemType item;
    public Sprite image;
    [TextArea(3,15)]
    public string text;

}
