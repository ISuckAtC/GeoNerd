using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] bool isObstacle = false;
    float velocity;
    public FMODUnity.EventReference rockHitSound;
    private void Start()
    {
        velocity = isObstacle ? 2.5f : Random.Range(2f, 4f);
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
                GameManager.FMODPlayStatic(rockHitSound, transform.position, Vector3.zero, 1f, false);
                Destroy(gameObject);
            }
            else if (other.GetComponent<CaveMovement>())
            {
                GameManager.FMODPlayStatic(rockHitSound, transform.position, Vector3.zero, 1f, false);
                Destroy(gameObject);
                other.GetComponent<CaveMovement>().loseLife();
            }
             
        }

        if (other.GetComponent<Troll>())
        {
            Destroy(gameObject);
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
