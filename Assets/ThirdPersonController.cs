using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float rotSpd;
    public float moveSpdMax;
    public float moveSpd;
    public float moveAcc;

    private Vector2 movement;

    [SerializeField]
    private Transform cam;

    private bool move = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        move = false;

        if (Input.GetKey(KeyCode.W))
        {
            transform.forward = cam.forward;

            move = true;
        }


        if (Input.GetKey(KeyCode.S))
        {
            transform.forward = -cam.forward;
            
            move = true;
        }
            

        if (Input.GetKey(KeyCode.A))
        {
            transform.forward = -cam.right;
            
            move = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.forward = cam.right;
            
            move = true;
        }

        transform.forward = new Vector3(transform.forward.x, 0f, transform.forward.z);

        if (move)
        {
            moveSpd += moveAcc * Time.deltaTime;
        }
        else
        {
            moveSpd -= moveAcc * Time.deltaTime;
        }
        moveSpd = Mathf.Clamp(moveSpd, 0f, moveSpdMax);
        
        Move();
    }

    private void Move()
    {
        transform.position +=  transform.forward * (moveSpd * Time.deltaTime);
    }
}
