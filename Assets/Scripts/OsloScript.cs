using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsloScript : MonoBehaviour
{
    public FMODUnity.EventReference cityAmbience;
    public GameObject LibraryMarker, OperaMarker, CastleMarker, reward;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Flags[Flag.OSLO_CASTLEPUZZLECOMPLETE] = true; // TEMP REMOVE

        if (GameManager.Flags[Flag.OSLO_CASTLEPUZZLECOMPLETE] && GameManager.Flags[Flag.OSLO_LIBRARYDONE] && GameManager.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE]) 
        {
            GameManager.Flags[Flag.OSLO_ARROW] = false;
            GameManager.Flags[Flag.OSLO_COMPLETE] = true;
        }

        GameManager.FMODPlayStatic(cityAmbience, Vector3.zero, Vector3.zero);
        if (GameManager.Flags[Flag.OSLO_LIBRARYDONE]) LibraryMarker.SetActive(false);
        if (GameManager.Flags[Flag.OSLO_CASTLEPUZZLECOMPLETE]) CastleMarker.SetActive(false);
        if (GameManager.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE]) OperaMarker.SetActive(false);
        if (GameManager.Flags[Flag.OSLO_COMPLETE]) reward.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcceptTrophy()
    {
        GameManager.Flags[Flag.OSLO_COMPLETE] = false;
    }

    public void Exit(string sceneToLoad)
    {
        if (GameManager.Flags[Flag.OSLO_LIBRARYDONE] && GameManager.Flags[Flag.OSLO_OPERAPUZZLECOMPLETE])
        {
            GameManager.Flags[Flag.OSLO_ARROW] = false;
            if (!GameManager.Flags[Flag.FOREST_ARROW]) GameManager.Flags[Flag.FOREST_ARROW] = true;
        }
        GameManager.Instance.LoadScene(sceneToLoad);
    }
}
