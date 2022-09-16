using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public string tag;
    public GameObject obj;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tag))
            obj.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag(tag))
            obj.SetActive(false);
    }

}
