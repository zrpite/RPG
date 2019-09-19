#pragma strict
var player : GameObject;
//var mainCamPrefab : GameObject;
private var mainCam : Transform;
static var onLoadGame : boolean = false;

function Start () {
		//Check for Current Player in the scene
		var currentPlayer : GameObject = GameObject.FindWithTag("Player");
		if(currentPlayer){
			// If there are the player in the scene already. Check for the Spawn Point Name
			// If it match then Move Player to the SpawnpointPosition
			var spawnPointName : String = currentPlayer.GetComponent(Status).spawnPointName;
			var spawnPoint : GameObject = GameObject.Find(spawnPointName);
			if(spawnPoint && !onLoadGame){
				currentPlayer.transform.root.position = spawnPoint.transform.position;
				currentPlayer.transform.root.rotation = spawnPoint.transform.rotation;
			}
			
			PlayerPrefs.SetFloat("PlayerX", currentPlayer.transform.position.x);
			PlayerPrefs.SetFloat("PlayerY", currentPlayer.transform.position.y);
			PlayerPrefs.SetFloat("PlayerZ", currentPlayer.transform.position.z);
				
			onLoadGame = false;
			var oldCam : GameObject = currentPlayer.GetComponent(AttackTrigger).Maincam.gameObject;
			if(!oldCam){
				return;
			}
			var cam : GameObject[] = GameObject.FindGameObjectsWithTag("MainCamera"); 
    		for (var cam2 : GameObject in cam) { 
  				if(cam2 != oldCam){
  					Destroy(cam2.gameObject);
  				}
  			}
			// If there are the player in the scene already. We will not spawn the new player.
			return;
		}
		//Spawn Player
		var spawnPlayer : GameObject = Instantiate(player, transform.position , transform.rotation);
		mainCam = GameObject.FindWithTag ("MainCamera").transform;
		var checkCam : ARPGcamera = mainCam.GetComponent(ARPGcamera);
		//Check for Main Camera
		if(mainCam && checkCam){
    		mainCam.GetComponent(ARPGcamera).target = spawnPlayer.transform;
		}
		
		//Screen.lockCursor = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		
		PlayerPrefs.SetFloat("PlayerX", spawnPlayer.transform.position.x);
		PlayerPrefs.SetFloat("PlayerY", spawnPlayer.transform.position.y);
		PlayerPrefs.SetFloat("PlayerZ", spawnPlayer.transform.position.z);
}
