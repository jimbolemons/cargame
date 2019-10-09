using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum TileDirection{
    straight,right,left
}
public enum TileSize{
    Big,Small,Tiny
}
public enum TileSiding{
   Ground,  LeftBeach, Beach, RightBeach//,Left_BeachToGround, Left_GroundToBeach, Right_BeachToGround, Right_GroundToBeach
}


public class Tile : MonoBehaviour
{

	public GameObject plane;
    public List<Transform> waypoints = new List<Transform>();
   

    public TileDirection direction = TileDirection.straight;
    public bool isIntersection = false;
    public TileSize tileSize = TileSize.Small;
    public List<Transform> EndNodes = new List<Transform>();
    // Update is called once per frame

    public List<GameObject> FakeEnds = new List<GameObject>();

    List<AIDriver> Cars = new List<AIDriver>();




   // public GameObject buildingprefab;
   // public List<GameObject> buildingprefabs = new List<GameObject>();

    public GameObject PickupSpawnPoints;

    AICarsManager AICars;

    public TileSiding siding = TileSiding.Ground;

    public GameObject[] sidings = new GameObject[4]; 


    public List<Building> BuildLocations = new List<Building>();

    public GameObject RightBuildings, LeftBuildings;
    public GameObject Alt_RightBuildings, Alt_LeftBuildings;

    void Awake(){
        AICars = GameManager.instance.AICars;
        EventManager.OnGameReset += ClearOnReset;
        //EventManager.OnResumeAftervideo += ClearOnReset;
       
    }
    void Start(){
        
        
    }
    void SpawnSingleBuilding(Transform location, int offset=0){
        GameObject prefab = TileSpawner.instance.GetOneBlockBuilding();
        GameObject building = Instantiate(prefab, location.position +(location.right * 5 * offset), location.rotation);
        building.transform.SetParent(location);
    }
    void SpawnTwoBlockBuilding(Transform location, int offset=0){
        GameObject prefab = TileSpawner.instance.GetTwoBlockBuilding();
        GameObject building = Instantiate(prefab, location.position + (location.right * 5 * offset), location.rotation);
        building.transform.SetParent(location);
    }
    void SpawnThreeBlockBuilding(Transform location, int offset=0){
        GameObject prefab = TileSpawner.instance.GetThreeBlockBuilding();
        GameObject building = Instantiate(prefab, location.position + (location.right * 5 * offset), location.rotation);
        building.transform.SetParent(location);
    }
    void GenerateBuildingList(){
        if(RightBuildings != null) AddBuildingsToBuildList(RightBuildings);
            
        
        if(LeftBuildings != null)  AddBuildingsToBuildList(LeftBuildings);
            
        if(Alt_RightBuildings != null)  AddBuildingsToBuildList(Alt_RightBuildings);

        if(Alt_LeftBuildings != null)  AddBuildingsToBuildList(Alt_LeftBuildings);

        
        
    }
    void AddBuildingsToBuildList(GameObject buildingParent){
        AddListOfBuildingsToBuildList(buildingParent.GetComponentsInChildren<Building>().ToList());
    }
    void AddListOfBuildingsToBuildList(List<Building> list){
        foreach(Building b in list){
            if(b.gameObject.activeInHierarchy)
                    BuildLocations.Add(b);
        }
    }
    void SpawnBuildings(){
        GenerateBuildingList();
        if(BuildLocations.Count>0){
            foreach(Building b in BuildLocations){
                if(b.isOneBlock()){
                    SpawnSingleBuilding(b.transform);

                }else if(b.isTwoBlock()){
                    bool spawnTwoBlock = Random.Range(0,100) > 30;
                    if(spawnTwoBlock){
                        SpawnTwoBlockBuilding(b.transform);//spawn two block or two ones
                    }else{
                        SpawnSingleBuilding(b.transform);
                        SpawnSingleBuilding(b.transform,1);
                    }
                }else if(b.isThreeBlock()){
                    bool spawnThreeBlock = Random.Range(0,100) > 50;
                    if(spawnThreeBlock){
                        SpawnThreeBlockBuilding(b.transform);
                    }else{
                        bool spawnTwoBlock = Random.Range(0,100) > 30;
                        if(spawnTwoBlock){
                            SpawnTwoBlockBuilding(b.transform);
                            SpawnSingleBuilding(b.transform,2);
                        }else{
                            SpawnSingleBuilding(b.transform);
                            if(spawnTwoBlock){
                                SpawnTwoBlockBuilding(b.transform,1);
                            }else{
                                SpawnSingleBuilding(b.transform,1);
                                SpawnSingleBuilding(b.transform,2);
                            }
                        }
                    }
                }

            }
        }
    }
    public void SpawnPickup(){
        if(PickupSpawnPoints == null || PickupSpawnPoints.transform.childCount ==0) return;
        int doSpawn =  Random.Range(0, 100);
        if(doSpawn > 25) return;

        int rand = Random.Range(0, PickupSpawnPoints.transform.childCount);
        GameObject pu = (GameObject)Instantiate( TileSpawner.instance.GetRandomPickup(), PickupSpawnPoints.transform.GetChild(rand).position + Vector3.up/2f,PickupSpawnPoints.transform.GetChild(rand).rotation,this.transform);
    }

    public void SpawnCoins(){
    }
    void ClearOnReset(){

    }
    public int GetWaypointCount(){ return waypoints.Count;}
    public void RealignToTile(Tile tile,int end){
        //make list of cars Rel position
        Vector3 MoveAmount = this.transform.position;

        this.transform.position = tile.EndNodes[end].position + (tile.EndNodes[end].forward *50f);

        this.transform.rotation = tile.EndNodes[end].rotation;


        MoveAmount = transform.position - MoveAmount;//find offest amount
        if(sidings[0] != null)
            SelectTileSideType(tile);

        //if not last tile then RECURSION
         if(TileMover.instance.LastTile() != this){
            TileMover.instance.FindTileAfter(this).RealignToTile(this,0);
         }
    }

    bool ChanceOfNewTile(){
        return  Random.Range(0,100) > 60;
    }
    

    TileSiding NewSideing(TileSiding currentSiding){
        int rand = Random.Range(0,100);
        int value =(int)currentSiding;
        if(rand<50){
            value = value-1;
            if(value<0)
                value =3;
        }else{
            value = value+1;
            if(value==4)
                value =0;
        }

        TileSiding newSide  = (TileSiding)value;
        if(newSide == currentSiding  || sidings[value] == null )
            return NewSideing(currentSiding); //try again
        else 
            return newSide;
    }
    void SelectTileSideType(Tile tile){
        if(ChanceOfNewTile()){//switch to new tile type
            SetSiding(NewSideing(tile.siding));
        }else{
            SetSiding(tile.siding);//stay on this tile type
        }
    }
    void AddRemoveSidingViaIndex(int i){
        sidings[i].SetActive(true);
        foreach(GameObject obj in sidings)
            if(obj != null && obj != sidings[i])
                Destroy(obj);
    }
    void PlaceAlternativeBuildings(){
        if(Alt_LeftBuildings != null)
            Destroy(Alt_LeftBuildings);
        if(Alt_RightBuildings != null)
            Destroy(Alt_RightBuildings);
    }
    void KeepBuildings(bool left, bool right){
        if(!left)
        Destroy(LeftBuildings);
        if(!right)
        Destroy(RightBuildings);
    }
    public void SetSiding(TileSiding sidingType){
        if(sidings.Length == 0)
            siding=TileSiding.Ground;
        else{

            if(sidings[(int)sidingType] == null )//if we cant set this tile as this type find new one
                SetSiding(NewSideing(sidingType));
            else{   
                siding = sidingType;
                AddRemoveSidingViaIndex((int)sidingType);

                switch(sidingType){
                    case TileSiding.Ground:
                      //  AddRemoveSidingViaIndex(0);

                        //AllowDisallowBuildings(true,false);

                        if(Alt_RightBuildings != null)
                            Destroy(RightBuildings);
                        if(Alt_LeftBuildings != null)
                            Destroy(LeftBuildings);
                    break;
                    case TileSiding.LeftBeach:
                        //AddRemoveSidingViaIndex(1);
                        
                        //Destroy(LeftBuildings);
                        KeepBuildings(false,true);
                        PlaceAlternativeBuildings();

                    break;
                    case TileSiding.RightBeach:
                       // AddRemoveSidingViaIndex(3);
                        KeepBuildings(true,false);
                        PlaceAlternativeBuildings();

                       // Destroy(RightBuildings);   
                    break;
                    case TileSiding.Beach:
                       // AddRemoveSidingViaIndex(2);
                        
                        KeepBuildings(false,false);
                       
                        PlaceAlternativeBuildings();
                    break;
                }
            }
        }
        SpawnBuildings();
    }
    public void AdjustCars(Vector3 amount){
       foreach(AIDriver car in AICars.Enemies){
            if(car.currentTile == this){
               car.transform.position+= amount;
            }
        }
    }
    //last is most recently added
    public void RealignWithPreviousetile(){

        RealignToTile(TileMover.instance.FindTileAfter(this),0);
    }


    public Transform FindFirstWay(bool onComing){
           // return waypoints[0];

        if(onComing){
            return waypoints[waypoints.Count-1];

        }else{
            return waypoints[0];

         }
    }
    bool hit = false;
    public void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player" && !hit){
            hit = true;
            TileSpawner.instance.AddNewTile();

            PlayerController.instance.HitTile(this);

        }
    }


    //0 = straight 1 == left 2 == right
    public void PlayerIntersectionChange(int dir){
            if(dir != 0){
                RemoveFakes();
                TileMover.instance.FindTileAfter(this).RealignToTile(this,dir);
                TileSpawner.instance.AddTurnAmount(dir);

            }
    }
    void RemoveFakes(){
        foreach(GameObject obj in FakeEnds){
            obj.SetActive(false);
        }
    }
    

   
}
