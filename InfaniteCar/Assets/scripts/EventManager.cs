using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
	public static EventManager instance;

	public delegate void GameEventAction();
    public static event GameEventAction OnGameStart;

    public static event GameEventAction OnPlayerDied;

    public static event GameEventAction OnGameReset;

    public static event GameEventAction OnBackToMenu;

    public static event GameEventAction OnResumeAftervideo;

    



    void Awake(){
    	instance = this;
    }
    public void PlayerDied(){
        Debug.Log("<color=red>Player died</color>");
        if(OnPlayerDied != null)
            OnPlayerDied();
        /*
          */
    }

    public void StartGame(){
        if(OnGameStart != null)
            OnGameStart();
        /*
          */
    }


    public void GameReset(){
        if(OnGameReset != null)
            OnGameReset();
        /*
          */
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1;

        if (OnBackToMenu != null)
            OnBackToMenu();

        
        /*
          */

    }


    public void ResumeGameAftervideo(){
        if(OnResumeAftervideo != null)
            OnResumeAftervideo();
        /*
          */
    }
}
