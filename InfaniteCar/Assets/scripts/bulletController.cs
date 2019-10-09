using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public float howLongToLive ;
    public bool markedForDeath = false;
    public bool bulletBounce = false;
    public float bounceTimer ;
    Rigidbody rb;
    void Start(){
        rb = GetComponent<Rigidbody>();
    }
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
        if (bulletBounce == true)
        {

            if (bounceTimer > 0)
            {
                bounceTimer -= Time.deltaTime;
            }
            else
            {
                //if the timer runs out then the bullet is marked to be deleted
                markedForDeath = true;

            }

        }

        this.transform.position -= TileMover.instance.GetMovementUpdate();   
        this.transform.position +=  this.transform.forward;

    }
    private void OnCollisionEnter(Collision col)
    {
            Debug.Log("<color=yellow>something hit ai driver</color>");

        Debug.Log("boom" +col.gameObject.name);
        // if the bullet hits an enemy it will do this
        if (col.gameObject.tag == "Enemy")
        {
            markedForDeath = true;
            Debug.Log("Bullet hit enemy" + col.gameObject.name);
            //mark the enemy as dead
        }
        //if the bullet hits somthing that is not an enemy it will do this
        if (col.gameObject.tag != "Enemy")
        {
            bulletBounce = true;
            //Debug.Log(col.gameObject.tag);
        }
       


    }
}
