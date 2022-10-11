using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject objToFace;

    private bool isArrow = true;

    public GameObject office, oslo, forest, trondheim;

    void Awake()
    {
        bool a = GameManager.Instance.enabled;
    }

    void Start()
    {
        
        if (GameManager.Flags[Flag.OFFICE_ARROW])
        {
            objToFace = office;
            return;
        }
        if (GameManager.Flags[Flag.OSLO_ARROW])
        {
            objToFace = oslo;
            return;
        }
        if (GameManager.Flags[Flag.FOREST_ARROW])
        {
            objToFace = forest;
            return;
        }
        if (GameManager.Flags[Flag.TRONDHEIM_ARROW])
        {
            objToFace = trondheim;
            return;
        }
        isArrow = false;
    }
    void Update()
    {
        if (isArrow)
        {
            transform.LookAt(objToFace.transform.position);
        }
    }
}
