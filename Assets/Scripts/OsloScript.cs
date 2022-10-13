using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsloScript : MonoBehaviour
{
    public GameObject LibraryMarker, OperaMarker, CastleMarker, reward;
    // Start is called before the first frame update
    void Start()
    {
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
            if (!GameManager.Flags[Flag.FOREST_ARROW]) GameManager.Flags[Flag.OFFICE_ARROW] = true;
        }
        GameManager.Instance.LoadScene(sceneToLoad);
    }
}
