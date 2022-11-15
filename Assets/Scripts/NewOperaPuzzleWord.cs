using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class NewOperaPuzzleWord : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public int correctOrder;
    private bool selected;
    [HideInInspector]
    public NewOpera overhead;
    public NewOperaPuzzleWord over;
    public NewOperaPuzzleWord under;
    
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
            if (over)
            {
                over.under = null;
                over = null;
            }
            if (under)
            {
                under.over = null;
                under = null;
            }
        }
    }
    public void OnPointerUp(PointerEventData e)
    {
        selected = false;
        overhead.currentlySelected = null;

        // ToList seems excessive but array is missing the method in Linq

        List<NewOperaPuzzleWord> sorted = overhead.words.ToList();
        sorted.Remove(this);

        // sort list by distance to puzzle piece, closest word is first index
        sorted.Sort((a,b) => Vector3.Distance(a.transform.position, transform.position) > Vector3.Distance(b.transform.position, transform.position) ? 1 : -1);

        //foreach (NewOperaPuzzleWord word in sorted) Debug.Log(Vector3.Distance(word.transform.position, transform.position));

        // if closest word is close enough to snap, snap it in place and assign the order
        if (sorted.Count > 0 && Vector3.Distance(sorted[0].transform.position, transform.position) < overhead.snapLeniency)
        {
            if (sorted[0].transform.position.y > transform.position.y)
            {
                if (sorted[0].under == null)
                {
                    sorted[0].under = this;
                    over = sorted[0];
                    transform.position = sorted[0].transform.position + new Vector3(0, -overhead.connectionGap, 0);
                }
            }
            else
            {
                if (sorted[0].over == null)
                {
                    sorted[0].over = this;
                    under = sorted[0];
                    transform.position = sorted[0].transform.position + new Vector3(0, overhead.connectionGap, 0);
                }
            }
            
        }

        overhead.CheckPuzzle();
    }
}
