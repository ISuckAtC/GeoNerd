using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RearCameraFollow : MonoBehaviour
{
    public Transform followPosition;
    public Transform followRotation;

    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followPosition.position, 1.5f * Time.deltaTime);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, followRotation.rotation, 1.5f * Time.deltaTime);
        
    }
}
