using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPost : MonoBehaviour
{
    public GameObject textArea;
    public TMPro.TextMeshPro textObject;
    
    void Update()
    {
        textArea.transform.LookAt(Camera.main.transform);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            textArea.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            textArea.SetActive(false);
        }
    }
}
