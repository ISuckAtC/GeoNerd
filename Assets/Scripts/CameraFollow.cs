using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
        transform.LookAt(target.position);
    }

    void Update()
    {
        transform.position = target.position + offset;
        transform.LookAt(target.position);

    }
}
