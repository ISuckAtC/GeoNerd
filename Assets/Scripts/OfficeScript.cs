using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeScript : MonoBehaviour
{
    [SerializeField] private bool spotlight; //Is spotlight object active

    [SerializeField] private bool folder = true; //Is case file object active

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void LightToggle() //used in the Canvas button for the lightswitch
    {
        spotlight = !spotlight;
        gameObject.SetActive(spotlight);
        
    }

    public void FolderOff() //used in the Canvas button for the folder (very temp)
    {
        folder = false;
        gameObject.SetActive(folder);

    }
}
