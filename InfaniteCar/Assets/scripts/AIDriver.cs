using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDriver : MonoBehaviour
{
    // Start is called before the first frame update
    //right lane 
    TileMover tileMover;
   public bool onComing = false;

    //find closest tile from GM

    public Tile currentTile;
    public Transform currentWaypoint;
    public Transform nextWaypoint;
   // float turnSpeed = .5f;
    float waypointMinDistance = 4f;
    int wayIndex = 0;
   // float speed = .3f;
    //float PoliceChaseSpeed= .4f;
    float laneOffset = 2.5f;
    bool police = false;
    float policespeedMod = 1f;
   // bool inPursuit = false;
    public bool isdead = false;
    private float x ;
    private float y ;
    public float wayPointdis;
    public float percentOfdis;
    float oilTime = 2;
    bool oil = false;
    public Vector3 spinOutRotation;

    private float rotSpeed = 500f;

    private Quaternion _lookRotation;
    private Vector3 _direction;
    float rand;

    public bool isPoliceCar2 = false;


    PlayerController pc;
    GameManager GM;
    AICarsManager AI;
    void Start(){
        GM = GameManager.instance;
    	pc = PlayerController.instance;
        // AI= AICarsManager.inst
        rand = Random.Range(.1f, -.1f);
     //   if (this.name == "police cruiser")
      //  {
       //     isPoliceCar = true;
       // }
       // Debug.Log("is this a police Cruser " + isPoliceCar);
    }
    public void Init(bool _onComing){
    	tileMover = TileMover.instance; 

    	onComing = _onComing;
    	
    	currentTile = TileMover.instance.FindLastTile();

    	currentWaypoint = currentTile.FindFirstWay(onComing);


        if (onComing)
			wayIndex =currentTile.GetWaypointCount();
        transform.SetParent(currentTile.transform);

        if (onComing)
        {
            this.transform.position = LeftLane();
            nextWaypoint = currentTile.waypoints[wayIndex - 1];
        }
        else
        {
            this.transform.position = RightLane();
            nextWaypoint = currentTile.waypoints[wayIndex + 1];
        }
    	
    }
    void OnDestroy(){
       RemoveDriver("On Destroy");

    }
    void OnDisable(){
       RemoveDriver("On Disable");

    }
    void OnDisabled(){
        RemoveDriver("On Disabled");

    }
   
    public void InitPolice(){
        tileMover = TileMover.instance; 
        
        currentTile = TileMover.instance.Tiles[1];

        currentWaypoint = currentTile.FindFirstWay(false);

        transform.SetParent(currentTile.transform);

        this.transform.position =RightLane();

        police=true;
    }

    void FixedUpdate()
    {
        if(!GM.GameRunning()) return;
    	if(currentTile != null && currentWaypoint !=null){
        	CheckWayPoint();
        	Movement();
            OilSpin();
    	}else{
    		if(currentTile == null)
    		  Debug.Log("<color=red> AI with no tile</color>");
            
    		if(currentWaypoint == null)    		  

            RemoveDriver("<color=red> AI with no waypoint</color>");
           isdead =true;
    	}
    }
    void Movement(){
        if(police) PoliceMovement();
        else StandardMovement();
    	
    }
    void PoliceMovement(){
        Vector3 dir= Vector3.zero;
        Vector3 target = Vector3.zero;
        float distance  = Vector3.Distance(this.transform.position, pc.transform.position);

        if(distance>10f){
            dir =RightLane() - this.transform.position;
            if(!oil)
            this.transform.LookAt(RightLane());

            dir = dir.normalized;

            this.transform.position +=dir *tileMover.GetUnstoppableSpeed() *policespeedMod *1.1f;
        }else{
            if (!oil)
               
            if (rand > 0)
               target = pc.transform.position + pc.transform.right * laneOffset / 2;
            else
                target = pc.transform.position + pc.transform.right * -laneOffset / 2;

            this.transform.LookAt(target);

            dir =target - this.transform.position;
            dir = dir.normalized;

            this.transform.position +=dir * tileMover.GetUnstoppableSpeed() *policespeedMod;
        }
    }
    void StandardMovement(){
        Vector3 dir= Vector3.zero;
        Vector3 dir2 = Vector3.zero;


        if (!onComing)
        {
            dir = RightLane() - this.transform.position;
            if(nextWaypoint != null)
            dir2 = RightLane2() - this.transform.position;
            //this.transform.LookAt(RightLane());
           _direction = (RightLane()  - transform.position).normalized;
           _lookRotation = Quaternion.LookRotation(_direction);
           transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotSpeed);
        }
        else
        {
            dir = LeftLane() - this.transform.position;
            if(nextWaypoint != null)
            dir2 = LeftLane2() - this.transform.position;
            //this.transform.LookAt(LeftLane());
           _direction = (LeftLane() - transform.position).normalized;
           _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotSpeed);
        }
        dir = dir.normalized;
        if(nextWaypoint != null)
            dir2 = NextWayDirection.normalized;


        percentOfdis = wayPointdis * .2f;
        if (percentOfdis <= .2f && nextWaypoint != null)
        {
            Vector3 somthing = ((dir * .1f) * x) + ((dir2 * .1f) * y);
            Debug.DrawRay(this.transform.position, somthing, Color.red, .5f);
            this.transform.position += somthing;
        }
        else
        {
            this.transform.position += dir * .5f;

        }


        
       // GetNextWaypoint();
        

    }

    
    Vector3 RightLane(){
    	return currentWaypoint.position + (currentWaypoint.right*laneOffset);
    }
    Vector3 RightLane2()
    {
        return nextWaypoint.position + (nextWaypoint.right * laneOffset);
    }
    Vector3 LeftLane(){
    	return currentWaypoint.position - (currentWaypoint.right*laneOffset);
    }
    Vector3 LeftLane2()
    {
        return nextWaypoint.position - (nextWaypoint.right * laneOffset);
    }
    void CheckWayPoint(){
    	float dist = Vector3.Distance(this.transform.position, currentWaypoint.position);
        if (dist <= percentOfdis)
        {
            if (NextWayDirection == Vector3.zero || nextWaypoint == null)
            {
             //   Debug.Log("no where to go");
                y = 0;
                x = 1;
            }
            else
            { 
                x = dist / (wayPointdis * .2f);
//                Debug.Log(x);
                y = 1 - x;
            }

        }

    	if( Vector3.Distance(this.transform.position, currentWaypoint.position) < waypointMinDistance){
            
            MoveToNextWay();

            NextWayDirection = getNextWaypointDirection();
            nextWaypoint = getNextWaypoint();

            wayPointdis = Vector3.Distance(this.transform.position, currentWaypoint.position);
        }
    }
	Vector3 WayDirection = new Vector3(0,0,1);
    Vector3 NextWayDirection = new Vector3(0, 0, 1);

    Vector3 getNextWaypointDirection() {
        if (!onComing)
        {
            if (wayIndex + 1 < currentTile.waypoints.Count)
            {
//                Debug.Log("yep");
                return currentTile.waypoints[wayIndex + 1].forward;
            }
            else
            {//Next tiles
                Tile nextTile = TileMover.instance.FindTileAfter(currentTile);
                if (nextTile == null)
                {
                    RemoveDriver("End of way 1");
                    
                    return Vector3.zero;
                }
                else
                {
                    return nextTile.waypoints[0].forward;
                }
            }
        }
        else
        {
            if (wayIndex - 1 >= 0)
            {
                return currentTile.waypoints[wayIndex - 1].forward;               
            }
            else
            {//Next tiles
                Tile nextTile = TileMover.instance.FindTileAfter(currentTile);
                if (nextTile == null)
                {
                   
                  
                    RemoveDriver("End of way 2");
                    return Vector3.zero;
                }
                else
                {
                    return nextTile.waypoints[nextTile.GetWaypointCount() - 1].forward;

                }
            }

        }
    }
    Transform getNextWaypoint() {
        if (!onComing)
        {
            if (wayIndex + 1 < currentTile.waypoints.Count)
            {
                //Debug.Log("yep");
                return currentTile.waypoints[wayIndex + 1];
            }
            else
            {//Next tiles
                Tile nextTile = TileMover.instance.FindTileAfter(currentTile);
                if (nextTile == null)
                {
                    RemoveDriver("End of way 3");
                    return null;
                }
                else
                {
                    return nextTile.waypoints[0];
                }
            }
        }
        else
        {
            if (wayIndex - 1 >= 0)
            {
                return currentTile.waypoints[wayIndex - 1];               
            }
            else
            {//Next tiles
                Tile nextTile = TileMover.instance.FindTileAfter(currentTile);
                if (nextTile == null)
                {
                    
                  
                    RemoveDriver("End of way 4");
                    return null;
                }
                else
                {
                    return nextTile.waypoints[nextTile.GetWaypointCount() - 1];

                }
            }

        }
    }

    void MoveToNextWay(){
    	Debug.Log("Find next waypoint");
        if (!onComing)
        {
            if (wayIndex + 1 < currentTile.waypoints.Count)
            {
                wayIndex++;                
                WayDirection = currentWaypoint.forward;
                currentWaypoint = currentTile.waypoints[wayIndex];
            }
            else
            {//Next tiles
                wayIndex = 0;
                currentTile = TileMover.instance.FindTileAfter(currentTile);

                if (currentTile == null)
                {
                    RemoveDriver("End of way 5");
                    
                }
                else
                {
                    WayDirection = currentWaypoint.forward;
                    currentWaypoint = currentTile.waypoints[wayIndex];
                    transform.SetParent(currentTile.transform);
                }
            }

        }
        else
        {
            if (wayIndex - 1 >= 0)
            {
                wayIndex--;                
                WayDirection = -currentWaypoint.forward;
                currentWaypoint = currentTile.waypoints[wayIndex];

            }
            else
            {//Next tiles
                currentTile = TileMover.instance.FindTileBefore(currentTile);

                if (currentTile == null)
                {
                    
                   
                    RemoveDriver("End of way 6");
                }
                else
                {
                    wayIndex = currentTile.GetWaypointCount() - 1;
                    WayDirection = -currentWaypoint.forward;
                    currentWaypoint = currentTile.waypoints[wayIndex];                   
                    transform.SetParent(currentTile.transform);

                }
            }

        }
    }
    public void RemoveDriver(string info){
        if (police)
        Debug.Log("police" + info);
        Debug.Log(info);

        isdead = true;
        GameManager.instance.AICars.RemoveDriver(this,info);
       
        if (police) IfDestroyedIsPoliceCar();
    }
    public void IfDestroyedIsPoliceCar() {

        AICarsManager.policeSpawned = false;

    }
    void OnCollisionEnter(Collision col)
    {
            Debug.Log("<color=red>something hit ai driver</color>");

        if (col.gameObject.tag == "Bullet")
        {
            if (!police)
            { 
            isdead = true;
            RemoveDriver("collision with bullet");
            }
            Debug.Log("boom" + col.gameObject.name);
            //mark the enemy as dead
        }else if (col.gameObject.tag != "Bullet") //if the bullet hits somthing that is not an enemy it will do this
        {
            Debug.Log(col.gameObject.tag);
        }

    }
    void OilSpin()
    {
        if (oil) { 
            if (oilTime > 0)
            {
                policespeedMod = .2f;
                this.transform.eulerAngles += spinOutRotation * Time.fixedDeltaTime;
                Debug.Log("spinnout");
                oilTime -= Time.deltaTime;
            }
            else
            {
                policespeedMod = 1.01f;
                oil = false;
                oilTime = 2;

            }
        
        }

    }
    public void HitOil()
    {
        oil = true;

    }
    public void Bump(float f)
    {

       // this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, this.transform.eulerAngles - new Vector3(0, f, 0), 1);


    }
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player") 
        {
            Debug.Log("<color=red>Player hit car from AIDriver on trigger</color>");
            if (!police)
            {
                    isdead = true;                
                    RemoveDriver("trigger with player");                
                    PlayerController.instance.HitOtherCar();                
                    TileMover.instance.PlayerHitCar();
            }
            
        }

        if (col.gameObject.tag == "spawner")
        {
            if (isPoliceCar2)
            {
                if (!police)
                {

                    AICarsManager.policeSpawned = false;
                    isdead = true;
                    RemoveDriver("trigger with player");
                }
            }


        }
        if (col.gameObject.tag == "Bullet")
        {
            if (!police)
            { 
                isdead = true;            
                RemoveDriver("trigger with bullet");
            }
           // Debug.Log("boom" + col.gameObject.name);
            //mark the enemy as dead
        }
        else if (col.gameObject.tag != "Bullet") //if the bullet hits somthing that is not an enemy it will do this
        {
            //Debug.Log("fuck" + col.gameObject.tag);
        }
        //if (col.gameObject.tag == "BombRadius")
        //{
         //   Debug.Log("<color=red>fuck i blew up</color>");
        //    isdead = true;
        //    if (police)
         //       GameManager.instance.AICars.RemoveDriver(this, "explosian");
        //}
    }

}
