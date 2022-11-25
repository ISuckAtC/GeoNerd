using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class CastlePuzzleParts : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //[HideInInspector]
    public int currentOrder = -1;
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

        List<Transform> sorted = overhead.partSlots.ToList();

        // sort list by distance to puzzle piece, closest slot is first index
        sorted.Sort((a,b) => Vector3.Distance(a.position, transform.position) > Vector3.Distance(b.position, transform.position) ? 1 : -1);


        // if closest slot is close enough to snap, snap it in place and assign the order
        if (sorted.Count > 0 && Vector3.Distance(sorted[0].position, transform.position) < overhead.snapLeniency)
        {
            GameManager.FMODPlayStatic(overhead.klink, transform.position, Vector3.zero);
            transform.position = sorted[0].position;
            currentOrder = overhead.partSlots.ToList().IndexOf(sorted[0]);
        }
        else currentOrder = -1;

        overhead.CheckPuzzle();
    }
}
