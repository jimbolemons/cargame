using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeLightController : MonoBehaviour
{
    MeshRenderer rend;
	PlayerInput input;
    
    void Start(){
        input = PlayerInput.instance;

    	rend = this.GetComponent<MeshRenderer>();
    }
    bool active = false;
    // Update is called once per frame
    void Update()
    {
        if(input==null)return;
        if(input.Down() && !active){
        	active=true;
        	rend.material.SetColor("_AdditionalColor", Color.white);
        }else if(!input.Down() && active){
        	active = false;
        	rend.material.SetColor("_AdditionalColor", Color.grey);

        }
        // if(input.Down()){
        // 	active=true;
        // 	rend.material.SetColor("_AdditionalColor", Color.white);
        // }else {
        // 	active = false;
        // 	rend.material.SetColor("_AdditionalColor", Color.grey);

        // }
    }
}
