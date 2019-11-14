using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Bumperside{
	Left,Right,Front,Back
}
public class CarBumper : MonoBehaviour
{
	public Bumperside bumperside = Bumperside.Front;
    public void OnTriggerEnter(Collider col)
    {
    	if (col.gameObject.tag != "Building" ) return;


    	switch(bumperside){
    		case Bumperside.Front:
    			PlayerController.instance.TakeDamage(100);
                TileMover.instance.BumpFront(-.1f);
                TileMover.instance.PlayerHitCar();
                break;
    		case Bumperside.Right:
    			PlayerController.instance.TakeDamage(10);
                CarMovement.instance.Turn(-5);
                TileMover.instance.BumpLeft(.1f);
                TileMover.instance.PlayerHitCar();

                break;
    		case Bumperside.Left:
    			PlayerController.instance.TakeDamage(10);
                CarMovement.instance.Turn(5);
                TileMover.instance.BumpRight(.1f);
                TileMover.instance.PlayerHitCar();

                break;
            case Bumperside.Back:
                PlayerController.instance.TakeDamage(10);
                
                TileMover.instance.BumpFront(.5f);
                TileMover.instance.PlayerHitCar();
                Debug.Log("bump");

                break;

        }
        
        
    }
}
