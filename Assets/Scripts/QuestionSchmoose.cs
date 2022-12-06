using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionSchmoose : MonoBehaviour
{
    private GameObject dialogue;

    public Dictionary<Flag, string> facts = new Dictionary<Flag, string>();

    [TextArea(3,8)]
    public List<string> allFacts = new List<string>();
    //private List<string> facts = new List<string>();

    public SpeechBubble speechBubble;

    public bool ready = true;

    public FaceObjectByVector face;
    
    void Start()
    {
        //speechBubble = GetComponent<SpeechBubble>();
        
        CheckAllStrings();
    }

    private void CheckAllStrings()
    {
        if (!GameManager.Flags[Flag.SCHMOOSE_Q1])  
            facts.Add(Flag.SCHMOOSE_Q1, allFacts[0]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q2])  
            facts.Add(Flag.SCHMOOSE_Q2, allFacts[1]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q3])  
            facts.Add(Flag.SCHMOOSE_Q3, allFacts[2]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q4])  
            facts.Add(Flag.SCHMOOSE_Q4, allFacts[3]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q5])  
            facts.Add(Flag.SCHMOOSE_Q5, allFacts[4]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q6])  
            facts.Add(Flag.SCHMOOSE_Q6, allFacts[5]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q7])  
            facts.Add(Flag.SCHMOOSE_Q7, allFacts[6]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q8])  
            facts.Add(Flag.SCHMOOSE_Q8, allFacts[7]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q9])  
            facts.Add(Flag.SCHMOOSE_Q9, allFacts[8]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q10])  
            facts.Add(Flag.SCHMOOSE_Q10, allFacts[9]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q11])  
            facts.Add(Flag.SCHMOOSE_Q11, allFacts[10]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q12])  
            facts.Add(Flag.SCHMOOSE_Q12, allFacts[11]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q13])  
            facts.Add(Flag.SCHMOOSE_Q13, allFacts[12]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q14])  
            facts.Add(Flag.SCHMOOSE_Q14, allFacts[13]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q15])  
            facts.Add(Flag.SCHMOOSE_Q15, allFacts[14]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q16])  
            facts.Add(Flag.SCHMOOSE_Q16, allFacts[15]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q17])  
            facts.Add(Flag.SCHMOOSE_Q17, allFacts[16]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q18])  
            facts.Add(Flag.SCHMOOSE_Q18, allFacts[17]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q19])  
            facts.Add(Flag.SCHMOOSE_Q19, allFacts[18]);
        if (!GameManager.Flags[Flag.SCHMOOSE_Q20])  
            facts.Add(Flag.SCHMOOSE_Q20, allFacts[19]);
    }
    
    private void ChanceToSetActive(float chance, int fact = -1)
    {
        if (Random.Range(0f, 1f) > chance)
        {
            ready = false;
            speechBubble.transform.parent.gameObject.SetActive(true);

            if (fact == -1)
            {
                speechBubble.text = "You have exhausted my knowledge.";
            }
            else
            {
                KeyValuePair<Flag, string> pair = facts.ElementAt(fact);
                speechBubble.text = pair.Value;
                
                GameManager.Flags[pair.Key] = true;
                facts.Remove(pair.Key);
                
                speechBubble.Run(((object schmoose) =>
                {
                    
                    ((QuestionSchmoose)schmoose).ready = true;
                    /*
                    void DisableActive()
                    {
                        speechBubble.transform.parent.gameObject.SetActive(false);

                        speechBubble.running = false;
                    }
                    */

                    ((QuestionSchmoose)schmoose).DelayDisableBubble(7f);

                }), this);
            }
        }
    }


    public void DelayDisableBubble(float delay)
    {
        Invoke(nameof(DisableBubble), delay);
    }

    private void DisableBubble()
    {
        speechBubble.running = false;
        
        speechBubble.transform.parent.gameObject.SetActive(false);
    }
        
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            face.lookAt = true;
            
            if (!GameManager.Flags[Flag.SCHMOOSE_ASKED])
            {
                GameManager.Flags[Flag.SCHMOOSE_ASKED] = true;
                ChanceToSetActive(1f, Random.Range(0, facts.Count));
            }
            else if (!GameManager.Flags[Flag.SCHMOOSE_COMPLETE])
            {
                ChanceToSetActive(0.2f, Random.Range(0, facts.Count));
            }
            else
            {
                ChanceToSetActive(0.2f);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            face.lookAt = false;
        }
    }
}
