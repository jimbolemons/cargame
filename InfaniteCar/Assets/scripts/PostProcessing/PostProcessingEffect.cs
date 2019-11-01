using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


// [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PostProcessingEffect", order = 1)]
// public class PostProcessingEffectScriptableObject : ScriptableObject
// {
//     public float intensity = 1f;
//     public Color color = Color.white;
//     public GameObject EffectObject;

// }
//[Serialiazable]
public class PostProcessingEffect : MonoBehaviour
{
	PostProcessingEffectScriptableObject script;
	
	public bool isInitialized{ set{initialized = value;} get{return initialized;}}
	public float Intensity{ set{script.intensity = value;} get{return script.intensity;}}
	public Color color{set{script.color=value;} get{return script.color; }}
	public PostProcessVolume Volume{set{volume=value;} get{return volume; }}



	bool initialized = false;
	PostProcessVolume  volume;
	Coroutine routine;
	public Coroutine Routine{ set{routine = value;} get{return routine;}}
	//bool test =false;
	bool destroy = false;
	public virtual void Init(PostProcessingEffectScriptableObject _script,bool doDestroy){
		destroy= doDestroy;
		script = _script;


	}
    
	//public void SetVolume(PostProcessVolume v){ volume =v;}
	//public void SetColor(Color c){ color =c;}

    public virtual void TurnOn(float length){
    	if(Routine!=null)
			StopCoroutine(Routine);
		Routine=	StartCoroutine(TurnOnRoutine(length));
	}


    public virtual void TurnOff(float length){
    	if(Routine!=null)
			StopCoroutine(Routine);
		Routine=	StartCoroutine(TurnOffRoutine(length));
	}


	public virtual IEnumerator TurnOnRoutine(float length){
		float time = 0;
		while(time<length){
			 time += Time.fixedDeltaTime;
			 SetValues(time/length);
			 yield return new WaitForSeconds(.01f);
		}
    }
    public virtual IEnumerator TurnOffRoutine(float length){
    	float time = 0;
		//float scale=0;
    	while(time<length){
			time += Time.fixedDeltaTime;
			 SetValues(1f-( time/length ));

			yield return new WaitForSeconds(.01f);
		
		}
		End();
    }


    public virtual void StartFlashReturn(float length){
    	if(Routine!=null)
			StopCoroutine(Routine);
		Routine =StartCoroutine(FlashReturn(length));
		

    }
    public virtual  IEnumerator FlashReturn(float length){
    	float time = 0;
		while(time<length){
			 time += Time.fixedDeltaTime;
			 //Debug.Log("Flash return " + time);
			 SetValues(1f-( time/length ));
			 yield return new WaitForSeconds(.01f);

		}
		End();
    }



	public virtual void StartFlash(float length){
		if(Routine!=null)
			StopCoroutine(Routine);
		Routine =StartCoroutine(FlashRoutine(length));
	}	
	public virtual IEnumerator FlashRoutine(float length){
		float time = 0;
		float third = length*.33f;
		//float scale=0;
		while(time<third){
			 time += Time.fixedDeltaTime;
			 SetValues(time/third);

			 yield return new WaitForSeconds(.01f);
		}
		while(time > third  && time < length){
			time += Time.fixedDeltaTime;
			 SetValues(time/third);

			 yield return new WaitForSeconds(.01f);
		}	
		End();
	}

	public virtual void SetValues(float scale){}
    public virtual void End(){
		if(Routine!=null)
			StopCoroutine(Routine);
			if(volume != null)
		 RuntimeUtilities.DestroyVolume(volume, true, true);
		if(destroy)
			Destroy(this.gameObject);
	}
}




	

