using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingEffectsManager : MonoBehaviour
{
    
	public static PostProcessingEffectsManager instance;
	public List<PostProcessingEffectScriptableObject> effectScripts = new List<PostProcessingEffectScriptableObject>();
    public Material mat;
    public float boop1;
    public float boop2;
    public float boop3;
    public float boop4;

  public float[] pushVals = {0,0,0,0};

    void Awake(){
		instance = this;
        pushVals = new float[4];
	}

	public float flashLength = .5f;

	void Start(){
		//Flash();
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			Flash();
            Debug.Log("fuck you");
            
		}
        

        for (int i = 0; i < pushVals.Length; i++)
        {
            if (pushVals[i] > 0)
                pushVals[i] *= .98f;
            if (pushVals[i] < 0)
                pushVals[i] *= .98f;

        }
        for (int i = 0; i < pushVals.Length; i++)
        {

            if (pushVals[i] > .01f)
            {
                
                //Debug.Log("i can take x2 damage right now");
                PlayerController.instance.extraDamage = true;
            }
            else if (pushVals[i] < -.01f)
            {
                
                //Debug.Log("i can take x2 damage right now");
                PlayerController.instance.extraDamage = true;
            }
            else {
                PlayerController.instance.extraDamage = false;
            }
            
        }


            mat.SetFloat("_GreenOffsetX", pushVals[0]);
        mat.SetFloat("_GreenOffsetY", pushVals[1]);
        mat.SetFloat("_BlueOffsetX", pushVals[2]);
        mat.SetFloat("_BlueOffsetY", pushVals[3]);

    }

    public void Flash() {    

        for (int i = 0; i < pushVals.Length; i++)
        {
            pushVals[i] -= Random.Range(.1f, -.1f);
        }

    }

	public void Flash2(){
		Debug.Log("flash");
		foreach(PostProcessingEffectScriptableObject ef in effectScripts){
			PostProcessingEffect effect = Instantiate(ef.EffectObject, this.transform.position, Quaternion.identity, this.transform).GetComponent<PostProcessingEffect>();
			effect.Init(ef,true);
			effect.StartFlashReturn(flashLength);
		}


	}
   
    

}
