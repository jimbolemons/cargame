using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform IdealPosition;
	public Transform LookTarget;
    GameObject CameraObj;
    public bool CameraIsShaking = false;
    float TurnSpeed = 1f;
    float ShakeSpeedReductionRate = 10;
    void Start(){
        CameraObj = this.transform.GetChild(0).gameObject;
    }
    float hopDistance = 10f;

    float shakeSpeed =3;

    Coroutine Routine;

    // Update is called once per frame
    void FixedUpdate()
    {
    	float distance = Vector3.Distance(transform.position, IdealPosition.position);
    	if(distance <2.5f && TurnSpeed >1.5f){
			TurnSpeed -=Time.fixedDeltaTime*10f;
    	}else if(distance >2.5f && TurnSpeed < 20f){
			TurnSpeed +=Time.fixedDeltaTime*10f;

    	}
        this.transform.position = Vector3.Slerp(transform.position, IdealPosition.position,Time.fixedDeltaTime*TurnSpeed);
    	
        transform.LookAt(LookTarget);



        //if(Input.GetKeyUp(KeyCode.S)) CameraShake(.25f,50f);

        if(CameraIsShaking){
            if(CameraObj.transform.localPosition.magnitude < .5f)
                CameraObj.transform.localPosition = new Vector3(RandomHopValue(),RandomHopValue(),RandomHopValue());
        }


        if(shakeSpeed >1)
            shakeSpeed-=Time.fixedDeltaTime * ShakeSpeedReductionRate;


        CameraObj.transform.localPosition = Vector3.Lerp(CameraObj.transform.localPosition, Vector3.zero,Time.fixedDeltaTime*shakeSpeed);

    }
    float RandomHopValue(){
        return Random.Range(-hopDistance,hopDistance);
    }
    public void CameraShake(float time, float intensity){
      Routine =StartCoroutine(ShakeEvent(time,intensity));
      
    }

    IEnumerator ShakeEvent(float duration, float intensity){
             
        float time = 0;
        StartShake(intensity);
        while(time<duration){
            time += Time.fixedDeltaTime;
            yield return new WaitForSeconds(.01f);
         }
        
        EndShake();
    }
    void StartShake(float intensity){
        shakeSpeed =intensity;
        CameraIsShaking=true;
    }
    void EndShake(){
        CameraIsShaking=false;

    }


}
