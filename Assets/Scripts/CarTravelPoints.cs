using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTravelPoints : MonoBehaviour
{
    public bool crossing;
    private int index;
    public void Start()
    {
        index = transform.GetSiblingIndex();
    }
    public void OnTriggerEnter(Collider col)
    {
        if (!crossing) return;
        if (col.TryGetComponent<ThirdPersonController>(out ThirdPersonController a))
        {
            Debug.Log("Cars will stop at index " + index);
            CityCar.stopAt = index;
        }
    }
    public void OnTriggerExit(Collider col)
    {
        if (!crossing) return;
        if (col.TryGetComponent<ThirdPersonController>(out ThirdPersonController a))
        {
            Debug.Log("Cars are no longer stopping");
            CityCar.stopAt = -1;
        }
    }
}
