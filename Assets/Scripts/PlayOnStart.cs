using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnStart : MonoBehaviour
{
    public FMODUnity.EventReference reference;
    public Vector3 velocity;
    public float volume;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.FMODPlayStatic(reference, transform.position, velocity, volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
