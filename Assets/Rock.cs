using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] bool isObstacle = false;
    float velocity;
    private void Start()
    {
        velocity = Random.Range(1f, 3f);
    }

    private void Update()
    {
        if(!isObstacle) transform.position = new Vector3(transform.position.x, transform.position.y - velocity * Time.deltaTime, 0);
        else transform.position = new Vector3(transform.position.x + velocity * Time.deltaTime, transform.position.y, 0);
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (!isObstacle)
        {
            if (other.transform.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
            else if (other.GetComponent<CaveMovement>())
            {
                Destroy(gameObject);
                other.GetComponent<CaveMovement>().loseLife();
            }
        }
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Ground"))
    //    {
    //        Destroy(this);
    //    }
    //}
}
