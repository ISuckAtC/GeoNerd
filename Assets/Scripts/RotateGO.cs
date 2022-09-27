using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGO : MonoBehaviour
{
    public bool self;
    public Vector3 axis;
    public float speed;
    

    // Update is called once per frame
    void Update()
    {
        if (self)
        {            
            transform.Rotate(axis * speed * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Rotate(axis * speed * Time.deltaTime, Space.World);
        }
    }
}
