using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontBumper : MonoBehaviour
{
    CarStats CS;
    TileMover tilemover;
    PlayerController PC;
    public bool Front;
    public bool Left;
    public bool Right;



    void Start()
    {
        CS = CarStats.instance;
        tilemover = TileMover.instance;
        PC = PlayerController.instance;

    }


    void Update()
    {
        

    }
    public void OnTriggerEnter(Collider col)
    {
        if (Front)
        {
//            Debug.Log("fuck i hit a building F");
            if (col.gameObject.tag == "Building")
            {
            //  Debug.Log("front");

                
                    PC.TakeDamage(1000);
            }
        }else if (Left){
            if (col.gameObject.tag == "Building")
            {
                // bump the car to the right
                tilemover.BumpRight();
              //  Debug.Log("left");
               
                    PC.TakeDamage(10);
            }  
        }
        else if (Right){
            if (col.gameObject.tag == "Building")
            {
                tilemover.BumpLeft();
                //bump the car to the left

                //Debug.Log("right");
               
                    PC.TakeDamage(10);
            }
        }
        else{
        // Debug.Log("I dont know what i just hit");

        }
        
    }
}
