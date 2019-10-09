using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsManager : MonoBehaviour
{
    public GameObject rocketPrefab;
    //PlayerInput input;
    List<RocketController> rockets = new List<RocketController>();
    List<RocketController> rocketsToRemove = new List<RocketController>();
    public float currentSpeed = .01f;
    public Transform barrel;
    public float thrust = 50;
    public float NumOfRockets = 20;
   // public bool canShootRocket;
    //public bool startboll = false;
   // public Enemy enemyToAttack ;
    //private Vector3 enemyToKill;
    //public Vector3 enemyPos;


     public List<AIDriver> InRange = new List<AIDriver>();
    public List<AIDriver> OutOfRange = new List<AIDriver>();
    
    

    void FixedUpdate()
    {

        //create rockets when active
        
        //update rockets , check to see if rockets need to be destroyed
        //destroy rockets when they are marked for death
        UpdateRockets();
        DeleteRockets();


        UpdateRangeRemoval();
    }


    public void Shoot(){
        if(ValidTargetsInRange() )
        {
            AddRocket();
        }
    }
    public bool AttemptShoot(){
        if(ValidTargetsInRange() )
        {
            AddRocket();
            return true;
        }
        return false;
    }
    bool ValidTargetsInRange(){
        return InRange.Count > 0;
    }
    

    void UpdateRockets()
    {
        foreach (RocketController obj in rockets)
        {
            //check to see how long it has been alive 
            // if it has been alive for more then 3 seconds kill it
            if (obj.markedForExplode == true)
            {
                rocketsToRemove.Add(obj);
            } 

            //check to see if bullet hits somthing
            //if it has mark for death and do damage 
        }
    }
    void DeleteRockets()
    {
        if (rocketsToRemove.Count > 0)
        {
            //destroys all bullets that are in the bullets to remove list 
            foreach (RocketController obj in rocketsToRemove)
            {
                rockets.Remove(obj);
                Destroy(obj.gameObject);
            }
            //clears bullets to remove after destorying all bullets
            rocketsToRemove.Clear();
        }
    }

    private void AddRocket()
    {
        Debug.Log("Shooting rockets confirmed");
        for (int i = 0; i < NumOfRockets; i++)
        {
            RocketController rocket = Instantiate(rocketPrefab, barrel.position, barrel.rotation, null).GetComponent<RocketController>();
            rocket.Init(this);
            rockets.Add(rocket);
        }
        
    }




    public void UpdateRangeRemoval()
    {

        if (OutOfRange.Count > 0)
        {

            foreach (AIDriver obj in OutOfRange)
            {
                InRange.Remove(obj);
            }
            OutOfRange.Clear();


            //clears bullets to remove after destorying all bullets



        }
        
    }
}

