using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagRaiser : MonoBehaviour
{
    [SerializeField] private bool raiseForestFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        if (raiseForestFlag)
        {
            GameManager.GameData.Flags[Flag.OSLO_ARROW] = false;
            GameManager.GameData.Flags[Flag.FOREST_ARROW] = true;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
