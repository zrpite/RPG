#pragma strict
var duration : float = 1.0;
var penetrate : boolean = false;
private var hitEffect : GameObject;

var setRotation : boolean = false;
var rotationSetTo : Vector3 = Vector3.zero;
	// Use this for initialization
function Start(){
	hitEffect = GetComponent(BulletStatus).hitEffect;
	//Set this object parent of the Shooter GameObject from BulletStatus
	if(setRotation){
		transform.eulerAngles = rotationSetTo;
	}
	var shooter : Transform = GetComponent(BulletStatus).shooter.transform;
	this.transform.parent = shooter;
	this.transform.position = new Vector3(transform.position.x , transform.position.y , shooter.position.z);
	Destroy(gameObject, duration);
}
	
function OnTriggerEnter (other : Collider) {  
	if(other.gameObject.tag == "Wall"){
		if(hitEffect && !penetrate){
			Instantiate(hitEffect, transform.position , transform.rotation);
		}
		if(!penetrate){
			//Destroy this object if it not Penetrate
			Destroy (gameObject);
		}
	}
}

@script RequireComponent(BulletStatus)