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

    public bool DebugKeys = false;
    void FixedUpdate()
    {   
      if (DebugKeys)
        UpdateKeys();
      else if(!DebugKeys)
       UpdateTouch();
    }
    
     void UpdateTouch(){
       
     }
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