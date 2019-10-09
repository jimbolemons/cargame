using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Bumperside{
	Left,Right,Front
}
public class CarBumper : MonoBehaviour
{
	public Bumperside bumperside = Bumperside.Front;
    public void OnTriggerEnter(Collider col)
    {
    	if (col.gameObject.tag != "Building") return;


    	switch(bumperside){
    		case Bumperside.Front:
    			PlayerController.instance.TakeDamage(1000);
    		break;
    		case Bumperside.Right:
    			PlayerController.instance.TakeDamage(10);
                TileMover.instance.BumpLeft();

    		break;
    		case Bumperside.Left:
    			PlayerController.instance.TakeDamage(10);
                TileMover.instance.BumpRight();

    		break;

    	}
        
        
    }
}
