using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
	

    public AICarsManager AICars;
   // public EventManager eventManager;
    //public bool override
    // Start is called before the first frame update
    public bool PlayerAlive = false;
    public bool GameStarted = false;
    int CoinsCollected = 0;
    void Awake()
    {
        instance = this;
        AICars= this.GetComponent<AICarsManager>();
        EventManager.OnPlayerDied += OnPlayerDied;
        EventManager.OnGameStart += StartGame;
        EventManager.OnResumeAftervideo += OnResumeAftervideo;
        
    }
    void OnDisable(){
        EventManager.OnPlayerDied -= OnPlayerDied;
        EventManager.OnGameStart -= StartGame;
    }
    void OnPlayerDied(){
        PlayerAlive = false;
    }

    public void SetCoinsCollected(int i){
        CoinsCollected =i;
    }
    public int GetCoinsCollected(){return CoinsCollected;}
    public void ResetGame(){
        //resest scores pickups 
        CoinsCollected=0;
    }
    public void StartGame(){
        //start game running 
        GameStarted = true;
        PlayerAlive =true;
        
    }
    void OnResumeAftervideo(){
        StartGame();
    }
    public bool GameRunning(){return PlayerAlive && GameStarted;}
}
