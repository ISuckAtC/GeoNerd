using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceGameObject : MonoBehaviour
{
    private GameObject objToFace;

    private bool isArrow = false;

    public GameObject office, oslo, forest, trondheim;
    
    
    void Start()
    {
        if (GameManager.Flags[Flag.OFFICE_ARROW])
        {
            objToFace = office;
            isArrow = true;
        }
        if (GameManager.Flags[Flag.OSLO_ARROW])
        {
            objToFace = oslo;
            isArrow = true;
        }
        if (GameManager.Flags[Flag.FOREST_ARROW])
        {
            objToFace = forest;
            isArrow = true;
        }
        if (GameManager.Flags[Flag.TRONDHEIM_ARROW])
        {
            objToFace = trondheim;
            isArrow = true;
        }
    }
    void FixedUpdate()
    {
        if (isArrow)
        {
            transform.LookAt(objToFace.transform.position);
        }
    }
}
