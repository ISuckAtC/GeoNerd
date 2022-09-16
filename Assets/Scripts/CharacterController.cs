using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    public float maxSpeed = 6f;
    public float turnSpeed = 90f;
    public Vector2 direction;

    public float currentSpeed;

    public float turnAcceleration = 5f;

    public float maxReverseSpeed = 3.8f;
    
    public float acceleration = 12f;
    public float stopSpeed = 9f;

    public List<float> averageFrameTime = new List<float>();
    public int averageStackLength;

    private float turnSpeedMultiplier;
    
    void Start()
    {
        turnSpeedMultiplier = 1 / maxSpeed;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration;
            }
            else
            {
                currentSpeed = maxSpeed;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (currentSpeed > -maxReverseSpeed)
            {
                currentSpeed -= acceleration;
            }
            else
            {
                currentSpeed = -maxReverseSpeed;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnSpeed * (currentSpeed * turnSpeedMultiplier) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turnSpeed * (currentSpeed * turnSpeedMultiplier) * Time.deltaTime);
        }

        if (!Input.GetKey(KeyCode.W) && (!Input.GetKey(KeyCode.S)))
        {
            if (currentSpeed > 0.1f)
            {
                currentSpeed -= stopSpeed * Time.deltaTime;
            }
            else if (currentSpeed < -0.1f)
            {
                currentSpeed += stopSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed = 0f;
            }
        }
        
        averageFrameTime.Add(Time.deltaTime);
        
        if (averageFrameTime.Count > averageStackLength) averageFrameTime.RemoveAt(0);
        
        Move(currentSpeed);
    }
    
    void Move(float speed)
    {
        float averageFrame = averageFrameTime.Sum() / averageStackLength;
        
        Debug.Log(averageFrame);
        
        transform.position += transform.forward * speed * averageFrame;
        
        //transform.Translate(transform.forward * Time.deltaTime * speed, Space.Self);
    }
}
