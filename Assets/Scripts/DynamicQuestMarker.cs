using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DynamicQuestMarker : MonoBehaviour
{
    //[SerializeField] GameObject questMarker;
    [SerializeField] float punchDuration = 10f;
    [SerializeField] float rotationPeriod = 5f;
    [SerializeField] float height = 1f;
    [SerializeField] int vibration = 0;
    [SerializeField] float elasticity = 0.25f;
    [SerializeField] int rotation = 60;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        InvokeRepeating("Punch", 0, punchDuration);
        InvokeRepeating("Rotate", 0, rotationPeriod);
    }

    // Update is called once per frame
    void Punch()
    {
        //divided by 10 to avoid decimals in inspector
        transform.DOPunchPosition(new Vector3(0, height/10, 0), punchDuration, vibration, elasticity, false); 
    }
    
    void Rotate()
    {
        transform.DORotate(new Vector3(0, rotation, 0), rotationPeriod, RotateMode.Fast);
        rotation = -rotation;
    }
}
