using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RearCameraFollow : MonoBehaviour
{
    public Transform followPosition;
    public Transform followRotation;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followPosition.position, 0.5f * Time.deltaTime);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, followRotation.rotation, 0.5f * Time.deltaTime);
        
    }
}
