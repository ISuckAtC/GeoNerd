using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsloScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit(string sceneToLoad)
    {
        if (GameManager.Flags[Flag.OSLO_LIBRARYDONE] && GameManager.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE])
        {
            GameManager.Flags[Flag.OSLO_ARROW] = false;
            if (!GameManager.Flags[Flag.FOREST_ARROW]) GameManager.Flags[Flag.OFFICE_ARROW] = true;
        }
        GameManager.Instance.LoadScene(sceneToLoad);
    }
}
