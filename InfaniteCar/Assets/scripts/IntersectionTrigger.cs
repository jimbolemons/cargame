using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntersectionTrigger : MonoBehaviour
{
	public bool isRight = false;
   
    public void OnTriggerEnter(Collider col){
    	if(col.gameObject.tag == "Player"){
    		if(isRight){
    			GetComponentInParent<Tile>().PlayerIntersectionChange(2);
    		}else{
    			GetComponentInParent<Tile>().PlayerIntersectionChange(1);

    		}
    	}
    }
}
