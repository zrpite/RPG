#pragma strict
var teleportToMap : String = "Level1";
var spawnPointName : String = "PlayerSpawnPoint"; //Use for Move Player to the SpawnPoint Position
var allowMountEnter : boolean = true;
//var spawnPosition : Vector3;

function OnTriggerEnter(other : Collider){
	if(other.tag == "Player"){
		if(!allowMountEnter && GlobalCondition.freezePlayer){
			return;
		}
		other.GetComponent(Status).spawnPointName = spawnPointName;
		ChangeMap();
	}
}

function ChangeMap(){
	//Destroy all Mounts
	var gos : GameObject[] = GameObject.FindGameObjectsWithTag("Mount");
	if(gos.Length > 0){
		for(var go : GameObject in gos){ 
			go.SendMessage("DestroySelf" , SendMessageOptions.DontRequireReceiver);
		}
	}
	
	Application.LoadLevel(teleportToMap);
}

@script AddComponentMenu ("Action-RPG Kit/Create Teleporter")