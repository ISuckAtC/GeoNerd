using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpInWorld : MonoBehaviour
{
    Canvas popUpCanvas = null;
    private void Start()
    {
        popUpCanvas = GetComponentInChildren<Canvas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player is the one who enters
        if (other.GetComponent<CharacterController>())
        {
            Debug.Log("car entered");
            popUpCanvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            popUpCanvas.enabled = false;
        }
    }

}
