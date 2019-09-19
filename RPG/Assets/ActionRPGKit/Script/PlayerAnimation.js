#pragma strict
public var runMaxAnimationSpeed : float = 1.0;
public var backMaxAnimationSpeed : float = 1.0;
public var sprintAnimationSpeed : float = 1.5;

private var player : GameObject;
private var mainModel : GameObject;

//var idle : String = "idle";
var idle : AnimationClip;
var run : AnimationClip;
var right : AnimationClip;
var left : AnimationClip;
var back : AnimationClip;
var jump : AnimationClip;
var jumpUp : AnimationClip;
var hurt : AnimationClip;
var doubleJump : AnimationClip;

var joyStick : GameObject; // For Mobile
private var moveHorizontal : float;
private var moveVertical : float;

function Start () {
	if(!player){
		player = this.gameObject;
	}
	mainModel = GetComponent(AttackTrigger).mainModel;
	if(!mainModel){
		mainModel = this.gameObject;
	}
	GetComponent(AttackTrigger).useMecanim = false;
	mainModel.GetComponent.<Animation>()[run.name].speed = runMaxAnimationSpeed;
	mainModel.GetComponent.<Animation>()[right.name].speed = runMaxAnimationSpeed;
	mainModel.GetComponent.<Animation>()[left.name].speed = runMaxAnimationSpeed;
	mainModel.GetComponent.<Animation>()[back.name].speed = backMaxAnimationSpeed;
	
	//mainModel.GetComponent.<Animation>()[jump.name].wrapMode  = WrapMode.ClampForever;
	
	if(hurt){
		mainModel.GetComponent.<Animation>()[hurt.name].layer = 5;
	}
	
}

function Update () {
	if(Time.timeScale == 0.0f){
		return;
	}
	if(GlobalCondition.freezeAll || GlobalCondition.freezePlayer){
		mainModel.GetComponent.<Animation>().CrossFade(idle.name);
		return;
	}
    var controller : CharacterController = player.GetComponent(CharacterController);
    
    if(joyStick){
		if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");
		}else{
			moveHorizontal = joyStick.GetComponent(MobileJoyStick).position.x;
			moveVertical = joyStick.GetComponent(MobileJoyStick).position.y;
		}
	}else{
		moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");
	}
		
    if ((controller.collisionFlags & CollisionFlags.Below) != 0){
       if(moveHorizontal > 0.1)
	      mainModel.GetComponent.<Animation>().CrossFade(right.name);
	   else if(moveHorizontal < -0.1)
	      mainModel.GetComponent.<Animation>().CrossFade(left.name);
	   else if(moveVertical > 0.1)
	      mainModel.GetComponent.<Animation>().CrossFade(run.name);
	   else if(moveVertical < -0.1)
	      mainModel.GetComponent.<Animation>().CrossFade(back.name);
	   else
	      mainModel.GetComponent.<Animation>().CrossFade(idle.name);
	      
	    if(Input.GetButtonDown("Jump") && jumpUp){
			mainModel.GetComponent.<Animation>()[jumpUp.name].layer = 2;
			mainModel.GetComponent.<Animation>().PlayQueued(jumpUp.name , QueueMode.PlayNow);
			//mainModel.GetComponent.<Animation>().Blend(jumpUp.name , 0.8 , 0.3);
		}
	}else{
		mainModel.GetComponent.<Animation>().CrossFade(jump.name);
	}
}

function DoubleJumpAnimation(){
	if(!doubleJump){
		return;
	}
	mainModel.GetComponent.<Animation>()[doubleJump.name].layer = 2;
	mainModel.GetComponent.<Animation>().PlayQueued(doubleJump.name , QueueMode.PlayNow);
}

function AnimationSpeedSet(){
	mainModel = GetComponent(AttackTrigger).mainModel;
	if(!mainModel){
		mainModel = this.gameObject;
	}
	mainModel.GetComponent.<Animation>()[run.name].speed = runMaxAnimationSpeed;
	mainModel.GetComponent.<Animation>()[right.name].speed = runMaxAnimationSpeed;
	mainModel.GetComponent.<Animation>()[left.name].speed = runMaxAnimationSpeed;
	mainModel.GetComponent.<Animation>()[back.name].speed = backMaxAnimationSpeed;
}

/*function OnLand(){
	if(doubleJump){
		mainModel.GetComponent.<Animation>().Stop(doubleJump.name);
	}
		
	if(jumpUp){
		mainModel.GetComponent.<Animation>().Stop(jumpUp.name);
	}

}*/

@script RequireComponent(AttackTrigger)
@script AddComponentMenu("Action-RPG Kit/Create Player(Legacy)")
