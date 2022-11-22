using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseAnimationScript : MonoBehaviour
{
    /*[HideInInspector] */public Schmoose parent;

    public void WalkCompleted()
    {
        if (parent.walkingSchmoose.Contains(transform)) parent.walkingSchmoose.Remove(transform);
    }
    
    public void JumpCompleted()
    {
        if (parent.jumpingSchmoose.Contains(transform)) parent.jumpingSchmoose.Remove(transform);
    }
}
