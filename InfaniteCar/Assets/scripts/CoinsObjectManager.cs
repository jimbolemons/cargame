using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsObjectManager : MonoBehaviour
{
	public List<Coin> coins = new List<Coin>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Coin c in GetComponentsInChildren<Coin>()){
        	coins.Add(c);
        	c.SetParent(this);
        }
        coins[coins.Count-1].gameObject.SetActive(false);
    }
    public void Remove(Coin c){
    	coins.Remove(c);
    	Destroy(c.gameObject);
    	if(coins.Count ==1)
    		coins[0].gameObject.SetActive(true);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
    	float offset =0;
        foreach(Coin c in coins){
        	c.transform.position = new Vector3(c.transform.position.x , Mathf.PingPong(Time.time-offset,1)+.25f ,c.transform.position.z);
        	offset += .25f;
        }
        
    }
}
