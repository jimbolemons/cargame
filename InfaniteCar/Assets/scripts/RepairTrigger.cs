using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider col){
    	if(col.gameObject.layer == LayerMask.NameToLayer("PlayerBody") ){
    		col.GetComponentInParent<CarStats>().RepaireDamages();
    	}
    }
}
