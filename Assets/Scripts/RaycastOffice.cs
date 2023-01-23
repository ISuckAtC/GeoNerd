using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastOffice : MonoBehaviour
{
    public Transform camera;
    private CameraFollow cf;

    public GameObject file;
    
    
    void Start()
    {
        cf = camera.gameObject.GetComponent<CameraFollow>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 9999f, 1<<9))
            {
                if (hit.collider.CompareTag("Exit"))
                {
                    cf.LeaveOffice();
                }
                
                
                if (hit.collider.CompareTag("CaseFile"))
                {
                    file.gameObject.SetActive(true);
                }
            }
        }
    }
}
