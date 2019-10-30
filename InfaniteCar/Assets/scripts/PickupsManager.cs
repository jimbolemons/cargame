using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsManager : MonoBehaviour
{
    PlayerInput input;


    public Transform RearSpawnPoint;
    public Transform FrontSpawnPoint;


    public bool hasOil = false;
    public bool hasRockets = false;
    public bool hasBomb = false;

    public bool hasSpeed = false;
    public bool hasShield = false;

    public bool usingSpeed = false;
    public bool usingShield = false;


	public GameObject oilPrefab;


    public GameObject bombPrefab;


    public Renderer ShieldRenderer;
    float shieldSpawnTime = 0;
    float shieldDuration = 10;

    public Renderer SpeedRenderer;
    float speedSpawnTime = 0;
    float speedDuration = 10;

    public RocketsManager rockets;


    CarStats CS;
    // Start is called before the first frame update
    void Start()
    {
    	CS = GetComponent<CarStats>();
        input = PlayerInput.instance;
        rockets = GetComponent<RocketsManager>();

    }

    // Update is called once per frame
    void Update()
    {


        if(usingShield && Time.time-shieldSpawnTime > shieldDuration)
        	usingShield = false;

		ShieldRenderer.enabled = usingShield;


		if(usingSpeed && Time.time-speedSpawnTime > speedDuration)
        	usingSpeed=false;
		SpeedRenderer.enabled = usingSpeed;


		if(Input.GetKeyDown(KeyCode.Q)){
			UseCurrentPickup();
		}
    }
   public  void UseCurrentPickup(){
    	Debug.Log("Attempt use pickup");
    	if(hasRockets)
           SpawnRocketsButton();
        else if(hasOil)
          SpawnOilButton();
        else if(hasBomb)
         	SpawnBombButton();
        else if(hasSpeed)
        	SpawnSpeedButton();
        else if(hasShield)	
        	SpawnShieldButton();
    }
    void SetAllFalse(){
    	hasOil = false;
    	hasRockets = false;
    	hasBomb = false;
    	hasSpeed = false;
    	hasShield = false;
    }
	public void CollectedPickUp(PickupType type){
    	switch(type){
            case PickupType.rockets:
                SetAllFalse();
                hasRockets = true;  
            break;
            case PickupType.oil:
                SetAllFalse();
                hasOil = true;
            break;
            case PickupType.bomb:
                SetAllFalse();
                hasBomb = true;
            break;
            case PickupType.speed:   
                SetAllFalse();
				hasSpeed = true;


            break;
            case PickupType.shield:
                SetAllFalse();
                hasShield =true;

            break;
            case PickupType.gas:
                CS.FillGas();
            break;
            case PickupType.repair:
                CS.RepaireDamages();
            break;

        }
    }
    public void SpawnOilButton(){
    	//Debug.Log("Spawn oil");
    	if(hasOil){
    		SpawnOil();
    	}
    }
    public void SpawnBombButton(){
    	//Debug.Log("Spawn bomb");

    	if(hasBomb){
    		SpawnBomb();
    	}
    }
    public void SpawnRocketsButton(){
    	//Debug.Log("Spawn rockets");

    	if(hasRockets){
    		if(rockets.AttemptShoot())//attempt to shoot
    			hasRockets=false;

    	}
    }
    public void SpawnSpeedButton(){

    	if(hasSpeed){
        Debug.Log("use speed");

    		hasSpeed = false;
    		usingSpeed = true;
			speedSpawnTime = Time.time;
    	}
    }
    public void SpawnShieldButton(){

    	if(hasShield){
        Debug.Log("use shield");

    		hasShield = false;
    		usingShield = true;
			shieldSpawnTime = Time.time;
    	}
    }
    void SpawnOil(){

    	hasOil=false;

    	Tile t = TileMover.instance.GetCurrentTile();

		GameObject	oilObject = Instantiate(oilPrefab,  new Vector3(RearSpawnPoint.position.x,0,RearSpawnPoint.position.z), RearSpawnPoint.rotation, null);
        oilObject.transform.SetParent(t.transform);
        Debug.Log(oilObject);
    }
    void SpawnBomb(){
    	Tile t = TileMover.instance.GetCurrentTile();
    	hasBomb = false;
        GameObject bombObject = Instantiate(bombPrefab, new Vector3(RearSpawnPoint.position.x, 0, RearSpawnPoint.position.z), RearSpawnPoint.rotation, null);
        bombObject.transform.SetParent(t.transform);
    }
}
