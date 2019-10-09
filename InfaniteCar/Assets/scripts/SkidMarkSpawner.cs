using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidMarkSpawner : MonoBehaviour
{
	public CarMovement cMovement;
	public GameObject SkidmarkPrefab;

    List<LineRenderer> skids = new List<LineRenderer>();
    PlayerController PC;
    bool skidding = false;
    int pointCount = 0;

    float AddRate = .01f;
    float LastAddTime = 0;

    TileMover tm;
	void Awake(){
        PC = GetComponent<PlayerController>();
		cMovement = this.GetComponent<CarMovement>();
        CarMovement.OnStartSkid += StartSkid;
        CarMovement.OnStopSkid += EndSkid;
        PlayerController.OnTileChange += OnTileChange;
	}
    void Start(){
        tm= TileMover.instance;

    }
    void OnDisabled(){
        CarMovement.OnStartSkid -= StartSkid;
        CarMovement.OnStopSkid -= EndSkid;
    }
    void AddPoint(){
        LastAddTime = Time.time;
        pointCount++;

        skids[0].positionCount = pointCount;
        skids[0].SetPosition(pointCount-1,cMovement.BackLeftTire.position);
       // skids[0].SetPosition(pointCount-1, PC.currTile.transform.InverseTransformPoint(cMovement.BackLeftTire.position));

        skids[1].positionCount = pointCount;
        skids[1].SetPosition(pointCount-1,cMovement.BackRightTire.position);
       // skids[1].SetPosition(pointCount-1, PC.currTile.transform.InverseTransformPoint(cMovement.BackRightTire.position));

    }
    void OnTileChange(){
        if(!skidding) return;
       // Debug.Log("Tile Change");
        EndSkid();
        StartSkid();
    }
    void FixedUpdate(){
        if(skidding){
            if(Time.time - LastAddTime > AddRate){
                AddPoint();
            }
            UpdatePoints();
        }
    }   
    Vector3[] points;
    void UpdatePoints(){
        foreach(LineRenderer l in skids){
            points = new Vector3[l.positionCount];
            l.GetPositions(points);
            for(int i = 0 ; i < points.Length; i++){
                
                points[i] -= tm.GetMovementUpdate();
            }
            l.SetPositions(points);
        }
    }
    void StartSkid(){
       // Debug.Log("Start skid");
        skidding = true;
        pointCount=0;
        LineRenderer lineLeft = Instantiate(SkidmarkPrefab, cMovement.BackLeftTire.position + (Vector3.up * .1f), Quaternion.identity, PC.currTile.transform ).GetComponent<LineRenderer>();
        LineRenderer lineRight= Instantiate(SkidmarkPrefab, cMovement.BackRightTire.position+ (Vector3.up * .1f), Quaternion.identity, PC.currTile.transform ).GetComponent<LineRenderer>();
        


        skids.Add(lineLeft);
        skids.Add(lineRight);
        skids[0].SetPosition(0,cMovement.BackLeftTire.position);
        skids[1].SetPosition(0,cMovement.BackRightTire.position);
        //skids[0].SetPosition(0, PC.currTile.transform.InverseTransformPoint(cMovement.BackLeftTire.position));
        //skids[1].SetPosition(0, PC.currTile.transform.InverseTransformPoint(cMovement.BackRightTire.position));
    }

    void EndSkid(){
        skidding = false;
        if(skids.Count>0){
            skids[0].useWorldSpace = false;
            skids[1].useWorldSpace = false;



       }
       // Debug.Log("stop skid");
        skids.Clear();
    }
}
