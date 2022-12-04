using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] Vector2 velocity;

    

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3)velocity * Time.deltaTime;
    }
}
