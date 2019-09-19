#pragma strict
var teleportToMap : String = "Level1";
var spawnPointName : String = "PlayerSpawnPoint"; //Use for Move Player to the SpawnPoint Position
var eventId : int = 2;
var conditionValue : int = 5;
private var player : GameObject;
var teleportWhenTrigger : boolean = true;

function OnTriggerEnter(other : Collider){
	if(other.tag == "Player"){
		player = other.gameObject;
		if(teleportWhenTrigger){
			ChangeMap();
		}
	}
}

function ChangeMap(){
	if(GlobalCondition.eventVar[eventId] >= conditionValue){
		if(!player){
			player = GameObject.FindWithTag("Player");
		}
		player.GetComponent(Status).spawnPointName = spawnPointName;
		Application.LoadLevel(teleportToMap);
	}
}