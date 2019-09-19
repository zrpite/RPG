#pragma strict
private var player : GameObject;

function OnTriggerEnter(other : Collider) {
	if(other.gameObject.tag == "Player") {
		player = other.gameObject;
		SaveData();
	}
 }
 
function SaveData(){
	PlayerPrefs.SetInt("PreviousSave", 10);
	PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
	PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
	PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
	print("Saved");
}