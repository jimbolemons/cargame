using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	bool hit = false;
    CoinsObjectManager COM;
    public bool SpecialCoin = false; 
    public void SetParent(CoinsObjectManager c){
    	COM=c;
    }
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player" && !hit)
        {
            if (SpecialCoin)
            {
                col.GetComponentInParent<CarStats>().AddCoinPints(10);
                Debug.Log("BIG MONEY");
            }

            else
            {
                col.GetComponentInParent<CarStats>().AddCoinPints(1);
                Debug.Log("MONEY");
            }
    		COM.Remove(this);
        }
    }
}
