using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float lifeTimer;
    public float rocketBounceTimer;
    public float enemyTimer;

    public bool markedForExplode = false;
    public bool boostTime = false;
  

    Quaternion lookOnLook = Quaternion.identity;

 


    public float thrust;
    public float boost = 100;
    public float forwardThrust = 10;
    public float upThrust;
    
    public float turnSpeed = .1f;


    private Rigidbody rb;
    RocketsManager RM;

    bool started= false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrust = Random.Range(-10f, 10f);
        upThrust = Random.Range(-10f, 0f);
    }
    public void Init(RocketsManager r){
        RM = r;
        started=true;
    }
    // use for physics
    void FixedUpdate()
    {
        if(!started)return;
        GoTowardsEnemy();
        Timers();
       
        if (enemyTimer <= 0)
        {
            boostTime = true;
           // noMoreSpeed = true;
        }

        if(enemyTimer > 0) {
            RocketVariations();
        }


    }
    private void GoTowardsEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
//        Debug.Log(TargetingArea.instance);
        List<AIDriver> Enemies = RM.InRange;
        AIDriver closestEnemy = null;        
        
        //go through the list of enemys to find the closest en
       //Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (AIDriver currentEnemy in Enemies)
        {
            if (currentEnemy != null) {
                float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
        }
        if (closestEnemy != null)
        {
            lookOnLook = Quaternion.LookRotation(closestEnemy.transform.position - transform.position);            
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * turnSpeed);
            if (boostTime)
            {
                rb.AddForce(transform.forward * boost, ForceMode.Force);
            }
        }
    }
    private void RocketVariations()
    {
        rb.AddForce(transform.forward * forwardThrust, ForceMode.Acceleration);
        rb.AddForce(transform.right * -thrust, ForceMode.Acceleration);
        rb.AddForce(transform.up * -upThrust, ForceMode.Acceleration);

        
    }

    private void Timers()
    {
        if (enemyTimer > 0)
        {
            enemyTimer -= Time.deltaTime;
        }
        if (lifeTimer > 0)
        {
            lifeTimer -= Time.deltaTime;
        }
        if (lifeTimer <= 0 )
        {
            //if the timer runs out then the bullet is marked to be deleted
            markedForExplode = true;
        }        
    }
    
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("boom"  + col.gameObject.name);
        // if the bullet hits an enemy it will do this
        if (col.gameObject.tag == "Enemy")
        {
            markedForExplode = true;
            RM.OutOfRange.Add(col.gameObject.GetComponent<AIDriver>());
        }
        //if the bullet hits somthing that is not an enemy it will do this
        if (col.gameObject.tag != "Enemy")
        {
            markedForExplode = true;
            //Debug.Log(col.gameObject.tag);
        }

    }
}
