using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;

public class WorldPoster : MonoBehaviour
{
    [Header("First word should be empty (this is the space), do not make this list over 256 entries long")]
    public List<string> words;
    public TMP_Dropdown[] dropdowns;
    private CharacterController player;
    public GameObject postUI;
    public void Start()
    {
        player = GetComponent<CharacterController>();
        for (int i = 0; i < dropdowns.Length; ++i)
        {
            dropdowns[i].ClearOptions();
            dropdowns[i].AddOptions(words);
        }
    }

    public void Post()
    {
        byte[] post = new byte[20];
        BitConverter.GetBytes(player.transform.position.x).CopyTo(post, 0);
        BitConverter.GetBytes(player.transform.position.y).CopyTo(post, 4);
        BitConverter.GetBytes(player.transform.position.z).CopyTo(post, 8);
        for (int i = 0; i < dropdowns.Length; ++i) post[12 + i] = (byte)dropdowns[i].value;
        Network.PostWorldMessage(post);
        Time.timeScale = 1f;
        postUI.SetActive(false);
    }
    public void Cancel()
    {
        Time.timeScale = 1f;
        postUI.SetActive(false);
    }
}
