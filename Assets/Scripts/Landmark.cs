using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Landmark : MonoBehaviour
{
    public string sceneName;

    public bool interactable;
    public bool enterable;

    private Scene scene;
    
    public void Use()
    {
        if (enterable)
            Enter();
        else if (interactable)
            Interact();
    }
    
    private void Interact()
    {
        
    }
    
    private void Enter()
    {
        GameManager.Instance.LoadScene(sceneName);
    }
}
