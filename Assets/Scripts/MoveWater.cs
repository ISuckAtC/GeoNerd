using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWater : MonoBehaviour
{
    public Material mat;
    [Range(0.001f, 0.01f)]
    public float offsetSpeed;
    
    private float offset;
    
    
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        offset += offsetSpeed * Time.fixedDeltaTime;

        mat.mainTextureOffset = new Vector2(offset, 0f);

        if (offset >= 1f)
            offset = 0f;
    }
}
