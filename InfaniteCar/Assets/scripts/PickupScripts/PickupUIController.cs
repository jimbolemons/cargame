using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///using UnityEngine.UI;

public class PickupUIController : MonoBehaviour
{
	public GameObject Rcokets, Bomb, oil, shield,Speed;
	PlayerController PC;
    // Start is called before the first frame update
    void Start()
    {
      PC =   PlayerController.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Bomb.SetActive(PC.pickups.hasBomb);
        Rcokets.SetActive(PC.pickups.hasRockets);
        oil.SetActive(PC.pickups.hasOil);
        shield.SetActive(PC.pickups.hasShield);
        Speed.SetActive(PC.pickups.hasSpeed);


    }
}
