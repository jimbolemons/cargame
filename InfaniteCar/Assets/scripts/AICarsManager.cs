using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarsManager : MonoBehaviour
{
	public GameObject othervehicle;
    public GameObject PoliceVehicle;

	public List<AIDriver> Enemies = new List<AIDriver>();
    List<AIDriver> EnemiesToRemove = new List<AIDriver>();
    public List<GameObject> CarPre = new List<GameObject>();
    public Transform enemyNearSpawnPoint;
	public Transform enemyFarSpawnPoint;

	bool OnComing = false;
	float LastSpawnTime =0;
    float LastPoliceTime =0;
    public static bool policeSpawned = false;
    AIDriver police;
	GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;
        EventManager.OnGameReset += OnGameReset;
        EventManager.OnResumeAftervideo += OnResumeAftervideo;
        EventManager.OnBackToMenu += OnBackToMenu;


    }
    void OnBackToMenu()
    {
        policeSpawned = false;
    }

    void OnGameReset(){
    	ClearAll();
        policeSpawned = false;
    }

    void OnResumeAftervideo(){        
        ClearAll();
        policeSpawned = false; 
    }

    void ClearAll(){
        ClearPolice();
        ClearCars();
    }
    void ClearPolice(){
        //if(police != null && police.gameObject!= null)
       // Destroy(police.gameObject);
        policeSpawned=false;
    }
    void ClearCars(){
        foreach (AIDriver obj in Enemies)
        {
            if(obj != null)
            Destroy(obj.gameObject);
        }
        Enemies.Clear();
        EnemiesToRemove.Clear();
    }
    void FixedUpdate()
    {
    	if(GM.GameRunning()){
            

	       if(Input.GetKeyUp(KeyCode.Space) ){
	      	//SpawnPC();
               // SpawnPolice();
            }
	       if (Time.time - LastSpawnTime > 1)
	        {
	            SpawnPC();
	            LastSpawnTime = Time.time;
	        }
           //if ((Time.time - LastPoliceTime) > 5)
            if(!policeSpawned)
            {
                SpawnPolice();
                policeSpawned=true;
                //LastPoliceTime = Time.time;
            }

    	}
        UpdateEnemiesToBeRemoved();
        
    }
    void SpawnPC(){
        int i = Random.Range(0,CarPre.Count);
        othervehicle = CarPre[i];

    	AIDriver en = (AIDriver)Instantiate(othervehicle, Vector3.one*-50f, Quaternion.identity).GetComponent<AIDriver>();   	
    	en.gameObject.transform.localScale = Vector3.one *.75f;
    	Enemies.Add(en);
        en.Init(OnComing);
        OnComing = !OnComing;
    }
     void SpawnPolice(){
        Debug.Log("Police spawned");
        police = (AIDriver)Instantiate(PoliceVehicle, Vector3.one * -50f, Quaternion.identity).GetComponent<AIDriver>();
        police.gameObject.transform.localScale = Vector3.one * .75f;
        Enemies.Add(police);
        police.InitPolice();
    }
    public void UpdateEnemiesToBeRemoved()
    {
        if (EnemiesToRemove.Count > 0)
        {
            //destroys all bullets that are in the bullets to remove list 
            foreach (AIDriver obj in EnemiesToRemove)
            {
        //Debug.Log("Remove and destroy Driver");

                Enemies.Remove(obj);
                Destroy(obj.gameObject);
            }
            //clears bullets to remove after destorying all bullets
            EnemiesToRemove.Clear();
        }
    }
    public void RemoveDriver(AIDriver driver,string s = null)
    {
       // if(s != null)
//            Debug.Log(s);
       
        if(Enemies.Contains(driver) && !EnemiesToRemove.Contains(driver))
        EnemiesToRemove.Add(driver);
//        Debug.Log(driver);
    }
}
