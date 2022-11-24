using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObjectByVector : MonoBehaviour
{
    public GameObject objToFace;

    public bool forwardDirection;
    public bool rightDirection;
    public bool upDirection;
    
    
    void Start()
    {
        //objToFace = Camera.main.gameObject;
    }
    
    void FixedUpdate()
    {
        if (forwardDirection)
        {
            transform.LookAt(objToFace.transform.position, Vector3.up);
        }
        else if (rightDirection)
        {
            transform.LookAt(objToFace.transform.position, Vector3.forward);
        }
        else if (upDirection)
        {
            transform.LookAt(objToFace.transform.position, Vector3.right);
        }
        
    }
}
