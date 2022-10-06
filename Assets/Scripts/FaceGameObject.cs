using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceGameObject : MonoBehaviour
{
    public GameObject objToFace;

    public bool isArrow;
    public GameObject[] positions;
    
    
    
    void FixedUpdate()
    {
        transform.LookAt(objToFace.transform.position);

        if (isArrow)
        {
            //if (GameManager.Flags)
            
            objToFace = positions[GameManager.Instance.currentTask];
        }
    }
}
