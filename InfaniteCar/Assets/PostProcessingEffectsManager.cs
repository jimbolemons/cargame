using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingEffectsManager : MonoBehaviour
{
    
	public static PostProcessingEffectsManager instance;
	public List<PostProcessingEffectScriptableObject> effectScripts = new List<PostProcessingEffectScriptableObject>();
	void Awake(){
		instance = this;
	}

	public float flashLength = .5f;

	void Start(){
		//Flash();
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.O)){
			Flash();
		}
	}

	public void Flash(){
		Debug.Log("flash");
		foreach(PostProcessingEffectScriptableObject ef in effectScripts){
			PostProcessingEffect effect = Instantiate(ef.EffectObject, this.transform.position, Quaternion.identity, this.transform).GetComponent<PostProcessingEffect>();
			effect.Init(ef,true);
			effect.StartFlashReturn(flashLength);
		}


	}


}
