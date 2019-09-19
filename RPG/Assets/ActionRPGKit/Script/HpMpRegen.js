#pragma strict
var hpRegen : int = 0;
var mpRegen : int = 3;
var hpRegenDelay : float = 3.0;
var mpRegenDelay : float = 3.0;

private var hpTime : float = 0.0;
private var mpTime : float = 0.0;
private var stat : Status;

function Start () {
	stat = GetComponent(Status);
}

function Update () {
	if(hpRegen > 0 && stat.health < stat.totalMaxHealth){
		if(hpTime >= hpRegenDelay){
			HPRecovery();
		}else{
			hpTime += Time.deltaTime;
		}
	}
	//----------------------------------------------------
	if(mpRegen > 0 && stat.mana < stat.totalMaxMana){
		if(mpTime >= mpRegenDelay){
			MPRecovery();
		}else{
			mpTime += Time.deltaTime;
		}
	}
}

function HPRecovery(){
	var amount : int = stat.totalMaxHealth * hpRegen / 100;
	if(amount <= 1){
		amount = 1;
	}
	stat.health += amount;
	hpTime = 0.0f;
	if(stat.health >= stat.totalMaxHealth){
		stat.health = stat.totalMaxHealth;
	}
}

function MPRecovery(){
	var amount : int = stat.totalMaxMana * mpRegen / 100;
	if(amount <= 1){
		amount = 1;
	}
	stat.mana += amount;
	mpTime = 0.0f;
	if(stat.mana >= stat.totalMaxMana){
		stat.mana = stat.totalMaxMana;
	}
}

