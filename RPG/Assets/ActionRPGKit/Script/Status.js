#pragma strict
var characterName : String = "";
var characterId : int = 0; // ID for Character Selection
var level : int = 1;
var atk : int = 0;
var def : int = 0;
var matk : int = 0;
var mdef : int = 0;
var exp : int = 0;
var maxExp : int = 100;
var maxHealth : int = 100;
@HideInInspector
var totalMaxHealth : int = 100;

var health : int = 100;
var maxMana : int = 100;
@HideInInspector
var totalMaxMana : int = 100;

var mana : int = 100;
var statusPoint : int = 0;
var skillPoint : int = 0;
private var dead : boolean = false;

var immortal : boolean = false;

@HideInInspector
var mainModel : GameObject;

@HideInInspector
var addAtk : int = 0;
@HideInInspector
var addDef : int = 0;
@HideInInspector
var addMatk : int = 0;
@HideInInspector
var addMdef : int = 0;
@HideInInspector
var addHP: int = 0;
@HideInInspector
var addMP: int = 0;

var deathBody : Transform;

@HideInInspector
var spawnPointName : String = ""; //Store the name for Spawn Point When Change Scene

//---------States----------
@HideInInspector
var buffAtk : int = 0;
@HideInInspector
var buffDef : int = 0;
@HideInInspector
var buffMatk : int = 0;
@HideInInspector
var buffMdef : int = 0;

@HideInInspector
var weaponAtk : int = 0;
@HideInInspector
var weaponMatk : int = 0;

//Negative Buffs
@HideInInspector
var poison : boolean = false;
@HideInInspector
var silence : boolean = false;
@HideInInspector
var web : boolean = false;
@HideInInspector
var stun : boolean = false;

@HideInInspector
var freeze : boolean = false; // Use for Freeze Character
@HideInInspector
var dodge : boolean = false;

//Positive Buffs
@HideInInspector
var brave : boolean = false; //Can be use for Weaken
@HideInInspector
var barrier : boolean = false;
@HideInInspector
var mbarrier : boolean = false;
@HideInInspector
var faith : boolean = false; //Can be use for Clumsy

//Effect
var poisonEffect : GameObject;
var silenceEffect : GameObject;
var stunEffect : GameObject;
var webbedUpEffect : GameObject;

var stunAnimation : AnimationClip;
var webbedUpAnimation : AnimationClip;

@HideInInspector
var useMecanim : boolean = false;

class elem{
	var elementName : String = "";
	var effective : int = 100;
}
var elementEffective : elem[] = new elem[5];
// 0 = Normal , 1 = Fire , 2 = Ice , 3 = Earth , 4 = Wind

class Resist{
	var poisonResist : int = 0;
	var silenceResist : int = 0;
	var webResist : int = 0;
	var stunResist : int = 0;
}
var statusResist : Resist;

@HideInInspector
var eqResist : Resist;
@HideInInspector
var totalResist : Resist;

class HiddenStat{
	var doubleJump : boolean = false;
	var drainTouch : int = 0;
	var autoGuard : int = 0;
	var mpReduce : int = 0;
}
@HideInInspector
var hiddenStatus : HiddenStat;

var sendMsgWhenDead : String = "";

function Start(){
	if(characterName != ""){
		gameObject.name = characterName;
	}
	CalculateStatus();
}

function OnDamage(amount : int , element : int) : String{	
	if(dead){
		return;
	}
	if(dodge){
		return "Evaded";
	}
	if(immortal){
		return "Invulnerable";
	}
	if(hiddenStatus.autoGuard > 0){
		var ran : int = Random.Range(0 , 100);
		if(ran <= hiddenStatus.autoGuard){
			return "Guard";
		}
	}

	amount -= def;
	amount -= addDef;
	amount -= buffDef;
	
	//Calculate Element Effective
	amount *= elementEffective[element].effective;
	amount /= 100;
	
	if(amount < 1){
		amount = 1;
	}
	
	health -= amount;

	if(health <= 0){
		health = 0;
		enabled = false;
		dead = true;
		Death();
	}
	return amount.ToString();
}

function OnMagicDamage(amount : int  , element : int) : String{
	if(dead){
		return;
	}
	if(dodge){
		return "Evaded";
	}
	if(immortal){
		return "Invulnerable";
	}
	if(hiddenStatus.autoGuard > 0){
		var ran : int = Random.Range(0 , 100);
		if(ran <= hiddenStatus.autoGuard){
			return "Guard";
		}
	}

	amount -= mdef;
	amount -= addMdef;
	amount -= buffMdef;
	
	//Calculate Element Effective
	amount *= elementEffective[element].effective;
	amount /= 100;
	
	if(amount < 1){
		amount = 1;
	}
	
	health -= amount;

	if(health <= 0){
		health = 0;
		enabled = false;
		dead = true;
		Death();
	}
	return amount.ToString();
}

function Heal(hp : int , mp : int){
	health += hp;
	if(health >= totalMaxHealth){
		health = totalMaxHealth;
	}
	
	mana += mp;
	if(mana >= totalMaxMana){
		mana = totalMaxMana;
	}
}


function Death(){
	if(sendMsgWhenDead != ""){
		SendMessage(sendMsgWhenDead , SendMessageOptions.DontRequireReceiver);
	}
	if(gameObject.tag == "Player"){
		SaveData();
		if(GetComponent(UIiMaster)){
			GetComponent(UIiMaster).DestroyAllUi();
		}
	}
	if(deathBody){
		Instantiate(deathBody, transform.position , transform.rotation);
	}else{
		print("This Object didn't assign the Death Body");
	}
	Destroy(gameObject);
}

function gainEXP(gain : int){
	exp += gain;
	if(exp >= maxExp){
		var remain : int = exp - maxExp;
		LevelUp(remain);
	}
}

function LevelUp(remainingEXP : int){
	exp = 0;
	exp += remainingEXP;
	level++;
	statusPoint += 5;
	skillPoint++;
	//Extend the Max EXP, Max Health and Max Mana
	maxExp = 1.25 * maxExp;
	maxHealth += 20;
	maxMana += 10;
	//Recover Health and Mana
	CalculateStatus();
	health = totalMaxHealth;
	mana = totalMaxMana;
	gainEXP(0);
	if(GetComponent(SkillWindow)){
		GetComponent(SkillWindow).LearnSkillByLevel(level);
	}
}

function SaveData(){
	PlayerPrefs.SetString("TempName", characterName);
	PlayerPrefs.SetInt("TempID", characterId);
	PlayerPrefs.SetInt("TempPlayerLevel", level);
	PlayerPrefs.SetInt("TempPlayerATK", atk);
	PlayerPrefs.SetInt("TempPlayerDEF", def);
	PlayerPrefs.SetInt("TempPlayerMATK", matk);
	PlayerPrefs.SetInt("TempPlayerMDEF", mdef);
	PlayerPrefs.SetInt("TempPlayerEXP", exp);
	PlayerPrefs.SetInt("TempPlayerMaxEXP", maxExp);
	PlayerPrefs.SetInt("TempPlayerMaxHP", maxHealth);
	PlayerPrefs.SetInt("TempPlayerMaxMP", maxMana);
	PlayerPrefs.SetInt("TempPlayerSTP", statusPoint);
	PlayerPrefs.SetInt("TempPlayerSKP", skillPoint);
			
	PlayerPrefs.SetInt("TempCash", GetComponent(Inventory).cash);
	var itemSize : int = GetComponent(Inventory).itemSlot.length;
	var a : int = 0;
	if(itemSize > 0){
		while(a < itemSize){
			PlayerPrefs.SetInt("TempItem" + a.ToString(), GetComponent(Inventory).itemSlot[a]);
			PlayerPrefs.SetInt("TempItemQty" + a.ToString(), GetComponent(Inventory).itemQuantity[a]);
			a++;
		}
	}
			
	var equipSize : int = GetComponent(Inventory).equipment.length;
	a = 0;
	if(equipSize > 0){
		while(a < equipSize){
			PlayerPrefs.SetInt("TempEquipm" + a.ToString(), GetComponent(Inventory).equipment[a]);
			a++;
		}
	}
	PlayerPrefs.SetInt("TempWeaEquip", GetComponent(Inventory).weaponEquip);
	PlayerPrefs.SetInt("TempSubEquip", GetComponent(Inventory).subWeaponEquip);
	PlayerPrefs.SetInt("TempArmoEquip", GetComponent(Inventory).armorEquip);
	PlayerPrefs.SetInt("TempAccEquip", GetComponent(Inventory).accessoryEquip);
	//Save Quest
	var questSize : int = GetComponent(QuestStat).questProgress.length;
	PlayerPrefs.SetInt("TempQuestSize", questSize);
	a = 0;
	if(questSize > 0){
		while(a < questSize){
			PlayerPrefs.SetInt("TempQuestp" + a.ToString(), GetComponent(QuestStat).questProgress[a]);
			a++;
		}
	}
	var questSlotSize : int = GetComponent(QuestStat).questSlot.length;
	PlayerPrefs.SetInt("TempQuestSlotSize", questSlotSize);
	a = 0;
	if(questSlotSize > 0){
		while(a < questSlotSize){
			PlayerPrefs.SetInt("TempQuestslot" + a.ToString(), GetComponent(QuestStat).questSlot[a]);
			a++;
		}
	}
	//Save Skill Slot
	a = 0;
	while(a < GetComponent(SkillWindow).skill.Length){
		PlayerPrefs.SetInt("TempSkill" + a.ToString(), GetComponent(SkillWindow).skill[a]);
		a++;
	}
	//Skill List Slot
	a = 0;
	while(a < GetComponent(SkillWindow).skillListSlot.length){
		PlayerPrefs.SetInt("TempSkillList" + a.ToString(), GetComponent(SkillWindow).skillListSlot[a]);
		a++;
	}	
	print("Saved");
}

function CalculateStatus(){
	addAtk = atk + buffAtk + weaponAtk;
	//addDef += def;
	addMatk = matk + buffMatk + weaponMatk;
	totalMaxHealth = maxHealth + addHP;
	totalMaxMana = maxMana + addMP;
	//addMdef += mdef;
	if(health >= totalMaxHealth){
		health = totalMaxHealth;
	}
	if(mana >= totalMaxMana){
		mana = totalMaxMana;
	}
	totalResist.poisonResist = statusResist.poisonResist + eqResist.poisonResist;
	totalResist.silenceResist = statusResist.silenceResist + eqResist.silenceResist;
	totalResist.stunResist = statusResist.stunResist + eqResist.stunResist;
	totalResist.webResist = statusResist.webResist + eqResist.webResist;
}

//----------States--------
function OnPoison(hurtTime : int){
	if(poison){
		return;
	}
	var chance = 100;
	chance -= totalResist.poisonResist;
	if(chance > 0){
		var per = Random.Range(0, 100);
		if(per <= chance){
			poison = true;
			var amount : int = maxHealth * 2 / 100; // Hurt 2% of Max HP
		}else{
			return;
		}
		
	}else{
		return;
	}
	//--------------------
	while(poison && hurtTime > 0){
		if(poisonEffect){ //Show Poison Effect
				var eff : GameObject = Instantiate(poisonEffect, transform.position, poisonEffect.transform.rotation);
				eff.transform.parent = transform;
			}
		yield WaitForSeconds(0.7); // Reduce HP  Every 0.7 Seconds
		health -= amount;
	
		if (health <= 1){
			health = 1;
		}
		if(eff){ //Destroy Effect if it still on a map
			Destroy(eff.gameObject);
		}
		hurtTime--;
		if(hurtTime <= 0){
			poison = false;
		}
	}
}


function OnSilence(dur : float){
	if(silence){
		return;
	}
	var chance = 100;
	chance -= totalResist.silenceResist;
	if(chance > 0){
		var per = Random.Range(0, 100);
		if(per <= chance){
			silence = true;
			if(silenceEffect){ //Show Poison Effect
				var eff : GameObject = Instantiate(silenceEffect, transform.position, transform.rotation);
				eff.transform.parent = transform;
			}
		}else{
			return;
		}
		
	}else{
		return;
	}
	yield WaitForSeconds(dur);
	if(eff){ //Destroy Effect if it still on a map
			Destroy(eff.gameObject);
		}
	silence = false;

}

function OnWebbedUp(dur : float){
	if(web){
		return;
	}
	var chance = 100;
	chance -= totalResist.webResist;
	if(chance > 0){
		var per = Random.Range(0, 100);
		if(per <= chance){
			web = true;
			freeze = true; // Freeze Character On (Character cannot do anything)
			if(webbedUpEffect){ //Show Poison Effect
				var eff : GameObject = Instantiate(webbedUpEffect, transform.position, transform.rotation);
				eff.transform.parent = transform;
			}
			if(webbedUpAnimation){// If you Assign the Animation then play it
				if(useMecanim){
					GetComponent(PlayerMecanimAnimation).PlayAnim(webbedUpAnimation.name);
				}else{
					mainModel.GetComponent.<Animation>()[webbedUpAnimation.name].layer = 25;
					mainModel.GetComponent.<Animation>().Play(webbedUpAnimation.name);
				}
			}
		}else{
			return;
		}
		
	}else{
		return;
	}
	yield WaitForSeconds(dur);
	if(eff){ //Destroy Effect if it still on a map
			Destroy(eff.gameObject);
		}
	if(webbedUpAnimation && !useMecanim){// If you Assign the Animation then stop playing
		mainModel.GetComponent.<Animation>().Stop(webbedUpAnimation.name);
	}
	freeze = false; // Freeze Character Off
	web = false;

}

function OnStun(dur : float){
	if(stun){
		return;
	}
	var chance = 100;
	chance -= totalResist.stunResist;
	if(chance > 0){
		var per = Random.Range(0, 100);
		if(per <= chance){
			stun = true;
			freeze = true; // Freeze Character On (Character cannot do anything)
			if(stunEffect){ //Show Stun Effect
				var eff : GameObject = Instantiate(stunEffect, transform.position, stunEffect.transform.rotation);
				eff.transform.parent = transform;
			}
			if(stunAnimation){// If you Assign the Animation then play it
				if(useMecanim){
					GetComponent(PlayerMecanimAnimation).PlayAnim(stunAnimation.name);
				}else{
					mainModel.GetComponent.<Animation>()[stunAnimation.name].layer = 25;
					mainModel.GetComponent.<Animation>().Play(stunAnimation.name);
				}
			}
		}else{
			return;
		}
		
	}else{
		return;
	}
	yield WaitForSeconds(dur);
	if(eff){ //Destroy Effect if it still on a map
			Destroy(eff.gameObject);
		}
	if(stunAnimation && !useMecanim){// If you Assign the Animation then stop playing
		mainModel.GetComponent.<Animation>().Stop(stunAnimation.name);
	}
	freeze = false; // Freeze Character Off
	stun = false;

}

function ApplyAbnormalStat(statId : int , dur : float){
	if(GlobalCondition.freezePlayer){
		return;
	}
	if(statId == 0){
		OnPoison(Mathf.FloorToInt(dur));
	}
	if(statId == 1){
		OnSilence(dur);
	}
	if(statId == 2){
		OnStun(dur);
	}
	if(statId == 3){
		OnWebbedUp(dur);
	}
}

function OnBarrier(amount : float , dur : float){
	//Increase Defense
	if(barrier){
		return;
	}
	barrier = true;
	buffDef = 0;
	buffDef += amount;
	CalculateStatus();
	yield WaitForSeconds(dur);
	buffDef = 0;
	barrier = false;
	CalculateStatus();
}

function OnMagicBarrier(amount : float , dur : float){
	//Increase Magic Defense
	if(mbarrier){
		return;
	}
	mbarrier = true;
	buffMdef = 0;
	buffMdef += amount;
	CalculateStatus();
	yield WaitForSeconds(dur);
	buffMdef = 0;
	mbarrier = false;
	CalculateStatus();
}

function OnBrave(amount : float , dur : float){
	//Increase Attack
	if(brave){
		return;
	}
	brave = true;
	buffAtk = 0;
	buffAtk += amount;
	CalculateStatus();
	yield WaitForSeconds(dur);
	buffAtk = 0;
	brave = false;
	CalculateStatus();
}

function OnFaith(amount : float , dur : float){
	//Increase Magic Attack
	if(faith){
		return;
	}
	faith = true;
	buffMatk = 0;
	buffMatk += amount;
	CalculateStatus();
	yield WaitForSeconds(dur);
	buffMatk = 0;
	faith = false;
	CalculateStatus();
}

function ApplyBuff(statId : int , dur : float , amount : int){
	if(statId == 1){
		//Increase Defense
		OnBarrier(amount , dur);
	}
	if(statId == 2){
		//Increase Magic Defense
		OnMagicBarrier(amount , dur);
	}
	if(statId == 3){
		//Increase Attack
		OnBrave(amount , dur);
	}
	if(statId == 4){
		//Increase Magic Attack
		OnFaith(amount , dur);
	}
}