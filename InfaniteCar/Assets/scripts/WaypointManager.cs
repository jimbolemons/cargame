using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
	public Transform startPoint,endPoint;
	public List<Transform> middlepoint = new List<Transform>(); 


  
  	 Transform GetStart(){return startPoint;}
  	 Transform GetLast(){return endPoint;}
  	 Transform GetFirstMiddle(){return middlepoint[0];}
  	 Transform GetMiddleViaIndex(int i){
  		if(i< middlepoint.Count)
  		return middlepoint[i];
  		else return endPoint;
  	}

  	public int GetMiddleWaypointsCount(){return middlepoint.Count;}







}
