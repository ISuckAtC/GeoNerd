using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropableItem : MonoBehaviour/*, IDropHandler*/
{
    // Start is called before the first frame update
    public GameObject objectToEnable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

    public void OnDrop(BaseEventData data)
    {
        if(objectToEnable)objectToEnable.SetActive(true);
        Destroy(gameObject);
    }
}
