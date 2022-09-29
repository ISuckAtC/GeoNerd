using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

    [SerializeField] private GameObject CurrentLandmark;
    
    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RuneTestBackup")
        {
            if (PlayerData.instance.SpawnPosition() != null)
                transform.position = PlayerData.instance.SpawnPosition();
            else
                transform.position = new Vector3(460.8f, 13.1f, 752.1f);    // Cabin position
        }
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
            if (turnSpeedMultiplier > 0f)
                turnSpeedMultiplier -= turnAcceleration * 2f * Time.deltaTime;
            else if (turnSpeedMultiplier >= -1f)
                turnSpeedMultiplier -= turnAcceleration * Time.deltaTime;
            else
                turnSpeedMultiplier = -1f;
            
            transform.Rotate(Vector3.up, turnSpeed * (currentSpeed * turnSpeedMultiplier) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (turnSpeedMultiplier < 0f)
                turnSpeedMultiplier += turnAcceleration * 2f * Time.deltaTime;
            else if (turnSpeedMultiplier <= 1f)
                turnSpeedMultiplier += turnAcceleration * Time.deltaTime;
            else
                turnSpeedMultiplier = 1f;
            
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
        if (!Input.GetKey(KeyCode.A) && (!Input.GetKey(KeyCode.D)))
        {
            if (turnSpeedMultiplier > 0.1f)
                turnSpeedMultiplier -= turnAcceleration * 0.5f * Time.deltaTime;
            else if (turnSpeedMultiplier < -0.1f)
                turnSpeedMultiplier += turnAcceleration * 0.5f * Time.deltaTime;
            else
                turnSpeedMultiplier = 0f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            Interact();
        }
        
        averageFrameTime.Add(Time.deltaTime);
        
        if (averageFrameTime.Count > averageStackLength) averageFrameTime.RemoveAt(0);
        
        Move(currentSpeed);
    }

    void Interact()
    {
        if (CurrentLandmark != null)
        {
            CurrentLandmark.GetComponent<Landmark>().Use();
        }
    }
    
    void Move(float speed)
    {
        float averageFrame = averageFrameTime.Sum() / averageStackLength;
        
        //Debug.Log(averageFrame);
        
        transform.position += transform.forward * speed * averageFrame;
    }

    public void SetLandmark(GameObject go)
    {
        CurrentLandmark = go;
    }
    public void RemoveLandmark()
    {
        CurrentLandmark = null;
    }
}
