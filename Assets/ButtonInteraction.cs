using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class ButtonInteraction : MonoBehaviour
{
    static ButtonInteraction selectedOne = null;
    // Start is called before the first frame update
    float initialScale;
    float initialPos;
    public int movement;
    public bool initialSelected = false;

    void Start()
    {
        initialScale = transform.localScale.x;
        initialPos = transform.position.x;
        if (initialSelected)
        {
            selectedOne = this;
            transform.position = new Vector3(initialPos + movement, transform.position.y, transform.position.z); ;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void NewOneSelected()
    {
        if (selectedOne != this)
        {
            if(selectedOne)
            selectedOne.transform.DOMoveX(initialPos, 0.5f);
        }
        selectedOne = this;
    }
    public void OnMouseEnterSize(BaseEventData data)
    {
        transform.DOScale(initialScale*1.25f, 0.5f);
    }
    public void OnMouseLeavesSize(BaseEventData data)
    {
        transform.DOScale(initialScale * 1f, 0.5f);
    }

    public void OnMouseEnterHorizontalSlide(BaseEventData data)
    {
        if (selectedOne != this)
        transform.DOMoveX(initialPos + movement, 0.5f);
    }
    public void OnMouseLeavesHorizontalSlide(BaseEventData data)
    {
        if (selectedOne != this)
            transform.DOMoveX(initialPos, 0.5f);
    }

}
