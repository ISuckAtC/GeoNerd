using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        
    }

    public void LogPosition(Vector3 pos)
    {
        PlayerPrefs.SetFloat("PlayerPosX", pos.x);
        PlayerPrefs.SetFloat("PlayerPosY", pos.y);
        PlayerPrefs.SetFloat("PlayerPosZ", pos.z);
    }

    public Vector3 SpawnPosition()
    {
        return new Vector3(PlayerPrefs.GetFloat("PlayerPosX"),PlayerPrefs.GetFloat("PlayerPosY"),PlayerPrefs.GetFloat("PlayerPosZ"));
    }
}
