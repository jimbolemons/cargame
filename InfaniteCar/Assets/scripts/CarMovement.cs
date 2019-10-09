using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public static CarMovement instance;
	PlayerInput input;
	float movement = .25f;
	float width = 15;

    float rotateSpeed = 1.5f;
    float baseRotationSpeed= 1.5f;

    float IncreaseScale = 2.5f;
    // //Tilting
    // float rotation = 180;
    // float maxAngle = 20;
    // float turnAngle = 0;

    public delegate void SkidEvent();
    public static event SkidEvent OnStartSkid;
    public static event SkidEvent OnStopSkid;

    public Transform BackLeftTire, BackRightTire;
    GameManager GM;
    public CarDataScriptableObject CarData;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Awake(){
        instance = this;
    }
    void Start()
    {
        GM = GameManager.instance;

        input = PlayerInput.instance;
        playerData = PlayerData.instance;
        CarData = playerData.currentSelection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GM.GameRunning()) return;
      
        HandleRotation();
       // HandleTilting();
    }
    void HandleRotation(){

        if(input.Left()){
            rotateSpeed += Time.fixedDeltaTime*CarData.Grip /15f;
            this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, this.transform.eulerAngles - new Vector3(0,rotateSpeed,0) , 1);
            Skid();
        }else if (input.Right() ){
            rotateSpeed += Time.fixedDeltaTime*CarData.Grip/15f;

            this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, this.transform.eulerAngles + new Vector3(0,rotateSpeed,0) , 1);
            Skid();

        }else if(rotateSpeed> baseRotationSpeed){
            rotateSpeed-= Time.fixedDeltaTime*CarData.Grip*4f;
            //Skid();
        }else{
            StopSkid();
        }

    }
    bool skidding = true;
    void Skid(){
        if(!skidding){
            skidding = true;
            if(OnStartSkid !=null)
                OnStartSkid();
        }
    }
    void StopSkid(){
         if(skidding){
            skidding = false;
            if(OnStopSkid !=null)
                OnStopSkid();

         }


         
    }
    // void HandleTilting(){
    //     if(input.Right() && turnAngle < 25){
    //         turnAngle +=1.75f;
    //     }else if(input.Left()&& turnAngle > -25){
    //         turnAngle -=1.75f;

    //     }else 
    //         turnAngle *= .95f;

    //     this.transform.localRotation = Quaternion.Euler(new Vector3(0,turnAngle,0));

    // }
}
