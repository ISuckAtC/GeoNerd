using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UISlideInBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float slideMax;
    public float slideSpeed;
    protected Vector3 origin;
    private Vector3 target;
    private bool over;
    public bool moveLeft;

    virtual public void OnPointerEnter(PointerEventData eventData)
    {
        over = true;
        transform.DOMoveX(origin.x + (moveLeft ? -slideMax : slideMax), 0.5f);
    }

    virtual public void OnPointerExit(PointerEventData eventData)
    {
        over = false;
        transform.DOMoveX(origin.x, 0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (over && (moveLeft ? transform.position.x > origin.x - slideMax : transform.position.x < origin.x + slideMax))
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, origin + new Vector3(moveLeft ? -slideMax : slideMax, 0f, 0f), slideSpeed * Time.deltaTime);
    //    }
    //    if (!over && (moveLeft ? transform.position.x < origin.x : transform.position.x > origin.x))
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, origin, slideSpeed * Time.deltaTime);
    //    }
    //}
}
