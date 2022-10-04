using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "Missions")]
public class Mission : ScriptableObject
{
    public int currentTask;
    public bool[] tasks;
    public Vector3[] waypoints;
    
    

}
