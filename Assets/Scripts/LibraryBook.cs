using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class LibraryBook : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public int currentOrder = -1;
    private bool selected;
    [HideInInspector]
    public NewLibraryPuzzle overhead;
    private Vector3 flatAngle;
    public Vector3 pickupAngle;
    public Sprite frontSprite;
    public Sprite backSprite;
    public float floorLevel;

    public UnityEngine.UI.Image glow;

    private UnityEngine.UI.Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        flatAngle = transform.rotation.eulerAngles;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if (currentOrder == -1) return;
        glow.transform.position = transform.position + new Vector3(-3f, 0f, 0f);
        Vector2 sizeD = GetComponent<RectTransform>().sizeDelta;
        sizeD.x *= 1.5f;
        sizeD.y *= 1.1f;
        glow.rectTransform.sizeDelta = sizeD;
        glow.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData e)
    {
        glow.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (overhead.currentlySelected == null)
        {
            overhead.currentlySelected = transform;
            transform.SetAsLastSibling();
            selected = true;
            image.sprite = frontSprite;
            image.SetNativeSize();
            transform.localRotation = Quaternion.Euler(pickupAngle.x, pickupAngle.y, pickupAngle.z);
        }
    }
    public void OnPointerUp(PointerEventData e)
    {
        selected = false;
        overhead.currentlySelected = null;

        // ToList seems excessive but array is missing the method in Linq

        List<Transform> sorted = overhead.wordSlots.ToList();

        // sort list by distance to puzzle piece, closest slot is first index
        sorted.Sort((a,b) => Vector3.Distance(a.position, transform.position) > Vector3.Distance(b.position, transform.position) ? 1 : -1);


        // if closest slot is close enough to snap, snap it in place and assign the order
        if (sorted.Count > 0 && Vector3.Distance(sorted[0].position, transform.position) < overhead.snapLeniency)
        {
            transform.position = sorted[0].position;
            currentOrder = overhead.wordSlots.ToList().IndexOf(sorted[0]);
            image.sprite = backSprite;
            image.SetNativeSize();
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else 
        {
            image.sprite = frontSprite;
            image.SetNativeSize();
            transform.localRotation = Quaternion.Euler(flatAngle.x, flatAngle.y, flatAngle.z);
            if (transform.localPosition.y > floorLevel) transform.localPosition = new Vector3(transform.localPosition.x, floorLevel, transform.localPosition.z);
            currentOrder = -1;
        }

        overhead.CheckPuzzle();
    }
}
