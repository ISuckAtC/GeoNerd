using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public enum ItemType {MagnifyingGlass, Camera, TNT, NONE }
   public ItemType itemType = ItemType.NONE;
}
