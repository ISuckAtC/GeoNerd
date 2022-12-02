using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public AnimationCurve curve;

    private float initialTime;
    private float time;

    private const float maxTime = 86400f;

    public Color morningColor;
    public Color dayColor;
    public Color eveningColor;
    public Color nightColor;

    private Color currentColor;

    private Light light;

    [SerializeField]
    private Light[] cabinLights;

    [Range(0,24)]
    public int hour;
    
    private bool lightswitch = true;

    private float timeNormalized;

    public bool testing;
    private bool oldTesting;
    void Start()
    {
        light = GetComponent<Light>();

        GameObject[] temp = GameObject.FindGameObjectsWithTag("CabinLight");

        cabinLights = new Light[temp.Length];

        for (int i = 0; i < temp.Length; i++)
        {
            cabinLights[i] = temp[i].GetComponent<Light>();
        }

        initialTime =  DateTime.Now.Hour * 3600f;
        initialTime += DateTime.Now.Minute * 60f;
        initialTime += DateTime.Now.Second;
        initialTime += DateTime.Now.Millisecond * 0.001f;

        Debug.Log("Time is: " + initialTime);

        time = initialTime;
    }

    void Update()
    {
        timeNormalized = time / maxTime;
        
        if (!testing)
        {
            time += Time.deltaTime;
            
            transform.rotation = Quaternion.Euler(curve.Evaluate(timeNormalized), 79.6f, 0f);
        }
        else
        {
            time = hour * 3600;
        
            transform.rotation = Quaternion.Euler(curve.Evaluate(timeNormalized), 79.6f, 0f);
        }

        if (testing != oldTesting)
        {
            SwitchTesting();
            
        }
    }

    private void SwitchTesting()
    {
        oldTesting = testing;

        if (!testing)
        {
            time =  DateTime.Now.Hour * 3600f;
            time += DateTime.Now.Minute * 60f;
            time += DateTime.Now.Second;
            time += DateTime.Now.Millisecond * 0.001f;
        }
    }
    
    private void FixedUpdate()
    {
        if (time / maxTime < 0.25f)
            currentColor = Color.Lerp(morningColor, dayColor, (timeNormalized) * 4f);
        else if (time / maxTime < 0.5f)
            currentColor = Color.Lerp(dayColor, eveningColor, ((timeNormalized) - 0.25f) * 4f);
        else if (time / maxTime < 0.75f)
            currentColor = Color.Lerp(eveningColor, nightColor, ((timeNormalized) - 0.5f) * 4f);
        else
            currentColor = Color.Lerp(nightColor, morningColor, ((timeNormalized) - 0.75f) * 4f);

        light.color = currentColor;


        if (!lightswitch && timeNormalized is > 0.75f or < 0.25f)
        {
            SwitchLights();
        }
        else if (lightswitch && timeNormalized is > 0.25f and < 0.75f)
        {
            SwitchLights();
        }
    }

    private void SwitchLights()
    {
        lightswitch = !lightswitch;
        
        if (!lightswitch)
            for (int i = 0; i < cabinLights.Length; i++)
            {
                cabinLights[i].intensity = 0f;
            }
        else
        {
            for (int i = 0; i < cabinLights.Length; i++)
            {
                cabinLights[i].intensity = 10f;
            }
        }
    }
}