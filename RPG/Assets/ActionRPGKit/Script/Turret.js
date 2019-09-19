#pragma strict
var startTime : float = 1.0;
var speed : float = 20.0;
var radius : float = 20.0;  //this is how far it checks for other objects
var attackPoint : Transform;
private var lockTarget : Transform;
var lockon : boolean = false;
var duration : float = 10.0;
var delay : float = 1.0;
var continous : int = 3;
var continueDelay : float = 0.2;
var bullet1 : Transform;
var destroyEffect : GameObject;

private var str = 0;
private var matk = 0;
private var shooter : GameObject;
private var shooterTag : String = "Player";
private var targetTag : String = "Enemy";

private var state : int = 0;
var relativeDirection = Vector3.forward;
private var closest : GameObject;
var rotateSpeed : float = 40.0;

var mainModel : GameObject; 
var idleAnim : String = "";
var attackAnim : String = "";
var spawnAnim : String = "";
var attackEffect : GameObject; 
var lockYAxis : boolean = false;

function Start(){
	if(!attackPoint){
		attackPoint = this.transform;
	}
	if(!mainModel){
		mainModel = this.gameObject;
	}
	if(lockYAxis){
		transform.eulerAngles.x = 0;
		transform.eulerAngles.z = 0;
	}
	if(spawnAnim != ""){
		mainModel.GetComponent.<Animation>().Play(spawnAnim);
		var ww = mainModel.GetComponent.<Animation>()[spawnAnim].length;
		yield WaitForSeconds(ww);
	}
	yield WaitForSeconds(startTime);
	state = 1;
	
	shooter = GetComponent(BulletStatus).shooter;
	shooterTag = GetComponent(BulletStatus).shooterTag;
	str = shooter.GetComponent(Status).atk;
	matk = shooter.GetComponent(Status).matk;
	
	if(shooterTag == "Player" || shooterTag == "Ally"){
		targetTag = "Enemy";
	}else{
		targetTag = "Player";
	}
	Attack();
	yield WaitForSeconds(duration);
	if(destroyEffect){
		Instantiate(destroyEffect, transform.position , transform.rotation);
	}
	Destroy (gameObject);
	//Destroy (gameObject, duration);
}

function Update () {
	if(state == 0){
		var absoluteDirection = transform.rotation * relativeDirection;
   		transform.position += absoluteDirection *speed* Time.deltaTime;
	}else if(state == 1){
		//lockTarget = FindClosestEnemy().transform;
		FindClosestEnemy();
		if(!lockTarget){
			return;
		}
		
		if(attackPoint != mainModel && lockYAxis){
			var destiny : Vector3 = lockTarget.position;
	   	 	destiny.y = transform.position.y;
	  		//transform.LookAt(destiny);
	  		
	  		var targetRotation = Quaternion.LookRotation(destiny - transform.position);
        	transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
	  		
  		}else if(attackPoint != mainModel){
  			transform.LookAt(lockTarget);
  		}
  		
  		attackPoint.LookAt(lockTarget);
  		lockon = true;
  	}

}

// Find the closest enemy 
function FindClosestEnemy() {
    // Find all game objects with tag Enemy
    var distance : float = Mathf.Infinity; 
    var position : Vector3 = transform.position;
    
    var objectsAroundMe : Collider[] = Physics.OverlapSphere(transform.position , radius);
    for(var obj : Collider in objectsAroundMe){
       if(obj.CompareTag(targetTag)){
         var diff : Vector3 = (obj.transform.position - transform.position); 
         var curDistance : float = diff.sqrMagnitude; 
         if (curDistance < distance) { 
         //------------
           lockTarget = obj.transform;
           distance = curDistance;
         } 
       }
     }

}

function Attack(){
	if(GlobalCondition.freezeAll){
		return;
	}
	var k = 0;
	var c = 0;
	while (k < 10){
		//transform.LookAt(target.transform);
		var bulletShootout : Transform;
		while (c < continous && lockTarget && shooter){
			if(attackAnim != ""){
				mainModel.GetComponent.<Animation>().Play(attackAnim);
			}
			if(attackEffect){
				Instantiate(attackEffect, attackPoint.transform.position , attackPoint.transform.rotation);
			}
			bulletShootout = Instantiate(bullet1, attackPoint.transform.position , attackPoint.transform.rotation);
			bulletShootout.GetComponent(BulletStatus).Setting(str , matk , shooterTag , shooter);
						c++;
			yield WaitForSeconds(continueDelay); 
		}
		if(idleAnim != ""){
			mainModel.GetComponent.<Animation>().Play(idleAnim);
		}
		yield WaitForSeconds(delay); 
		c = 0;
	
	}
		//Wait();
}

function OnTriggerEnter(other : Collider){
	if(other.gameObject.tag == "Wall" && state == 0 || other.gameObject.tag == "Enemy" && state == 0) {
		state = 1;
	}
}

@script RequireComponent(BulletStatus)

