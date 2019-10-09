using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{

	public GameObject deathPanel;
	public GameObject startPanel;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnPlayerDied += OnPlayerDied;
        EventManager.OnGameStart += OnGameStart;
        EventManager.OnGameReset += OnGameReset;
        EventManager.OnResumeAftervideo += OnResumeAftervideo;


        OnGameReset();//to get panels on
    }

   
    void OnPlayerDied(){
    	deathPanel.SetActive(true);
        deathPanel.GetComponent<DeathPanelManager>().SetScore(GameManager.instance.GetCoinsCollected());
    }
    void OnGameStart(){
    	startPanel.SetActive(false);
    }
    void OnGameReset(){
    	startPanel.SetActive(true);
    	deathPanel.SetActive(false);

    }
    void OnResumeAftervideo(){
        deathPanel.SetActive(false);
        
    }
}
