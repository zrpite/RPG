#pragma strict
var expGain : int = 20;
var player : GameObject;
function Start () {
    player = GameObject.FindWithTag ("Player");
    if(!player){
		return;
	}
    
    player.GetComponent(Status).gainEXP(expGain);
}