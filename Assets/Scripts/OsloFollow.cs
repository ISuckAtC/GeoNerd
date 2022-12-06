using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Unity.Mathematics;

public class OsloFollow : MonoBehaviour
{
    public Transform target;
    private Quaternion initialRotation;
    public Vector3 offset;

    public float rotationSpeed;

    public bool testing;

    private Ray ray;

    public float hitRange;

    public GameObject enterObject;
    private TextMeshPro enterText;

    private void Start()
    {
        initialRotation = transform.rotation;

        enterText = enterObject.GetComponent<TextMeshPro>();
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);

        transform.position = target.position + offset;

        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, hitRange))
        {
            enterObject.transform.position = hit.point;
            
            if (hit.transform.CompareTag("Enterable"))
            {
                string sceneName = hit.transform.GetComponent<LoadScene>().sceneName;
                
                switch (sceneName)
                {
                    case "Opera":
                        {
                            enterText.text = "Enter Opera";

                            break;
                        }
                    case "Library":
                        {
                            enterText.text = "Enter Library";

                            break;
                        }
                    case "Castle":
                        {
                            enterText.text = "Enter Castle";

                            break;
                        }
                    default:
                        {
                            enterText.text = "";
                            break;
                        }
                }
            }
            else
            {
                enterText.text = "";
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || testing)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (Vector3.Distance(transform.position, hit.point) < hitRange)
                {
                    GameObject hitObject = hit.transform.gameObject;

                    if (!testing)
                        if (hit.transform.CompareTag("Enterable"))
                            hitObject.GetComponent<LoadScene>().Load();
                }

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