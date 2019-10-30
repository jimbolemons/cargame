using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPostTrigger : MonoBehaviour
{
   public GameObject LampMeshObj;
   public GameObject LampPhysicsObj;

    void OnTriggerEnter(Collider col){
    	if(col.gameObject.tag == "Player" || col.gameObject.tag == "PhysicsObject" ){
    		//spawn real lamppost and remove this one
    		LampMeshObj.SetActive(false);
    		LampPhysicsObj.SetActive(true);

    		LampPhysicsObj.GetComponent<Rigidbody>().AddForce(PlayerController.instance.playerForward*10000f);

            col.GetComponentInParent<PlayerController>().TakeDamage(10);

    	}
    }
}
