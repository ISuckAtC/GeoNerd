using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Troll : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<CaveMovement>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }
}
