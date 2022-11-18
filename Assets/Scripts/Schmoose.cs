using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Schmoose : MonoBehaviour
{
    public string eatAnimationName;
    public string walkAnimationName;
    public string jumpAnimationName;
    
    public Animator[] schmeese;
    private string[] actions;

    void Awake()
    {
            actions = new [] { eatAnimationName, eatAnimationName, eatAnimationName, eatAnimationName, walkAnimationName, jumpAnimationName };
    }
    void Start()
    {
        InvokeRepeating("PlayRandomAnimation", 1f, 1f);
    }

    void PlayRandomAnimation()
    {
        int youInParticular = Random.Range(0, schmeese.Length);
        int action = Random.Range(0, 6);

        Animator current = schmeese[youInParticular];
        
        if (action > 3)
            current.gameObject.transform.eulerAngles = new Vector3(0f, Random.Range(0f,360f), 0f);
        else
            current.gameObject.transform.eulerAngles = new Vector3(0f, Random.Range(current.gameObject.transform.eulerAngles.y - 30f, current.gameObject.transform.eulerAngles.y + 30f), 0f);
        
        current.Play(actions[action]);
    }
}
