using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;

    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;
    bool action = false;
    bool actionAlt = false;

    public bool Up() { return up; }
    public bool Down() { return down; }
    public bool Left() { return left; }
    public bool Right() { return right; }
    public bool Action() { return action; }
    public bool ActionAlt() { return actionAlt; }

    bool upPress = false;
    bool downPress = false;
    bool leftPress = false;
    bool rightPress = false;
    bool actionPress = false;
    bool actionAltPress = false;

    public bool UpPress() { return upPress; }
    public bool DownPress() { return downPress; }
    public bool LeftPress() { return leftPress; }
    public bool RightPress() { return rightPress; }
    public bool ActionPress() { return actionPress; }
    public bool ActionAltPress() { return actionAltPress; }

    bool upRelease = false;
    bool downRelease = false;
    bool leftRelease = false;
    bool rightRelease = false;
    bool actionRelease = false;
    bool actionAltRelease = false;

    public bool UpRelease() { return upRelease; }
    public bool DownRelease() { return downRelease; }
    public bool LeftRelease() { return leftRelease; }
    public bool RightRelease() { return rightRelease; }
    public bool ActionRelease() { return actionRelease; }
    public bool ActionAltRelease() { return actionAltRelease; }

    void Awake()
    {
        instance = this;
    }
    public void SetLeftDown(){
        left=true;
        leftPress = true;
    }
    public void SetLeftUp(){
        left=false;
        leftRelease = true;
    }

    public void SetRightDown(){
        right=true;
        rightPress = true;
    }
    public void SetRightUp(){
        right=false;
        rightRelease = true;
    }
    public void SetDownPress(){
        down=true;
        downPress = true;
    }
    public void SetDownRelease(){
        down=false;
        downRelease = true;
    }
    public void SetActionDown(){
        action=true;
        actionPress = true;
    }
    public void SetActionUp(){
        action=false;
        actionRelease = true;
    }

    //public bool DebugKeys = false;
    void FixedUpdate()
    {   
       #if UNITY_EDITOR
        UpdateKeys();
        #endif
       // UpdateTouch();
    }
    // void UpdateTouch(){
    //     left =false;
    //     right= false;
    //     down = false;


        
    //     //down = left&& right;
    //     if(Input.touchCount ==1 ){
    //         Touch touch = Input.GetTouch(0);
    //         if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))return;

    //         float pos = touch.position.x;
    //         if(pos > Screen.width/2f){
    //             right = true;
    //         }else {
    //             left = true;
    //         }
    //     }else if(Input.touchCount >1){
    //         Touch touchA = Input.GetTouch(0);
    //         Touch touchB = Input.GetTouch(1);
    //         if(EventSystem.current.IsPointerOverGameObject(touchA.fingerId))return;
    //         if(EventSystem.current.IsPointerOverGameObject(touchB.fingerId))return;

    //         float posA = touchA.position.x;
    //         float posB = touchB.position.x;
    //         if((posA > Screen.width/2f && posB < Screen.width/2f ) ||   (posA < Screen.width/2f && posB > Screen.width/2f )){
    //             down = true;
    //         }else if(posA > Screen.width/2f && posB > Screen.width/2f){
       
    //             right = true;

    //         }else if(posA < Screen.width/2f && posB < Screen.width/2f){
    //             left = true;

    //         }
    //     }
    // }
    void UpdateKeys(){

        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        action = Input.GetKey(KeyCode.Space);
        actionAlt = Input.GetKey(KeyCode.LeftShift);

        upRelease = Input.GetKeyUp(KeyCode.W);
        downRelease = Input.GetKeyUp(KeyCode.S);
        leftRelease = Input.GetKeyUp(KeyCode.A);
        rightRelease = Input.GetKeyUp(KeyCode.D);
        actionRelease = Input.GetKeyUp(KeyCode.Space);
        actionAltRelease = Input.GetKeyUp(KeyCode.LeftShift);

        upPress = Input.GetKeyDown(KeyCode.W);
        downPress = Input.GetKeyDown(KeyCode.S);
        leftPress = Input.GetKeyDown(KeyCode.A);
        rightPress = Input.GetKeyDown(KeyCode.D);
        actionPress = Input.GetKeyDown(KeyCode.Space);
        actionAltPress = Input.GetKeyDown(KeyCode.LeftShift);

    }
    void LateUpdate(){
        leftPress = false;
        leftRelease = false;
        rightPress = false;
        rightRelease = false;
        actionPress = false;
        actionRelease = false;
        downPress = false;
        downRelease = false;

    }
}