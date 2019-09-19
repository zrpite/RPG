#pragma strict
var returnToPos : Transform;

function OnTriggerEnter(other : Collider){
	if(other.tag == "Player"){
		other.transform.position = returnToPos.position;
	}
}