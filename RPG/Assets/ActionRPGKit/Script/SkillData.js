#pragma strict

class Skil {
	var skillName : String = "";
	var icon : Texture2D;
	var skillPrefab : Transform;
	var skillAnimation : AnimationClip;
	var manaCost : int = 10;
	var castTime : float = 0.5;
	var skillDelay : float = 0.5;
	var description : String = "";
	var castEffect : GameObject;
	var sendMsg : String = ""; //Send Message calling function when use this skill.
	var whileAttack : whileAtk = whileAtk.Immobile;
}

var skill : Skil[] = new Skil[3];