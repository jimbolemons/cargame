using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TileOptions{
	public string name;
	public GameObject Tile;
	public bool Use = true;
}
public class TileSpawner : MonoBehaviour
{

	public static TileSpawner instance;

	 [SerializeField]
	public List<TileOptions> TileTypes = new List<TileOptions>();
	TileMover mover;

	int segments = 7;
    public int turns = 0;
    public List<GameObject> pickups = new List<GameObject>();
    public TileSpawnerScriptableObject spawnManagerValues;

    public List<GameObject> OneBlockBuildings = new List<GameObject>();
    public List<GameObject> TwoBlockBuildings = new List<GameObject>();
    public List<GameObject> ThreeBlockBuildings = new List<GameObject>();



	void Awake(){
		instance = this;
	}
    // Start is called before the first frame update
    void Start()
    {
        pickups = new List<GameObject>(Resources.LoadAll<GameObject>("Pickups"));

    	mover = GetComponent<TileMover>();
        SpawnFirstTiles();

    }

    public GameObject GetOneBlockBuilding(){
       return OneBlockBuildings[Random.Range(0,OneBlockBuildings.Count)];
    }
    public GameObject GetTwoBlockBuilding(){
       return TwoBlockBuildings[Random.Range(0,TwoBlockBuildings.Count)];
    }
    public GameObject GetThreeBlockBuilding(){
       return ThreeBlockBuildings[Random.Range(0,ThreeBlockBuildings.Count)];
    }


    public GameObject GetRandomPickup(){
    	return pickups[Random.Range(0, pickups.Count)];
        //return pickups[0];

    }
    void SpawnFirstTiles(){
    	for(int i = 0; i <segments;i ++){
    		Tile obj = Instantiate(TileTypes[0].Tile, new Vector3(0,0,(i*50)-100),Quaternion.identity,this.transform).GetComponent<Tile>();
    		mover.Tiles.Add(obj);
            obj.SetSiding(TileSiding.Beach);

    	}
        PlayerController.instance.Init();

    }

     public void AddNewTile(){

        GameObject pref = FindNextTileType();
    	Tile obj = Instantiate(pref, Vector3.one * 100f ,Quaternion.identity,this.transform).GetComponent<Tile>();
     
       	obj.SpawnPickup();
        //obj.SpawnCoins();

       	mover.Tiles.Add(obj);
       	mover.TilesToRemove.Add( mover.Tiles[0]);

        obj.RealignToTile(mover.FindTileBefore(obj),0);
        
    }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }

     public void AddTurnAmount(int dir){
     	Debug.Log("Add turn amount");
        if(dir==1){
            turns --;
        }else if(dir==2){
            turns ++;
        }
    }
    public int TilesSinceInter =3;
    public int TilesSinceTurn =0;

    GameObject FindNextTileType(){
        int rand = Random.Range(0,TileTypes.Count);
       if(!TileTypes[rand].Use )
            return FindNextTileType();

        if(TileTypes[rand].Tile.GetComponent<Tile>().isIntersection){
            if(TilesSinceInter <10){
                return FindNextTileType();
            }else if( TilesSinceInter > 10){
                TilesSinceInter =0;
                TilesSinceTurn++;
                return TileTypes[rand].Tile;
            }
        } 



        if( TileTypes[rand].Tile.GetComponent<Tile>().direction == TileDirection.right  ){
            if(turns>=1 || TilesSinceTurn < 2){
                return FindNextTileType();
            }else{
                TilesSinceInter ++;
                TilesSinceTurn =0;
                turns ++;
            }
        }else if(TileTypes[rand].Tile.GetComponent<Tile>().direction == TileDirection.left ){
            if(turns<=-1 || TilesSinceTurn < 2){
                return FindNextTileType();
            }else{
                TilesSinceInter ++;
                TilesSinceTurn =0;
                turns --;
            }
        }

       

        return TileTypes[rand].Tile;

    }
}
