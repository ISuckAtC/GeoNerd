using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpInWorld : MonoBehaviour
{
    [SerializeField] GameObject popUpObject;
    [SerializeField] GameObject exclamationMark;
    public Flag questMarkerFlag;

    private bool exclamationMarkActive;

    private CharacterController cc;
    
    private void Start()
    {
        if(popUpObject) popUpObject.SetActive(false);
        if (exclamationMark) 
        {
            exclamationMarkActive = GameManager.Flags[questMarkerFlag];
            exclamationMark.SetActive(exclamationMarkActive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player is the one who enters
        if (cc = other.GetComponent<CharacterController>())
        {
            if (popUpObject) popUpObject.SetActive(true);
            if (exclamationMark) exclamationMark.SetActive(false);
            
            cc.SetLandmark(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            if (popUpObject) popUpObject.SetActive(false);
            if (exclamationMark) exclamationMark.SetActive(exclamationMarkActive);
            
            cc.RemoveLandmark();
            cc = null;
        }
    }

}
