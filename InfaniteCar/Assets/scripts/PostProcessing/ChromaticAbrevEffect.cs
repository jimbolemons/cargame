using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class ChromaticAbrevEffect : MonoBehaviour {
PostProcessVolume  volume;
ChromaticAberration effect;
private float intensity = 1f;
public bool test = false;

Coroutine Routine;
public bool isInitialized{ set{initialized = value;} get{return initialized;}}
	bool initialized = false;
	public void Init(float _intensity){
		effect = ScriptableObject.CreateInstance<ChromaticAberration>();
		effect.enabled.Override(true);
		intensity=_intensity;
		effect.intensity.Override(0);

		initialized=true;
		//StartFlash(length);
	}
	void Update(){
		if(test){
			Init(1);
			test = false;
			StartFlash(1);
		}
	}
	public void TurnOn(float length){
		if(Routine!=null)
			StopCoroutine(Routine);
		Routine =StartCoroutine(TurnOnRoutine(length));}
    public void TurnOff(float length){
    	if(Routine!=null)
			StopCoroutine(Routine);
    	Routine =StartCoroutine(TurnOffRoutine(length));}
    IEnumerator TurnOnRoutine(float length){
		volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f,effect);
		float time = 0;

		float scale=0;
		while(time<length){
			 time += Time.fixedDeltaTime;
			  scale = (time/length);
			effect.intensity.value =.03f+ ((scale) *intensity);
			 yield return new WaitForSeconds(.01f);
		}
    }
    IEnumerator TurnOffRoutine(float length){
    	float time = 0;

		float scale=0;
    	while(time<length){
			time += Time.fixedDeltaTime;
			// Debug.Log("B" +time);
			 scale = (time/length);
			effect.intensity.value =Mathf.Clamp( .03f+((1f-scale) *intensity),0.01f,1000);
			yield return new WaitForSeconds(.01f);
		
		}
			End();
    }
    public void StartFlashReturn(float length){
		Routine =StartCoroutine(FlashReturn(length));
		
		
    }
	IEnumerator FlashReturn(float length){
float time = 0;

		volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f,effect);
			  effect.intensity.value =Mathf.Clamp((intensity),0,100);

		while(time<length){
			 time += Time.fixedDeltaTime;
			 effect.intensity.value =Mathf.Clamp( (1f-(time/length)) * intensity ,0.01f,100);

			 yield return new WaitForSeconds(.01f);

		}
		End();
	}

	public void StartFlash(float length){
		//if(Routine!=null)
			//StopCoroutine(Routine);
		Routine =StartCoroutine(FlashRoutine(length));
	}	
	IEnumerator FlashRoutine(float length){
		volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f,effect);
		float time = 0;
		float half = length/2f;
		float third = length*.33f;
		float two_Third = length*.66f;

		float scale=0;
		while(time<third){
			 time += Time.fixedDeltaTime;
			//Debug.Log("A "+time);
			 scale = (time/third);
			 effect.intensity.value = ((time/third) *intensity);
			 yield return new WaitForSeconds(.01f);
		}
		while(time > third  && time < length){
			time += Time.fixedDeltaTime;
			// Debug.Log("B" +time);
			 scale = ((time-third)/two_Third);
			effect.intensity.value =Mathf.Clamp( ((1f-((time-third)/two_Third)) *intensity),0.01f,1000);
			 yield return new WaitForSeconds(.01f);
		}	
		End();
	}
	public void End(){
		if(Routine!=null)
			StopCoroutine(Routine);
		 RuntimeUtilities.DestroyVolume(volume, true, true);
		 //Destroy(this.GetComponent<ChromaticAbrevEffect>());
	}
    // void OnDestroy()
    // {
    //     RuntimeUtilities.DestroyVolume(volume, true, true);
    // }
}


