﻿using UnityEngine;
using System.Collections;

public class TitleC : MonoBehaviour {
	
	public Texture2D tip;
	public string goToScene = "Field1";
	public string spawnPointName = "PlayerSpawnPointC"; 
	public GameObject characterDatabase;
	public Transform modelPosition;
	public Vector2 characterUiSize = new Vector2(400 , 460); //Show Detail GUI of your Character from CharacterData.
	
	private int page = 0;
	//private int presave = 0;
	
	private int saveSlot = 0;
	private string charName = "Richea";
	private int charSelect = 0;
	private int maxChar = 1;
	private CharacterDataC charData;
	private GameObject showingModel;
	
	void Start (){
		//Screen.lockCursor = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		charData = characterDatabase.GetComponent<CharacterDataC>();
		maxChar = charData.player.Length;
		if(!modelPosition){
			modelPosition = this.transform;
		}
	}
	
	void OnGUI(){
		if(page == 0){
			//Menu
			if (GUI.Button ( new Rect(Screen.width - 420,160 ,280 ,100), "Start Game")) {
				page = 2;
			}
			if (GUI.Button ( new Rect(Screen.width - 420,280 ,280 ,100), "Load Game")) {
				//Check for previous Save Data
				page = 3;
			}
			if (GUI.Button ( new Rect(Screen.width - 420,400 ,280 ,100), "How to Play")) {
				page = 1;
			}
		}
		
		if(page == 1){
			//Help
			GUI.Box ( new Rect(Screen.width /2 -250,115,400,400), tip);
			
			if (GUI.Button ( new Rect(Screen.width - 280, Screen.height -150,250 ,90), "Back")) {
				page = 0;
			}
		}
		
		if(page == 2){
			//Create Character and Select Save Slot
			GUI.Box ( new Rect(Screen.width / 2 - 250,170,500,400), "Select your slot");
			if (GUI.Button ( new Rect(Screen.width / 2 + 185,175,30,30), "X")) {
				page = 0;
			}
			//---------------Slot 1 [ID 0]------------------
			if(PlayerPrefs.GetInt("PreviousSave0") > 0){
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,205,400,100), PlayerPrefs.GetString("Name0") + "\n" + "Level " + PlayerPrefs.GetInt("PlayerLevel0").ToString())) {
					//When Slot 1 already used
					saveSlot = 0;
					page = 4;
				}
			}else{
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,205,400,100), "- Empty Slot -")) {
					//Empty Slot 1
					saveSlot = 0;
					page = 5;
					SwitchModel();
				}
			}
			//---------------Slot 2 [ID 1]------------------
			if(PlayerPrefs.GetInt("PreviousSave1") > 0){
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,315,400,100), PlayerPrefs.GetString("Name1") + "\n" + "Level " + PlayerPrefs.GetInt("PlayerLevel1").ToString())) {
					//When Slot 2 already used
					saveSlot = 1;
					page = 4;
				}
			}else{
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,315,400,100), "- Empty Slot -")) {
					//Empty Slot 2
					saveSlot = 1;
					page = 5;
					SwitchModel();
				}
			}
			//---------------Slot 3 [ID 2]------------------
			if(PlayerPrefs.GetInt("PreviousSave2") > 0){
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,425,400,100), PlayerPrefs.GetString("Name2") + "\n" + "Level " + PlayerPrefs.GetInt("PlayerLevel2").ToString())) {
					//When Slot 3 already used
					saveSlot = 2;
					page = 4;
				}
			}else{
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,425,400,100), "- Empty Slot -")) {
					//Empty Slot 3
					saveSlot = 2;
					page = 5;
					SwitchModel();
				}
			}
		}
		
		if(page == 3){
			//Load Save Slot
			GUI.Box ( new Rect(Screen.width / 2 - 250,170,500,400), "Menu");
			if (GUI.Button ( new Rect(Screen.width / 2 + 185,175,30,30), "X")) {
				page = 0;
			}
			//---------------Slot 1 [ID 0]------------------
			if(PlayerPrefs.GetInt("PreviousSave0") > 0){
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,205,400,100), PlayerPrefs.GetString("Name0") + "\n" + "Level " + PlayerPrefs.GetInt("PlayerLevel0").ToString())) {
					//When Slot 1 already used
					saveSlot = 0;
					LoadData ();
				}
			}else{
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,205,400,100), "- Empty Slot -")) {
					//Empty Slot 1
				}
			}
			//---------------Slot 2 [ID 1]------------------
			if(PlayerPrefs.GetInt("PreviousSave1") > 0){
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,315,400,100), PlayerPrefs.GetString("Name1") + "\n" + "Level " + PlayerPrefs.GetInt("PlayerLevel1").ToString())) {
					//When Slot 2 already used
					saveSlot = 1;
					LoadData ();
				}
			}else{
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,315,400,100), "- Empty Slot -")) {
					//Empty Slot 2
				}
			}
			//---------------Slot 3 [ID 2]------------------
			if(PlayerPrefs.GetInt("PreviousSave2") > 0){
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,425,400,100), PlayerPrefs.GetString("Name2") + "\n" + "Level " + PlayerPrefs.GetInt("PlayerLevel2").ToString())) {
					//When Slot 3 already used
					saveSlot = 2;
					LoadData ();
				}
			}else{
				if (GUI.Button ( new Rect(Screen.width / 2 - 200,425,400,100), "- Empty Slot -")) {
					//Empty Slot 3
				}
			}
			
		}
		
		if(page == 4){
			//Overwrite Confirm
			GUI.Box ( new Rect(Screen.width /2 - 150,200,300,180), "Are you sure to overwrite this slot?");
			if (GUI.Button ( new Rect(Screen.width / 2 - 110,260,100,40), "Yes")) {
				page = 5;
				SwitchModel();
			}
			if (GUI.Button ( new Rect(Screen.width / 2 +20,260,100,40), "No")) {
				page = 0;
			}
		}
		
		if(page == 5){
			//Character Select and Name Your Character
			GUI.Box ( new Rect(80,100,300,360), "Enter Your Name");
			
			GUI.Label ( new Rect(100, 200, 300, 40), charData.player[charSelect].description.textLine1);
			GUI.Label ( new Rect(100, 230, 300, 40), charData.player[charSelect].description.textLine2);
			GUI.Label ( new Rect(100, 260, 300, 40), charData.player[charSelect].description.textLine3);
			GUI.Label ( new Rect(100, 290, 300, 40), charData.player[charSelect].description.textLine4);
			
			charName = GUI.TextField ( new Rect(120, 140, 220, 40), charName, 25);
			if (GUI.Button ( new Rect(180,400,100,40), "Done")) {
				NewGame();
			}
			
			//Previous Character
			if (GUI.Button ( new Rect(Screen.width /2 - 110,380,50,150), "<")) {
				if(charSelect > 0){
					charSelect--;
					SwitchModel();
				}
			}
			//Next Character
			if (GUI.Button ( new Rect(Screen.width /2 + 190,380,50,150), ">")) {
				if(charSelect < maxChar -1){
					charSelect++;
					SwitchModel();
				}
			}
			//Show Detail GUI of your Character from CharacterData.
			if(charData.player[charSelect].guiDescription)
				GUI.DrawTexture( new Rect(Screen.width - characterUiSize.x - 5 ,40,characterUiSize.x,characterUiSize.y), charData.player[charSelect].guiDescription);
		}
		
	}
	
	void NewGame(){
		PlayerPrefs.SetInt("SaveSlot", saveSlot);
		//PlayerPrefs.SetString("Name" +saveSlot.ToString(), charName);
		//PlayerPrefs.SetInt("PlayerID" +saveSlot.ToString() , charSelect);
		PlayerPrefs.SetInt("Loadgame", 0);
		GameObject pl = Instantiate(charData.player[charSelect].playerPrefab , transform.position , transform.rotation) as GameObject;
		pl.GetComponent<StatusC>().spawnPointName = spawnPointName;
		pl.GetComponent<StatusC>().characterId = charSelect;
		pl.GetComponent<StatusC>().characterName = charName;
		GlobalConditionC.playerId = pl.GetComponent<StatusC>().characterId;
		Application.LoadLevel(goToScene);
	}
	
	void LoadData(){
		PlayerPrefs.SetInt("SaveSlot", saveSlot);
		SpawnPlayerC.onLoadGame = true;
		//if(presave == 10){
		PlayerPrefs.SetInt("Loadgame", 10);
		int playerId = PlayerPrefs.GetInt("PlayerID" +saveSlot.ToString());
		GlobalConditionC.playerId = playerId;
		GameObject pl = Instantiate(charData.player[playerId].playerPrefab , transform.position , transform.rotation) as GameObject;
		pl.GetComponent<StatusC>().spawnPointName = spawnPointName;
		Application.LoadLevel(goToScene);
		//}
	}
	
	void SwitchModel(){
		if(showingModel){
			Destroy(showingModel);
		}
		//Spawn Showing Model from Character Database
		if(charData.player[charSelect].characterSelectModel){
			showingModel = Instantiate(charData.player[charSelect].characterSelectModel , modelPosition.position , modelPosition.rotation) as GameObject;
		}
	}
	
}