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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //dir.Normalize();
        //dir = dir * Time.deltaTime * velocity;
        //transform.position = Vector3.Lerp(transform.position, transform.position + dir, 0.3f);
    }

    private void FixedUpdate()
    {


        Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput);
        inputVector.Normalize();
        Vector3 targetVelocity = new Vector3(inputVector.x * velocity , rb.velocity.y, inputVector.z * velocity);
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, 0.2f);

        if(horizontalInput != 0 || verticalInput != 0)
            rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(inputVector, Vector3.up), rotationVelocity);


    }
}
