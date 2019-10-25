using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    CarStats CS;
    //public BombSpawner bSpawner;
    //public OilSlick oil;

    public static PlayerController instance;
    //public Vector3 playerForward = new Vector3(0,0,1);
    public Vector3 playerRight = new Vector3(0, 0, 1);
    public Vector3 playerFront = new Vector3(0, 0, 1);
    // Start is called before the first frame update


    public delegate void PlayerEvent();
    public static event PlayerEvent OnTileChange;

    //public float TotalDistance = 0;
    //public float distanceToNextWay = 0;

    public Transform onComingWaypoint;
   public float NextWaytotalDistance =0;
    bool inited = false;

   // public TextMeshProUGUI  tex;

    public Tile currTile;
    public float turnAngle = 0;
    float PlayerBrakeAmount = 0;


    PlayerData playerData;
    GameObject CarMesh;
    public Transform CarMeshContainer;


    public PickupsManager pickups;

    public bool DebugNoMovement = false;
    public CameraController camera;


    PlayerInput input;

    public Vector3 playerForward{ get { return this.transform.forward;}
    }
    public void Init(){
        onComingWaypoint =TileMover.instance.Tiles[2].waypoints[0].transform;
        NextWaytotalDistance =  Mathf.Abs((onComingWaypoint.position - this.transform.position).magnitude);
        currTile = TileMover.instance.Tiles[1];
        CarMesh = Instantiate(playerData.currentSelection.MeshObject, Vector3.zero, Quaternion.identity,CarMeshContainer);
        inited = true;
        CarMesh.transform.localScale = Vector3.one*.75f;
    }
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CS = CarStats.instance;
        playerData = PlayerData.instance;
        input = PlayerInput.instance;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // playerForward = this.transform.forward;
        playerRight = this.transform.right;
        playerFront = this.transform.forward;

        if(input.Down()){
            if( PlayerBrakeAmount < .29f){
                PlayerBrakeAmount +=playerData.currentSelection.Acceleration*5f ;
            }else {
                PlayerBrakeAmount =.3f;
            }

        }else{
            if(PlayerBrakeAmount >0.01f ){
                PlayerBrakeAmount -= playerData.currentSelection.Acceleration ;
               

            }else 
                 PlayerBrakeAmount =0;

        }
       PlayerBrakeAmount = Mathf.Clamp01(PlayerBrakeAmount);

    }
    public bool IsStarted(){
        return inited;
    }
    public float GetCurrentSpeed(){
        if(DebugNoMovement) return 0;
        //Debug.Log("Get player speed car: " + playerData.currentSelection.Speed  + "   upgrade "+ (playerData.playerUpgrades.CarUpgrades[playerData.SelectionIndex].Speed *.1f));
        if(pickups.usingSpeed)
          return ( playerData.currentSelection.Speed+ (playerData.playerUpgrades.CarUpgrades[playerData.SelectionIndex].Speed *.1f)) *1.5f ;
        else
          return  playerData.currentSelection.Speed+ (playerData.playerUpgrades.CarUpgrades[playerData.SelectionIndex].Speed *.1f) ;

    }

    public float BrakeAmount{ set{PlayerBrakeAmount = value;}  get{ return(1-PlayerBrakeAmount);}}

    public void SetTurnAngle(float t,float total){
        turnAngle = Mathf.Clamp(t/total, -1,1);
    }
    public void HitTile( Tile tile){
        GetComponent<CarStats>().HitTile(currTile);//pass in previouse tile to add to distance
        
        onComingWaypoint = TileMover.instance.Tiles[4].waypoints[0].transform;
        NextWaytotalDistance =  Mathf.Abs((onComingWaypoint.position - this.transform.position).magnitude);

       currTile = tile;
       if(OnTileChange!=null)
            OnTileChange();
    }
    
    public void HitOtherCar(){
      
        TakeDamage(10);
    }
    public void TakeDamage(int amount){
        //camera.CameraShake(.2f,50);
        //PostProcessingEffectsManager.instance.Flash();
        if (!pickups.usingShield)
        this.GetComponent<CarStats>().TakeDamage(amount);

    }
    
    public void CollectedPickUp(PickupType type){
        pickups.CollectedPickUp(type);


    }
    // void OnCollisionEnter(Collision col)
    // {
    //     Debug.Log("<color=red>something hit ai driver</color>");

    //     if (col.gameObject.tag == "Enemy") 
    //     {
    //     Debug.Log("<color=red>Player hit car</color>");
            
    //       HitOtherCar();
    //        TileMover.instance.PlayerHitCar();
    //     }

    // }
}
