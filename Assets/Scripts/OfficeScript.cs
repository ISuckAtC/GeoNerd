using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeScript : MonoBehaviour
{
    [SerializeField] private bool spotlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightToggle()
    {
        spotlight = !spotlight;
        gameObject.SetActive(spotlight);
        
    }
}
