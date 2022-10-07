using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceGameObject : MonoBehaviour
{
    public GameObject objToFace;
    
    
    
    void FixedUpdate()
    {
        transform.LookAt(objToFace.transform.position);
    }
}
