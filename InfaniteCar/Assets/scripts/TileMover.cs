using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileMover : MonoBehaviour
{
	public static TileMover instance;
	//public GameObject tile;
	//public List<GameObject> TileTypes = new List<GameObject>();

	public List<Tile> Tiles = new List<Tile>();
	public List<Tile> TilesToRemove = new List<Tile>();
	//int segments = 6;
	public float baseSpeed = .5f;
    float InitialBaseSpeed = .3f;
	float playerSpeed = 0;
	//float offset = 0;
	float maxSpeed = 1f;
	float width=15;
    float sideForce = 0;
    public PickupsManager pickups;

    //public float currentSpeed = 0;
    // PlayerInput input;



    public float PlayerBrakeAmount = 1;



    float CarDataSpeed =0;
   // float CarDataAccel =0;


    PlayerController PC;
    GameManager GM;
    //PlayerData playerData;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        EventManager.OnGameReset += OnGameReset;
        EventManager.OnResumeAftervideo += OnResumeAftervideo;

        InitialBaseSpeed = baseSpeed;
    }
    void Start(){
        GM = GameManager.instance;
        PC = PlayerController.instance;

    }
    void OnGameReset(){
        baseSpeed = InitialBaseSpeed;
        RealignPlayer();
    }
    void OnResumeAftervideo(){

        RealignPlayer();

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if(!GM.GameRunning())   return;

        MoveTiles(); 
        CheckTilestoRemove();


        baseSpeed += Time.fixedDeltaTime/ 1000f;
        

        if(HitBreak<1) HitBreak+=.1f;

        
        if (Mathf.Abs(sideForce) > 0) sideForce *= .9f;

        
        
    }

    void CheckTilestoRemove(){
    	if(TilesToRemove.Count >0){
    		foreach(Tile obj in TilesToRemove){
               
		    	Tiles.Remove(obj);
		    	Destroy(obj.gameObject);
    		}
    		TilesToRemove.Clear();
    	}
    }


    Vector3 Absolute(Vector3 v){
        return new Vector3(Mathf.Abs(v.x),  Mathf.Abs(v.y), Mathf.Abs(v.z));
    }



    public Tile LastTile(){
        return Tiles.Last();
    }
    Vector3 offset = Vector3.zero;
     Vector3 movement ;
     Vector3 absMovement;
    void MoveTiles(){
         movement = GetMovementUpdate();
        absMovement = Absolute(movement);
    	foreach(Tile obj in Tiles){
            obj.transform.position -= movement;
            ApplyDrift(obj.gameObject,PC.turnAngle);
            offset += absMovement;

    	}
    }
    float driftThreshold = .65f;
    void ApplyDrift(GameObject obj, float ang){
        if(ang >driftThreshold ){
            obj.transform.position += DriftDirection() * .1f * DriftAbsValue(ang);
        }else if(ang <-driftThreshold){
            obj.transform.position -= DriftDirection() * .1f *DriftAbsValue(ang);
        }
    
    }
    float DriftAbsValue(float ang){
        float amount = (Mathf.Abs(ang) -driftThreshold) /  (1f- driftThreshold );
        return amount;
    }
    Vector3 DriftDirection(){
        return (PC.transform.right+ (PC.transform.forward *2f)).normalized;
    }
    float HitBreak = 1; 
    public Vector3 GetMovementUpdate(){
        return PC.playerForward* GetSpeed() + GetSideForce();
    }
    

    public float GetSpeed(){
       float debugNoMovement =1;
        if(PC.DebugNoMovement) debugNoMovement=0;

        //Debug.Log("Base: " + baseSpeed +  "  player speed: " + PC.GetCurrentSpeed());
        float speedValue = baseSpeed + (baseSpeed *PC.GetCurrentSpeed());
        speedValue *= HitBreak;//0-1
        speedValue *= debugNoMovement;// 0 or 1
        speedValue *= PC.BrakeAmount;
        
            //Debug.Log(PC.BrakeAmount);
            return speedValue;
        //return (baseSpeed * PlayerBrakeAmount) * HitBreak*debugNoMovement + PC.GetCurrentSpeed();
    }
    public Vector3 GetSideForce()
    {
        return PC.playerRight * sideForce;
    }

    public float GetUnstoppableSpeed(){
        //return (baseSpeed*2f);
        float debugNoMovement = 1;
        if (PC.DebugNoMovement) debugNoMovement = 0;

        //Debug.Log("Base: " + baseSpeed +  "  player speed: " + PC.GetCurrentSpeed());
        float speedValue = baseSpeed + (baseSpeed * PC.GetCurrentSpeed());
        speedValue *= HitBreak;//0-1
        speedValue *= debugNoMovement;// 0 or 1
        if (pickups.usingSpeed)
            speedValue /= 1.5f;
        // speedValue *= PC.BrakeAmount;
        //Debug.Log(PC.BrakeAmount);
        return speedValue;
        //return (baseSpeed * PlayerBrakeAmount) * HitBreak*debugNoMovement + PC.GetCurrentSpeed();
    }

    void RealignPlayer(){
        Vector3 offset =PC.transform.position - PC.onComingWaypoint.position;
        foreach(Tile obj in Tiles){
            obj.transform.position += offset;
           // transform.RotateAround (Vector3.zero, new Vector3 (0, 1, 0), 90);
        }
    }
    public void PlayerHitCar(){
        HitBreak=0;
    }
    public Tile FindTileAfter(Tile tile){
    	int index = Tiles.IndexOf(tile);
    	if(index +1 >= Tiles.Count)
    		return null;
    	return Tiles[index+1];
    }
    
    public Tile FindTileBefore(Tile tile){
    	int index = Tiles.IndexOf(tile);
    	if(index -1 <0)
    		return null;
    	return Tiles[index-1];
    }
    public Tile FindTileAheadOfPlayer(){
        return Tiles[4];

    }
    public Tile FindLastTile(){
        return Tiles[Tiles.Count-1];
    }
    public Tile GetCurrentTile()
    {
        return Tiles[2];

    }
    public void BumpRight(float Bump) {
        sideForce = Bump;
    }
    public void BumpLeft(float Bump)
    {
        sideForce = -Bump;
    }
}
