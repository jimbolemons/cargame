using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathPanelManager : MonoBehaviour
{
    public TextMeshProUGUI CoinsText;
    // GameManager GM;
    // // Start is called before the first frame update
    // void Start()
    // {
    // 	GM = GameManager.instance;
    //     EventManager.OnPlayerDied += OnPlayerDied;
    // }
   // bool active = false;
    private void Start()
    {
        EventManager.OnPlayerDied += OnPlayerDied;
    }
    

    
   public void SetScore(int score){
        //Debug.Log("Player died on death panel");
    	CoinsText.text = score.ToString("00");
    }
    void OnPlayerDied()
    {
       

    }
    
}
