using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpInWorld : MonoBehaviour
{
    [SerializeField] GameObject popUpObject;
    [SerializeField] GameObject exclamationMark;
    private void Start()
    {
        if(popUpObject)popUpObject.SetActive(false);
        if (exclamationMark) exclamationMark.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player is the one who enters
        if (other.GetComponent<CharacterController>())
        {
            if (popUpObject) popUpObject.SetActive(true);
            if (exclamationMark) exclamationMark.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            if (popUpObject) popUpObject.SetActive(false);
            if (exclamationMark) exclamationMark.SetActive(true);
        }
    }

}
