using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class CarSelectionManager : MonoBehaviour
{
	public List<CarDataScriptableObject> CarTypes = new List<CarDataScriptableObject>();
	int SelectionIndex = 0;
	CarDataScriptableObject currentSelection;
	public Transform CenterSpawn;
	public PlayerData playerData;
	public Image speedImage,gripImage,accelImage;
    public Image AdditionalSpeedImage,AdditionalGripImage,AdditionalAccelImage;

	GameObject VisableMesh;

    public TextMeshProUGUI Title,CoinAmount,CarPrice;
    public GameObject LockedImage;
    public GameObject PurchaseCarPanel;
    public GameObject UpgradeCarPanel;

    public TextMeshProUGUI SpeedUpgradeCostText,GripUpgradeCostText,AccelUpgradeCostText;

    // Start is called before the first frame update
    void Start()
    {
    	playerData = PlayerData.instance;
        UpdatePanel();
    }

   

    public void Next(){
    	SelectionIndex++;
    	if(SelectionIndex >= CarTypes.Count)
    		SelectionIndex =0;
    	UpdatePanel();
    }
    public void Previouse(){
    	SelectionIndex--;
    	if(SelectionIndex < 0)
    		SelectionIndex = CarTypes.Count;
    	UpdatePanel();
    }
    public void AttemptPurchaseCar(){
        //if has money
        if(playerData.playerUnlocks.Coins > currentSelection.CarPrice){
            //take money and save that data
            playerData.playerUnlocks.Coins -= currentSelection.CarPrice;
            playerData.playerUnlocks.Cars[SelectionIndex]=true;
            playerData.SavePlayerUnlocks(UpdatePanel);
        }
        

    }
    void UpdatePanel(){
        //Debug.Log("Update panel  " +playerData.playerUnlocks.Coins.ToString("00"));
    	playerData.currentSelection = CarTypes[SelectionIndex];
    	currentSelection = CarTypes[SelectionIndex];

        CoinAmount.text = playerData.playerUnlocks.Coins.ToString("00");
        CarPrice.text = currentSelection.CarPrice.ToString("00");
        playerData.SelectionIndex =SelectionIndex;
        if(!playerData.playerUnlocks.Cars[SelectionIndex]){ //if unlocked
            LockedImage.SetActive(true);
            PurchaseCarPanel.SetActive(true);
            UpgradeCarPanel.SetActive(false);

        }else{
            LockedImage.SetActive(false);
            PurchaseCarPanel.SetActive(false);
            UpgradeCarPanel.SetActive(true);
            UpdateUpgradesPanel();


        }
    	if(VisableMesh != null)
    		Destroy(VisableMesh);

    	VisableMesh = Instantiate(currentSelection.MeshObject,CenterSpawn.position, CenterSpawn.rotation,CenterSpawn);
		VisableMesh.transform.localEulerAngles += Vector3.up *90;
        UpdateCarStats();
    	
        Title.text = currentSelection.CarType;
    }
    void UpdateCarStats(){
        speedImage.fillAmount = currentSelection.Speed /5f;
        gripImage.fillAmount = currentSelection.Grip /5f;
        accelImage.fillAmount = currentSelection.Acceleration /.1f;

        AdditionalSpeedImage.fillAmount = (currentSelection.Speed /5f)  + ((float)CurrentCarUpgrades().Speed * .05f);
        AdditionalGripImage.fillAmount = (currentSelection.Grip /5f)  + (CurrentCarUpgrades().Grip * .05f);
        AdditionalAccelImage.fillAmount = (currentSelection.Acceleration /.1f) + (CurrentCarUpgrades().Accel * .05f);


    }
    void UpdateUpgradesPanel(){


        SpeedUpgradeCostText.text = SpeedUpgradeCost().ToString("00");
        GripUpgradeCostText.text =GripUpgradeCost().ToString("00");
        AccelUpgradeCostText.text = AccelUpgradeCost().ToString("00");
    }

    public void Play(){
        if(playerData.playerUnlocks.Cars[SelectionIndex]){ //if unlocked
            
    	   SceneManager.LoadScene("MainScene");
        }else{
            Debug.Log("TODO present buy button more");
        }
    }
    SingleCarUpgrade CurrentCarUpgrades(){
        if(playerData == null){
            Debug.Log("<color=red> No playerData on CarSelection </color>");
            return null;
        }
        return playerData.playerUpgrades.CarUpgrades[SelectionIndex];
    }

    int SpeedUpgradeCost(){
        SingleCarUpgrade carUpgrades = CurrentCarUpgrades(); 
        return (carUpgrades.Speed*50) + 50;
    }
    int GripUpgradeCost(){
        SingleCarUpgrade carUpgrades = CurrentCarUpgrades();
        return (carUpgrades.Grip*50) + 50;
    }
    int AccelUpgradeCost(){
        SingleCarUpgrade carUpgrades = CurrentCarUpgrades();
        return (carUpgrades.Accel*50) + 50;
    }

    public void UpgradeSpeed(){
        if(playerData.playerUnlocks.Coins > SpeedUpgradeCost() && CurrentCarUpgrades().Speed <5 ){

            playerData.playerUnlocks.Coins -= SpeedUpgradeCost();

            CurrentCarUpgrades().Speed ++;

            SaveUpgradeData();
        }
    }
    public void UpgradeGrip(){
        if(playerData.playerUnlocks.Coins > GripUpgradeCost() && CurrentCarUpgrades().Grip <5 ){

            playerData.playerUnlocks.Coins -= GripUpgradeCost();

            CurrentCarUpgrades().Grip ++;

            SaveUpgradeData();
        }
    }
    public void UpgradeAccel(){
         if(playerData.playerUnlocks.Coins > AccelUpgradeCost() && CurrentCarUpgrades().Accel <5 ){

            playerData.playerUnlocks.Coins -= AccelUpgradeCost();

            CurrentCarUpgrades().Accel ++;

            SaveUpgradeData();
        }
    }
    void SaveUpgradeData(){
        playerData.SavePlayerUnlocks();
        playerData.SavePlayerUpgrades(UpdatePanel);
    }
    public void ReturnToMenu(){


    }
}
