using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingSize{
	OneBlock,TwoBlock,ThreeBlock
}
public class Building : MonoBehaviour
{
    public BuildingSize buidingSize = BuildingSize.OneBlock;
    public BuildingSize GetbuildingSize(){return buidingSize;}
    public bool isOneBlock(){ return buidingSize == BuildingSize.OneBlock;}
    public bool isTwoBlock(){ return buidingSize == BuildingSize.TwoBlock;}
    public bool isThreeBlock(){ return buidingSize == BuildingSize.ThreeBlock;}




    
}
