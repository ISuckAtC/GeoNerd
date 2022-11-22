using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Unity.Mathematics;

public class OsloFollow : MonoBehaviour
{
    public Transform target;
    private Quaternion initialRotation;
    public Vector3 offset;

    public float rotationSpeed;

    public bool testing;

    private Ray ray;
    
    private void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);

        transform.position = target.position + offset;


        if (Input.GetKeyDown(KeyCode.Mouse0) || testing)
        {
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject hitObject = hit.transform.gameObject;
            
                if (!testing)
                    if (!hit.transform.CompareTag("Hitbox"))
                        hitObject.GetComponent<LoadScene>().Load();
                /*
                if (hit.transform.CompareTag("Exit"))
                {
                    
                }
                if (hit.transform.CompareTag("Opera"))
                {
                    
                }
                if (hit.transform.CompareTag("Library"))
                {
                    
                }
                */
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
    
} 