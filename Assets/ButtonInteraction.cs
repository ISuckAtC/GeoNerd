using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class ButtonInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    float initialScale;
    void Start()
    {
        initialScale = transform.localScale.x;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMouseEnter(BaseEventData data)
    {
        transform.DOScale(initialScale*1.25f, 0.5f);
    }
    public void onMouseLeaves(BaseEventData data)
    {
        transform.DOScale(initialScale * 1f, 0.5f);
    }

}
