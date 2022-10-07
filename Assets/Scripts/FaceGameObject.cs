using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceGameObject : MonoBehaviour
{
    public GameObject objToFace;
    
    void Start()
    {
        objToFace = Camera.main.gameObject;
    }
    
    void FixedUpdate()
    {
        transform.LookAt(objToFace.transform.position);
    }
}
