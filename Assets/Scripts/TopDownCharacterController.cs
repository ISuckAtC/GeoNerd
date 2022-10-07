using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    private Vector2 direction;

    [Range(0f, 20f)]
    public float acceleration;
    [Range(0f, 10f)]
    public float maxSpeed;
    [Range(1f, 1.1f)]
    public float resistance;

    [SerializeField] 
    private Vector2 currentSpeed;

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();
        
        if (direction.x != 0)
        {
            if ((direction.x > 0 && currentSpeed.x < 0) || (direction.x < 0 && currentSpeed.x > 0))
            {
                direction.x *= resistance;
            }

            currentSpeed.x += direction.x * acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed.x /= resistance;
        }
        
        if (direction.y != 0)
        {
            if ((direction.y > 0 && currentSpeed.y < 0) || (direction.y < 0 && currentSpeed.y > 0))
            {
                direction.y *= resistance;
            }

            currentSpeed.y += direction.y * acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed.y /= resistance;
        }

        if (currentSpeed.magnitude > maxSpeed) currentSpeed = currentSpeed.normalized * maxSpeed;

        transform.position += new Vector3(currentSpeed.x * Time.deltaTime, 0, currentSpeed.y * Time.deltaTime);

        if (currentSpeed.magnitude > 0.1f)
        {
            Vector2 lookDirection = currentSpeed.normalized;

            transform.forward = new Vector3(lookDirection.x, 0f, lookDirection.y);
        }
            
    }

}
