using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject notebookMenu;
    void Start()
    {
        //notebookMenu.SetActive(!notebookMenu.activeSelf);
        notebookMenu.SetActive(false);
        //Invoke("DisableNotebook", 6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            notebookMenu.SetActive(!notebookMenu.activeSelf);
        }
    }

    private void DisableNotebook()
    {
        notebookMenu.SetActive(false);
    }
    public void CloseNotebook()
    {
        if (notebookMenu) notebookMenu.SetActive(false);
    }
}
