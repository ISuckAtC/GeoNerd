using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomAnimValue : MonoBehaviour
{

    [SerializeField] string[] parameters;
    [SerializeField] FloatPair[] values;


    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (!anim)
            Debug.Log("There is no animator attached to this GameObject");
        else if (parameters.Length != values.Length)
            Debug.Log("The number of parameters and values is not matching");


        for(int x = 0; x < parameters.Length; x++)
        {
            anim.SetFloat(parameters[x], UnityEngine.Random.Range(values[x].first, values[x].second));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
