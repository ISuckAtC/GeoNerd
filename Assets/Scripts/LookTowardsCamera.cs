using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsCamera : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        //transform.LookAt(2 * transform.position - cam.transform.position);

    }
}
