﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanal : MonoBehaviour
{
    bool active = false;
    public GameObject GO; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TogglePanal()
    {
        active = !active;
        this.transform.GetChild(0).gameObject.SetActive(active);
        if(active)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;



    }
}
