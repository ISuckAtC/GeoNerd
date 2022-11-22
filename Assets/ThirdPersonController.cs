using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float rotSpd;
    public float moveSpdMax;
    public float moveSpd;
    public float moveAcc;

    private Vector3 movement;

    [SerializeField]
    private Transform cam;

    private bool move = false;
    
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        move = false;

        

        if (Input.GetKey(KeyCode.W))
        {
            movement = transform.forward;
            //transform.forward = cam.forward;

            move = true;
        }


        if (Input.GetKey(KeyCode.S))
        {
            movement = -transform.forward;
            //transform.forward = -cam.forward;
            
            move = true;
        }
            

        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(transform.position, Vector3.up, -rotSpd * Time.deltaTime);
        
            //transform.forward = -cam.right;
            
            //move = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(transform.position, Vector3.up, rotSpd * Time.deltaTime);
        
            //transform.forward = cam.right;
            
            //move = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpdMax *= 2f;
            anim.speed *= 2f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpdMax *= 0.5f;
            anim.speed *= 0.5f;
        }

        //transform.forward = new Vector3(transform.forward.x, 0f, transform.forward.z);

        if (move)
        {
            moveSpd += moveAcc * Time.deltaTime;
        }
        else
        {
            moveSpd -= moveAcc * moveSpdMax * Time.deltaTime;
        }
        moveSpd = Mathf.Clamp(moveSpd, 0f, moveSpdMax);
        
        anim.SetBool("Move", move);
        
        Move();
    }

    private void Move()
    {
        transform.position +=  movement * (moveSpd * Time.deltaTime);
    }
}
