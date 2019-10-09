using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChromaEffect : PostProcessingEffect
	{
		Color idleColor;
		ChromaticAberration effect;
		

		public override void Init(PostProcessingEffectScriptableObject _script, bool doDestroy){
			base.Init(_script,doDestroy);
			effect = ScriptableObject.CreateInstance<ChromaticAberration>();

			effect.enabled.Override(true);


			effect.intensity.Override(0);
			effect.enabled.Override(true);
			effect.intensity.Override(0);

			isInitialized = true;
			//volume = ;
			//SetVolume(PostProcessManager.instance.QuickVolume(gameObject.layer, 100f,effect));
			Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f,effect);
		}

	    //sclae  0 -1
		public override void SetValues(float scale){
			effect.intensity.value =Mathf.Clamp((scale *Intensity),0.01f,100);
			

		}
	}
