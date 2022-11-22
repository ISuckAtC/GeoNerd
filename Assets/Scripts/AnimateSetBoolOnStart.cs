using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSetBoolOnStart : MonoBehaviour
{
    public string boolToSet;
    public bool value;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool(boolToSet, value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
