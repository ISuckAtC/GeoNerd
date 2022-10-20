using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour
{
    private Rigidbody rigidbody;
    private GameObject player;
    public float detectionRange;
    public float speed;
    public bool follow;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player && Vector3.Distance(player.transform.position, transform.position) <= detectionRange)
        {
            rigidbody.velocity = ((player.transform.position - transform.position).normalized * speed) * (follow ? 1 : -1);
        }
    }
}
