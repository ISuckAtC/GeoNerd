using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorsManager : MonoBehaviour
{
    [SerializeField] Texture2D[] cursors;

  

    public Texture2D[] GetCursors()
    {
        return cursors;
    }
}
