using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWhenTriggered : MonoBehaviour
{
    public GameObject objectToEnable;

    public string tagToCheck;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCheck))
        {
            objectToEnable.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagToCheck))
        {
            objectToEnable.SetActive(false);
        }
    }

}
