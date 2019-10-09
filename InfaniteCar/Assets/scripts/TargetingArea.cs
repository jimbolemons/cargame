using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingArea : MonoBehaviour
{ 
    //public static TargetingArea instance;
    // public List<AIDriver> InRange = new List<AIDriver>();
    // public List<AIDriver> OutOfRange = new List<AIDriver>();

    // void Awake()
    // {
    //     instance = this;
        
    // }

public RocketsManager RM;
    
    public void OnTriggerEnter(Collider col )
    {
        if (col.gameObject.tag == "Enemy")
        {
            // add this item to an arry of in range enemies
            RM.InRange.Add(col.GetComponent<AIDriver>());

        }
        
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            // remove this item from the arry of in range enemies
           RM.OutOfRange.Add(col.GetComponent<AIDriver>());
        }

    }
    
   
}
