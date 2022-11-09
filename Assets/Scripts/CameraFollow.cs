using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class CameraFollow : MonoBehaviour
{

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	private void Start()
    {
        transform.LookAt(target.position);
    }


    void Update()
	{
		if (!target) return;


		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = new Vector3(transform.position.x, transform.position.y, smoothedPosition.z);
	}

}
*/
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Quaternion initialRotation;
    public Vector3 offset;

    public bool targetOffice = false;
    public Transform officeTransform;

    private bool moveCamera;

    private CharacterController cc;
    

    private void Start()
    {
        initialRotation = transform.rotation;

        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        //transform.LookAt(target.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            moveCamera = true;

            targetOffice = !targetOffice;
            
            cc.ToggleOffice(targetOffice);
            
            //MoveToOffice();
        }

        if (moveCamera)
        {
            if (targetOffice)
            {
                transform.position = Vector3.Lerp(transform.position, officeTransform.position, 5f * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, officeTransform.rotation, 5f * Time.deltaTime);


                if (Vector3.Distance(transform.position, officeTransform.position) < 0.5f)
                {
                    transform.position = officeTransform.position;
                    moveCamera = false;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, target.position + offset, 5f * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, 5f * Time.deltaTime);

                if (Vector3.Distance(transform.position, target.position + offset) < 0.5f)
                {
                    transform.position = target.position + offset;
                    moveCamera = false;
                }
            }
        }


        if (!targetOffice && !moveCamera)
            transform.position = target.position + offset;


        //transform.LookAt(target.position);
    }

    public bool CheckOfficeToggled()
    {
        return targetOffice;
    }


    public void ToggleOffice()
    {
        moveCamera = true;
        targetOffice = !targetOffice;
        /*
        if (targetOffice)
        {
            targetOffice = false;
            //transform.rotation = initialRotation;
        }
        else
        {
            targetOffice = true;
            //transform.position = officeTransform.position;
            //transform.rotation = officeTransform.rotation;
        }
        */
    }
}