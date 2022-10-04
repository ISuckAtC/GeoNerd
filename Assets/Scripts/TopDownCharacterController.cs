using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    private Vector2 direction;

    public float acceleration;

    public float maxSpeed;
    public float resistance;

    [SerializeField] 
    private Vector2 currentSpeed;
    
    private bool onHitbox;
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private GameObject light;


    void Update()
    {
        if (onHitbox)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                light.SetActive(!light.activeSelf);
                GameManager.Flags[Flag.OSLO_LIBRARYDONE] = true;
            }
        }
        
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Highlight"))
        {
            other.gameObject.GetComponent<ParticleSystem>().Play();
            Debug.Log("Playing");
        }

        if (other.CompareTag("Hitbox"))
        {
            UI.SetActive(true);
            onHitbox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Highlight"))
        {
            other.GetComponent<ParticleSystem>().Stop();
        }
        
        if (other.CompareTag("Hitbox"))
        {
            UI.SetActive(false);
            onHitbox = false;
        }
    }
}
