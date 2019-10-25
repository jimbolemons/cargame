using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTilting : MonoBehaviour
{
	PlayerInput input;
	float rotataion = 180;
    float maxAngle = 35;
    float turnAngle = 0;
    float turn2 = 0;
    public float turnSpeed;
    GameManager GM;
    CarDataScriptableObject data;
    TileMover tile;
    void Start()
    {
        GM = GameManager.instance;
        input = PlayerInput.instance;
        data = CarMovement.instance.CarData;
        tile = TileMover.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GM.GameRunning()) return;


       // Debug.Log(turn2);

        if(input.Right() && turnAngle < maxAngle && input.Down() == false && !input.Left())
        {
            turnAngle +=data.Grip * Time.fixedDeltaTime * turnSpeed;
            turn2 = turnAngle / 50;

            if (turn2 >= .1f)
                turn2 = .1f;

            if (turn2 > .05f) 
                tile.BumpLeft(turn2 / 5);
        }
        if (input.Left() && input.Down() == false && turnAngle > -maxAngle && !input.Right()){
           turnAngle -= data.Grip * Time.fixedDeltaTime * turnSpeed;
            turn2 = turnAngle / 50;

            if (turn2 <= -.1f)
                turn2 = -.1f;

            if (turn2 < -.05f) 
                tile.BumpLeft(turn2 / 5);          

        }

        if(!input.Right() && !input.Left() ){
            //if not turning apply turn angle to car/camera
            turnAngle *= .9f;
            turn2 = 0;
        }

        this.transform.localRotation = Quaternion.Euler(new Vector3(0,turnAngle,0));
       
        PlayerController.instance.SetTurnAngle(turnAngle,maxAngle);
    }
}
