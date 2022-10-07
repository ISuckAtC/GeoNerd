using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class CastlePuzzleParts : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public int currentOrder;
    private bool selected;
    [HideInInspector]
    public CastlePuzzle overhead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (overhead.currentlySelected == null)
        {
            overhead.currentlySelected = transform;
            selected = true;
        }
    }
    public void OnPointerUp(PointerEventData e)
    {
        selected = false;
        overhead.currentlySelected = null;

        // ToList seems excessive but array is missing the method in Linq
        int snapSlot = overhead.partSlots.ToList().FindIndex(x => Vector3.Distance(x.position, transform.position) < overhead.snapLeniency);
        if (snapSlot != -1)
        {
            transform.position = overhead.partSlots[snapSlot].position;
            currentOrder = snapSlot;
        }
        else currentOrder = -1;

        overhead.CheckPuzzle();
    }
}
