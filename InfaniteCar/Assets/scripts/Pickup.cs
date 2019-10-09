using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PickupType{
	rockets,oil,bomb,speed,gas,repair,shield
}


public class Pickup : MonoBehaviour
{	

    PlayerController player;
    public PickupType type;
//    public Placement placement;
    // Start is called before the first frame update
    void Awake()
    {
        player = PlayerController.instance;

    }

    private void OnTriggerEnter(Collider col)
    {
	        if (col.gameObject.tag == "Player")
	        {
	            player.CollectedPickUp(type);
	            Destroy(this.gameObject);
	        }
    }
}
