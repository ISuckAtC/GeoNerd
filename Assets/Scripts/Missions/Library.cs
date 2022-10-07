using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
        
    private bool onHitbox;
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private GameObject light;

    public GameObject cameraImage;
    public GameObject librarianHappy;
    public GameObject librarianUnhappy;
    public GameObject exitPrompt;

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
        
        if (other.CompareTag("Exit"))
        {
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
                UnityEngine.SceneManagement.SceneManager.LoadScene("NorwayMap");
            }
            else
            {
                // Light off - mission incomplete
                librarianUnhappy.SetActive(false);
            }
        }
        
        if (other.CompareTag("Exit"))
        {
            exitPrompt.SetActive(false);
        }
    }
}
