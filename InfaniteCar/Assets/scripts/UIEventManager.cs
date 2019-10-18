using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEventManager : MonoBehaviour
{

	public GameObject deathPanel;
	public GameObject startPanel;
    public GameObject mainCanvas;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnPlayerDied += OnPlayerDied;
        EventManager.OnGameStart += OnGameStart;
        EventManager.OnGameReset += OnGameReset;
        EventManager.OnBackToMenu += OnBackToMenu;
        EventManager.OnResumeAftervideo += OnResumeAftervideo;
       // deathPanel = GameObject.Find("DeathPanel");


        OnGameReset();//to get panels on
    }

   
    void OnPlayerDied(){
       // if (deathPanel == null)
        //deathPanel = GameObject.Find("DeathPanel");
    	deathPanel.SetActive(true);
        deathPanel.GetComponent<DeathPanelManager>().SetScore(GameManager.instance.GetCoinsCollected());
    }
    void OnGameStart(){
       if(startPanel != null)
    	startPanel.SetActive(false);
        if (Time.timeScale < 1)
            Time.timeScale = 1;
    }
    void OnGameReset(){
        mainCanvas.SetActive(true);
        startPanel.SetActive(true);
    	deathPanel.SetActive(false);

    }
    void OnBackToMenu()
    {
        mainCanvas.SetActive(false);
        startPanel.SetActive(true);
        deathPanel.SetActive(false);
       

    }
    void OnResumeAftervideo(){
        deathPanel.SetActive(false);
        
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCanvas.SetActive(true);
    }

}
