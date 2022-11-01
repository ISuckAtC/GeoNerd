using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISlideInBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float slideMax;
    public float slideSpeed;
    private Vector3 origin;
    private Vector3 target;
    private bool over;
    public bool moveLeft;

    public void OnPointerEnter(PointerEventData eventData)
    {
        over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        over = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (over && (moveLeft ? transform.position.x > origin.x - slideMax : transform.position.x < origin.x + slideMax))
        {
            transform.position = Vector3.MoveTowards(transform.position, origin + new Vector3(moveLeft ? -slideMax : slideMax, 0f, 0f), slideSpeed * Time.deltaTime);
        }
        if (!over && (moveLeft ? transform.position.x < origin.x : transform.position.x > origin.x))
        {
            transform.position = Vector3.MoveTowards(transform.position, origin, slideSpeed * Time.deltaTime);
        }
    }
}
