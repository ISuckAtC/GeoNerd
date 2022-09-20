using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//using DG.Tweening;
public class MenuManager : MonoBehaviour
{




    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject audioSettingsPanel;


    public GameObject upperMargin;
    public GameObject centerPoint;
    public GameObject bottomMargin;
    GameObject currPanel;
    Stack<GameObject> backList;

    void Start()
    {
        currPanel = menuPanel;
        backList = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadPanel(string panelToLoad)
    {
        
        switch (panelToLoad)
        {
            case "Menu":
                if (currPanel == menuPanel) break;
                backList.Push(currPanel);
                currPanel.transform.DOMoveY(bottomMargin.transform.position.y, 1);
                menuPanel.transform.DOMoveY(centerPoint.transform.position.y, 1);
                backList.Push(currPanel);
                currPanel = menuPanel;
                break;
            case "Settings":
                if (currPanel == settingsPanel) break;
                backList.Push(currPanel);
                currPanel.transform.DOMoveY(bottomMargin.transform.position.y, 1);
                settingsPanel.transform.DOMoveY(centerPoint.transform.position.y, 1);
                backList.Push(currPanel);
                currPanel = settingsPanel;
                break;
        }
    }
    

    public void GoBack()
    {
        if(backList.Count > 0)
        {
            currPanel.transform.DOMoveY(upperMargin.transform.position.y, 1);
            backList.Peek().transform.DOMoveY(centerPoint.transform.position.y, 1);

            currPanel = backList.Pop();

        }
    }
}
