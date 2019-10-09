using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    public GameObject currentHitObj;

    public float speed;
    public float deathTimer = 3;

    public GameObject explotion;

    public float bombRadius = 20;

    private float currentHitDis;

    void FixedUpdate()
    {
        Timers();
    }
    void Timers()
    {
        
        deathTimer -= Time.deltaTime;
        
        if (deathTimer <= 0)
        {
            Detonate();
        }
    }

  

    private void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "Enemy")
        {
            Detonate();
        }

        if (trig.gameObject.tag != "Enemy")
        {

            //isdead = true;
           // Debug.Log(trig.gameObject.tag);
        }
        //detect if the enemys are hitting the bomb
        //if they are hitting the bomb 
        //then detonate it       
    }

    private void Detonate()
    {
       
       GameObject explode = Instantiate(explotion, this.transform.position, Quaternion.identity);
        Destroy(explode, .5f);
        Explosion();
        Destroy(this.gameObject);




    }
    void Explosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, bombRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                GameManager.instance.AICars.RemoveDriver(hitColliders[i].GetComponent<AIDriver>());               

            }
            i++;
        }



    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * currentHitDis, bombRadius);
    }

}

