using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DirectionalCharacterController : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 desiredDirection;
    private Vector2 speed;
    
    public float accSpeed;
    public float maxSpeed;
    public float rotSpeed;
    [SerializeField] private float currSpeed;

    private Rigidbody rb;

    private Vector3 groundNormal;

    [SerializeField] private GameObject CurrentLandmark;

    private bool moving;
    
    [SerializeField]
    private Transform rotObj;
    [SerializeField]
    private Transform carObj;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currSpeed = 0f;
    }

    private void Update()
    {
        desiredDirection.x = 0;
        desiredDirection.y = 1;
        moving = false;
        
        
        if (Input.GetKey(KeyCode.D))
        {
            desiredDirection.y = -1;
            moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            desiredDirection.x = -1;
            moving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            desiredDirection.y = 1;
            moving = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            desiredDirection.x = 1;
            moving = true;
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            desiredDirection.y = 0;
        }
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            desiredDirection.x = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        

        if (desiredDirection != Vector2.zero) transform.forward = new Vector3(direction.x, 0f, direction.y).normalized;

        Move();
    }
    
    
    void Interact()
    {
        if (CurrentLandmark != null)
        {
            CurrentLandmark.GetComponent<Landmark>().Use();
        }
    }
    
    public void SetLandmark(GameObject go)
    {
        CurrentLandmark = go;
    }

    private void Move()
    {
        if (moving)
        {
            currSpeed += accSpeed * Time.deltaTime;
        }
        else
        {
            currSpeed -= accSpeed * 2f * Time.deltaTime;
        }
        currSpeed = Mathf.Clamp(currSpeed, 0f, maxSpeed);
        
        direction = Vector2.Lerp(direction, desiredDirection, rotSpeed * Time.deltaTime);

        if (Mathf.Abs(direction.x) > 0.2f || Mathf.Abs(direction.y) > 0.2f)
            direction.Normalize();

        transform.position += (new Vector3(direction.x, 0f, direction.y) * currSpeed * Time.deltaTime);
        
        
        RaycastHit hitInfo;
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 30f, transform.position.z), Vector3.down);
        if (Physics.Raycast(ray, out hitInfo, 60f, 1 << 3))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);

            groundNormal = hitInfo.normal;
        }
        else
        {
            UnityEngine.Debug.LogWarning("Character Controller can't find ground.");
        }
        
        
        LerpToNormal();

    }
    
    public void RemoveLandmark()
    {
        CurrentLandmark = null;
    }
    
    private void LerpToNormal()
    {
        Quaternion tempRotation = Quaternion.FromToRotation(rotObj.transform.up, groundNormal) * rotObj.transform.rotation;

        rotObj.transform.eulerAngles = new Vector3(tempRotation.eulerAngles.x, rotObj.transform.eulerAngles.y, tempRotation.eulerAngles.z);

        carObj.transform.rotation = Quaternion.Slerp(carObj.transform.rotation, rotObj.transform.rotation, 4f * Time.deltaTime);
    }
}