using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class UISlideItemBar : UISlideInBar
{

    public float slideMin;
    private bool selected = false;
    public Transform objectToMove;
    void Start()
    {
        origin = objectToMove.position;
    }
    override public void OnPointerEnter(PointerEventData eventData)
    {
        if(!selected)
            objectToMove.DOMoveX(origin.x + (moveLeft ? -slideMin : slideMin), 0.5f);
        else
            objectToMove.DOMoveX(origin.x + (moveLeft ? -(slideMin + slideMax) : (slideMin + slideMax)), 0.5f);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
            objectToMove.DOMoveX(origin.x, 0.5f);
        else
            objectToMove.DOMoveX(origin.x + (moveLeft ? -(slideMax) : (slideMax)), 0.5f);
    }

    public void Select()
    {
        if (!selected)
        {
            objectToMove.DOMoveX(origin.x + (moveLeft ? -slideMax : slideMax), 0.5f);
        }
        else
        {
            objectToMove.DOMoveX(origin.x, 0.5f);
        }
        selected = !selected;
    }

    

}
