using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTimer : MonoBehaviour
{
    public float howLongToLive;
    public bool markedForDeath = false;    
    void FixedUpdate()
    {
        //timer for each bullet 
        if (howLongToLive > 0)
        {
            howLongToLive -= Time.deltaTime;
        }
        else
        {
            //if the timer runs out then the bullet is marked to be deleted
            markedForDeath = true;
        }        
    }
}
