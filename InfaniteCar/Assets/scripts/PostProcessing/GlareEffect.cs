using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class GlareEffect : MonoBehaviour {
PostProcessVolume  volume;
Bloom m_Bloom;
private Color idleColor = new Color(.5f,.5f,.75f);
public bool test = false;

public Color color = new Color(.5f,.5f,.75f);

private float intensity = 1f;
float initIntensity = 0;
public bool isInitialized{ set{initialized = value;} get{return initialized;}}
	bool initialized = false;
	public void Init(float _Intensity,Color clr){


		m_Bloom = ScriptableObject.CreateInstance<Bloom>();
		m_Bloom.enabled.Override(true);
		color =clr;
		intensity = _Intensity;

		initIntensity = m_Bloom.intensity;//.Override(0);
		m_Bloom.intensity.Override(0);

		m_Bloom.threshold.Override(.2f);

		m_Bloom.diffusion.Override(5.3f);
		m_Bloom.color.Override(clr);
		
		initialized = true;

		
	}



	Coroutine Routine;

	public void Update(){
		//Init(50);
		if(test){
			test = false;
			StartFlash(1f);
		}
	}
    public void TurnOn(float length){
    	if(Routine!=null)
			StopCoroutine(Routine);
		Routine=	StartCoroutine(TurnOnRoutine(length));
	}


    public void TurnOff(float length){
    	if(Routine!=null)
			StopCoroutine(Routine);
		Routine=	StartCoroutine(TurnOffRoutine(length));
	}


    IEnumerator TurnOnRoutine(float length){
		volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f,m_Bloom);
		float time = 0;

		float scale=0;
		while(time<length){
			 time += Time.fixedDeltaTime;
			 scale = (time/length);
			 m_Bloom.intensity.value =initIntensity+((time/length) *intensity);
			 //Color lerp = Color.Lerp(idleColor,Color.white,Mathf.Clamp(scale-.25f,0,1));
			 yield return new WaitForSeconds(.01f);
		}
    }
    IEnumerator TurnOffRoutine(float length){
    	float time = 0;

		float scale=0;
    	while(time<length){
			time += Time.fixedDeltaTime;
			 scale = (time/length);
			//Color lerp = Color.Lerp(Color.white,idleColor,Mathf.Clamp(scale-.25f,0,1));
			m_Bloom.intensity.value =initIntensity+((1f-scale) *intensity);
			yield return new WaitForSeconds(.01f);
		
		}
			End();
    }
    public void StartFlashReturn(float length){
		Routine =StartCoroutine(FlashReturn(length));
		

    }
    IEnumerator FlashReturn(float length){
    	float time = 0;

		volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f,m_Bloom);
			 m_Bloom.intensity.value =Mathf.Clamp(initIntensity+(intensity),0.01f,100);

		while(time<length){
			 time += Time.fixedDeltaTime;
			 m_Bloom.intensity.value =Mathf.Clamp(initIntensity+ (1f-(time/length)) * intensity ,0.01f,100);

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
		volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f,m_Bloom);
		float time = 0;
		float half = length/2f;
		float third = length*.33f;
		float two_Third = length*.66f;
		Debug.Log("start intensity " + intensity);

		float scale=0;
		while(time<third){
			 time += Time.fixedDeltaTime;
			 scale = (time/third);
			 m_Bloom.intensity.value =Mathf.Clamp(initIntensity+((time/third) *intensity),0.01f,100);

			 Debug.Log("up  "+ intensity + "  " +initIntensity +"  " +(time/third) +"  " + m_Bloom.intensity.value);
			 Color lerp = Color.Lerp(idleColor,color,Mathf.Clamp(scale-.25f,0,1));
			//m_Bloom.color.Override(lerp);
			 yield return new WaitForSeconds(.01f);
		}
		while(time > third  && time < length){
			time += Time.fixedDeltaTime;
			scale = ((time-third)/two_Third);
			Color lerp = Color.Lerp(color,idleColor,Mathf.Clamp(scale-.25f,0,1));

			//m_Bloom.color.Override(lerp);
			m_Bloom.intensity.value =Mathf.Clamp(initIntensity+ ((1f-((time-third)/two_Third)) *intensity),0.01f,100);
			// m_Bloom.intensity.Override((1f-((time-third)/two_Third)) *intensity);
			 Debug.Log("Down " +m_Bloom.intensity.value);

			 yield return new WaitForSeconds(.01f);
		}	
		End();
	}
	public void End(){
		if(Routine!=null)
			StopCoroutine(Routine);
		 RuntimeUtilities.DestroyVolume(volume, true, true);
		// Destroy(this);
	}
    // void OnDestroy()
    // {
    //     RuntimeUtilities.DestroyVolume(volume, true, true);
    // }
}
