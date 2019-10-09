using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGlare : MonoBehaviour {

	public GlareEffect glare;
	public bool useGlare;
	public ChromaticAbrevEffect ChromaticAberr;
	public bool useChroma;

	public float flashLength = .25f;
	public float BloomIntensity = 2f;
	public float ChromaticIntensity = .01f;

	void Start(){
		//Flash();
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.O)){
			Flash();
		}
	}

	public void Flash(){
		if(useGlare ){
			if( glare==null)
	       		glare = this.gameObject.AddComponent<GlareEffect>();
	       	if(!glare.isInitialized)
	       		glare.Init(BloomIntensity,Color.white);
			glare.StartFlashReturn(flashLength);

	      }
	    if(useChroma  ){
	    	if(ChromaticAberr==null)
	       		ChromaticAberr = this.gameObject.AddComponent<ChromaticAbrevEffect>();
	       	if(!ChromaticAberr.isInitialized)
	       		ChromaticAberr.Init(ChromaticIntensity);
			ChromaticAberr.StartFlashReturn(flashLength);

	    }



	}


	public void StartFlash(){
		
		if(glare==null){
	       	glare = this.gameObject.AddComponent<GlareEffect>();
	       	glare.Init(BloomIntensity,Color.white);
	     }
	    if(ChromaticAberr==null){
	       	ChromaticAberr = this.gameObject.AddComponent<ChromaticAbrevEffect>();
	       	ChromaticAberr.Init(ChromaticIntensity);
	    }
	    glare.TurnOn(flashLength);
	    ChromaticAberr.TurnOn(flashLength);




	}
	public void EndFlash(){
		if(glare!=null)
  			glare.TurnOff(flashLength*2f);
		if(ChromaticAberr!=null)
	    	ChromaticAberr.TurnOff(flashLength*2f);
		glare=null;
		ChromaticAberr=null;
	}
}
