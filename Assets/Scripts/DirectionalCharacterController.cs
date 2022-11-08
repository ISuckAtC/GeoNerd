using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using FMOD;
using FMOD.Studio;

public class DirectionalCharacterController : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 desiredDirection;

    private Vector2 speed;

    public float accSpeed;
    public float maxSpeed;
    public float rotSpeed;
    

    private void Start()
    {
        
    }

    private void Update()
    {
        desiredDirection.x = 0;
        desiredDirection.y = 0;
        
        if (Input.GetKey(KeyCode.D))
        {
            desiredDirection.y = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            desiredDirection.x = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            desiredDirection.y = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            desiredDirection.x = 1;
        }


        Move();
    }

    private void Move()
    {
        transform.position += (new Vector3(desiredDirection.x, 0f, desiredDirection.y) * maxSpeed * Time.deltaTime).normalized;
    }
}





