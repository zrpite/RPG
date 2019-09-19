#pragma strict
var radius : float = 5.0;
var delayPerHit : float = 1.0;
var duration : float = 3.1;

private var element : Elementala = Elementala.Normal;
private var attackType : AtkType = AtkType.Physic;
private var wait : float = 0;
private var bl : BulletStatus;
private var variance : int = 15;
private var popup : Transform;
private var hitEffect : GameObject;

var setRotation : boolean = false;
var rotationSetTo : Vector3 = Vector3.zero;

function Start(){
	if(setRotation){
		transform.eulerAngles = rotationSetTo;
	}
	bl = GetComponent(BulletStatus);
	hitEffect = bl.hitEffect;
	popup = bl.Popup;
	attackType = bl.AttackType;
	element = bl.element;
	variance = bl.variance;
	ApplyDamage();
	if(duration > 0){
		Destroy (gameObject, duration);
	}
}

function Update(){
	if(wait >= delayPerHit){
		ApplyDamage();
		wait = 0;
	}else{
		wait += Time.deltaTime;
	}
}

function ApplyDamage(){
	var hitColliders : Collider[] = Physics.OverlapSphere(transform.position, radius);
		
	for(var i : int = 0; i < hitColliders.Length; i++) {
		if(bl.shooterTag == "Enemy" && hitColliders[i].tag == "Player" && hitColliders[i].tag == "Ally" && hitColliders[i].gameObject != bl.shooter){	  
			Damage(hitColliders[i].gameObject);
				
		}else if(bl.shooterTag == "Player" && hitColliders[i].tag == "Enemy" && hitColliders[i].gameObject != bl.shooter){  	
			Damage(hitColliders[i].gameObject);
		}
	}
}
	
function Damage(target : GameObject){
	if(hitEffect){
		Instantiate(hitEffect, target.transform.position , transform.rotation);
	}
	var varMin : int = 100 - variance;
	var varMax : int = 100 + variance;
	var total : int = bl.totalDamage * Random.Range(varMin ,varMax) / 100;
	var popDamage : String = "";
	if(attackType == AtkType.Physic){
		popDamage = target.GetComponent(Status).OnDamage(total , parseInt(element));
	}else{
		popDamage = target.GetComponent(Status).OnMagicDamage(total , parseInt(element));
	}
	var dmgPop : Transform = Instantiate(popup, target.transform.position , transform.rotation);	
	dmgPop.GetComponent(DamagePopup).damage = popDamage;
	if(bl.flinch){
		var dir : Vector3 = (target.transform.position - transform.position).normalized;
		target.SendMessage("Flinch" , dir , SendMessageOptions.DontRequireReceiver);
	}
	if(GetComponent(AbnormalStatAttack) && popDamage != "Miss" && popDamage != "Evaded" && popDamage != "Guard" && popDamage != "Invulnerable"){
		GetComponent(AbnormalStatAttack).InflictAbnormalStats(target);
	}

}

@script RequireComponent(BulletStatus)
