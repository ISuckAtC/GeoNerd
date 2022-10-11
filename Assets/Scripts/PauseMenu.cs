using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool paused = false;

    [SerializeField] private GameObject menu;


    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0f;
            menu.SetActive(true);
            
        }
        else
        {
            paused = false;
            Time.timeScale = 1f;
            menu.SetActive(false);
            
        }
    }

    private void FixedUpdate()
    {
        if (paused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
