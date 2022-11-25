using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCar : MonoBehaviour
{
    public List<CarTravelPoints> travelPoints;
    public float speed;
    public float startDelay;
    public float hitForce;
    private int index;
    private Vector3 startPosition;
    private bool stopped = true;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Invoke(nameof(DelayedStart), startDelay);
    }

    void DelayedStart()
    {
        stopped = false;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Hitbox")
        {
            SkinnedMeshRenderer[] r = col.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer rend in r) 
            {
                Material m = Instantiate(rend.material);
                m.color = Color.red;
                rend.material = m;
            }
            col.rigidbody.AddForce((transform.forward + new Vector3(0, 1f, 0)).normalized * hitForce, ForceMode.Force);
            col.rigidbody.AddTorque(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * 4382f, ForceMode.VelocityChange);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped) return;
        float distance = Vector3.Distance(travelPoints[index].transform.position, transform.position);
        float moveDistance = speed * Time.deltaTime;
        while (moveDistance >= distance)
        {
            float actualMove = moveDistance - distance;
            moveDistance -= actualMove;

            transform.position = Vector3.MoveTowards(transform.position, travelPoints[index].transform.position, actualMove);
            index++;
            if (index == travelPoints.Count) 
            {
                //Destroy(gameObject);
                index = 0;
                transform.position = startPosition;
                return;
            }
            distance = Vector3.Distance(travelPoints[index].transform.position, transform.position);
        }
        transform.position = Vector3.MoveTowards(transform.position, travelPoints[index].transform.position, moveDistance);
        transform.forward = (travelPoints[index].transform.position - transform.position).normalized;
    }
}
