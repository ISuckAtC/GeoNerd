using System.Collections;
using System.Collections.Generic;
using UnityEngine;




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

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraFollow : MonoBehaviour
//{
//    public Transform target;
//    public Vector3 offset;

//    private void Start()
//    {
//        transform.LookAt(target.position);
//    }

//    void Update()
//    {
//        transform.position = target.position + offset;
//        transform.LookAt(target.position);

//    }
//}
