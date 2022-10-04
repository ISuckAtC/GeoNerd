using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SearchService;
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

    private Vector3 groundNormal;
    public GameObject carObj;
    public GameObject rotationObj;
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
        
        RaycastHit hitInfo;
        Ray ray = new Ray(new Vector3(transform.position.x, 50f, transform.position.z), Vector3.down);
        if (Physics.Raycast(ray, out hitInfo, 60f, 1 << 3))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);

            groundNormal = hitInfo.normal;
        }
        else
        {
            Debug.LogWarning("Character Controller can't find ground.");
        }

        LerpToNormal();
    }

    private void LerpToNormal()
    {
        Quaternion tempRotation = Quaternion.FromToRotation (rotationObj.transform.up, groundNormal) * rotationObj.transform.rotation;

        rotationObj.transform.eulerAngles = new Vector3(tempRotation.eulerAngles.x, rotationObj.transform.eulerAngles.y, tempRotation.eulerAngles.z);

        carObj.transform.rotation = Quaternion.Slerp(carObj.transform.rotation, rotationObj.transform.rotation, 4f * Time.deltaTime);
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
