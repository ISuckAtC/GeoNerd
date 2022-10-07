using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
        
    private bool onHitbox;
    private bool onExit;
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private GameObject light;

    public GameObject cameraImage;
    public GameObject librarianHappy;
    public GameObject librarianUnhappy;
    public GameObject exitPrompt;
    public GameObject painting1;
    public GameObject painting2;

    
    
    void Update()
    {
        if (onHitbox)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                light.SetActive(!light.activeSelf);
                GameManager.Flags[Flag.OSLO_LIBRARYDONE] = light.activeSelf;
            }
        }

        if (onExit)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.LoadScene("Oslo");
            }
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Highlight"))
        {
            other.gameObject.GetComponent<ParticleSystem>().Play();
            Debug.Log("Playing");
        }

        if (other.CompareTag("Hitbox"))
        {
            UI.SetActive(true);
            onHitbox = true;
        }

        if (other.CompareTag("Librarian"))
        {
            if (light.activeSelf)
            {
                // Light on - mission complete
                librarianHappy.SetActive(true);
            }
            else
            {
                // Light off - mission incomplete
                librarianUnhappy.SetActive(true);
            }
        }

        if (other.CompareTag("Painting"))
        {
            if (other.gameObject.name == "Deich1")
            {
                painting1.SetActive(true);
            }
            else if (other.gameObject.name == "Deich2")
            {
                painting2.SetActive(true);
            }
        }
        
        if (other.CompareTag("Exit"))
        {
            onExit = true;
            exitPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Highlight"))
        {
            other.GetComponent<ParticleSystem>().Stop();
        }
        
        if (other.CompareTag("Hitbox"))
        {
            UI.SetActive(false);
            onHitbox = false;
        }
        
        if (other.CompareTag("Librarian"))
        {
            if (light.activeSelf)
            {
                // Light on - mission complete
                librarianHappy.SetActive(false);
            }
            else
            {
                // Light off - mission incomplete
                librarianUnhappy.SetActive(false);
            }
        }
        
        if (other.CompareTag("Painting"))
        {
            if (other.gameObject.name == "Deich1")
            {
                painting1.SetActive(false);
            }
            else if (other.gameObject.name == "Deich2")
            {
                painting2.SetActive(false);
            }
        }
        
        if (other.CompareTag("Exit"))
        {
            onExit = false;
            exitPrompt.SetActive(false);
        }
    }
}
