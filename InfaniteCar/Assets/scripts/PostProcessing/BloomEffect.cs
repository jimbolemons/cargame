using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BloomEffect : PostProcessingEffect
{
	Bloom effect;
	Color idleColor;
	//float initIntensity = 0;

	public override void Init(PostProcessingEffectScriptableObject _script, bool doDestroy){
		base.Init(_script,doDestroy);


		effect = ScriptableObject.CreateInstance<Bloom>();
		effect.enabled.Override(true);

		effect.intensity.Override(0);
		effect.threshold.Override(.2f);
		effect.diffusion.Override(5.3f);

		idleColor = Color.white;
		effect.color.Override(_script.color);

		//SetVolume();
		color = _script.color;
		Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f,effect);
		isInitialized = true;
		
	}
    
    //sclae  0 -1
	public override void SetValues(float scale){


//		Debug.Log("set values");

		effect.intensity.value =Mathf.Clamp((scale *Intensity),0.01f,100);
		Color lerp = Color.Lerp(idleColor,color,Mathf.Clamp(scale,0,1));
		//effect.color.Override(lerp);

	}
}
