using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] float rotationVelocity;

    float horizontalInput = 0;
    float verticalInput  = 0;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //dir.Normalize();
        //dir = dir * Time.deltaTime * velocity;
        //transform.position = Vector3.Lerp(transform.position, transform.position + dir, 0.3f);
    }

    private void FixedUpdate()
    {
        //Vector3 dir = new Vector3(horizontalInput * velocity * Time.fixedDeltaTime, 0, verticalInput * velocity * Time.fixedDeltaTime);
        //dir.Normalize();
        //rb.position = Vector3.Lerp(rb.position, rb.position + dir, 0.3f);


        Vector3 targetVelocity = new Vector3(horizontalInput * velocity, 0, verticalInput * velocity);
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, 0.6f);
    }
}
