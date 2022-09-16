using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanMovement : MonoBehaviour
{
    [SerializeField]
    float velocity = 1f;
    Rigidbody rb = null;

    float m_MovementInputValue = 0;
    float m_TurnInputValue;


   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_MovementInputValue  = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");
    }


    private void FixedUpdate()
    {

        //rb.AddForce(transform.forward * Input.GetAxis("Vertical") * velocity * Time.fixedDeltaTime);
        rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime);
    }

}



//Get input for both axes
//if the van is stopped cant rotate
//calculate first which diretion is going to tacke and move ther, rotate the car towards the direction is now facing