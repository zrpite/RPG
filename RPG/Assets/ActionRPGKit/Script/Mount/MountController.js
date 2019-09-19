#pragma strict
var mainModel : GameObject;
var runSpeed : float = 9;
var getOnPosition : Transform;
var getOffPosition : Transform;
var mountPoint : Transform;
var useMecanim : boolean = false;
private var playerMecanim : boolean = false;
var walkingSound : AudioClip;

var legacyAnimationSet : MountLegacy;
var mecanimAnimator : Animator;

private var onRiding : boolean = false;
private var player : Transform;
private var motor : CharacterMotor;
private var freeze : boolean = false;
private var moveHorizontal : float = 0;
private var moveVertical : float = 0;
private var onGetOff : boolean = false;
var cameraChangeTarget : boolean = true;

function Start(){
	DontDestroyOnLoad(transform.gameObject);
	if(!mainModel){
		mainModel = this.gameObject;
	}
	if(!mecanimAnimator && useMecanim){
		mecanimAnimator = mainModel.GetComponent(Animator);
	}
	motor = GetComponent(CharacterMotor);
	
	motor.movement.maxForwardSpeed = runSpeed;
	motor.movement.maxSidewaysSpeed = runSpeed;
}

function Update(){
	if(onGetOff && getOffPosition){
		PlayerMoveTowardsTarget(getOffPosition.position);
	}
	if(onRiding && !player){
		GlobalCondition.freezePlayer = false;
		onRiding = false;
		if(!useMecanim){
			mainModel.GetComponent.<Animation>().CrossFade(legacyAnimationSet.idleAnimation.name);
		}else{
			mecanimAnimator.SetBool("run" , false);
		}
	}
	if(GlobalCondition.freezeAll || freeze){
		motor.inputMoveDirection = Vector3(0,0,0);
		return;
	}
	if(Time.timeScale == 0.0){
    	return;
    }
    
	if(Input.GetKeyDown("f") && onRiding || Input.GetButtonDown("Jump") && onRiding){
		GetOff();
	}
	
	if(onRiding){
		if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");
		}else{
			moveHorizontal = 0;
			moveVertical = 0;
		}
		
		var cameraTransform = Camera.main.transform;
		var forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		var right = Vector3(forward.z, 0, -forward.x);
		var targetDirection = moveHorizontal * right + moveVertical * forward;
		
		//----------------------------------
		if(moveHorizontal != 0 || moveVertical != 0){
	    	transform.rotation = Quaternion.LookRotation(targetDirection.normalized);
	    }
		//-----------------------------------------------------------------------------
		if(moveVertical != 0 && walkingSound && !GetComponent.<AudioSource>().isPlaying|| moveHorizontal != 0 && walkingSound && !GetComponent.<AudioSource>().isPlaying){
			GetComponent.<AudioSource>().clip = walkingSound;
			GetComponent.<AudioSource>().Play();
		}
		
		motor.inputMoveDirection = targetDirection.normalized;
		
		if(!useMecanim){
			//Play Legacy Animation
			if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
	     		mainModel.GetComponent.<Animation>().CrossFade(legacyAnimationSet.movingAnimation.name);
			}else{
				mainModel.GetComponent.<Animation>().CrossFade(legacyAnimationSet.idleAnimation.name);
			}
		}else{
			//Play Mecanim Animation
			if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
	     		mecanimAnimator.SetBool("run" , true);
			}else{
				mecanimAnimator.SetBool("run" , false);
			}
		}
	}
}

function GetOn(pl : Transform){
	if(onRiding){
		return;
	}
	player = pl;
	playerMecanim = player.GetComponent(AttackTrigger).useMecanim;
	onRiding = true;
	Physics.IgnoreCollision(GetComponent(Collider), player.GetComponent(Collider));
	player.position = getOnPosition.position;
	player.rotation = getOnPosition.rotation;
	if(!mountPoint){
		mountPoint = this.transform;
	}
	player.parent = mountPoint;
	GlobalCondition.freezePlayer = true;
	player.GetComponent(CharacterMotor).enabled = false;
	freeze = true;
	var mm : GameObject = player.GetComponent(AttackTrigger).mainModel;
	//Play Animation
	if(!playerMecanim){
		//Player Use Legacy
		mm.GetComponent(Animation)[legacyAnimationSet.getOnAnimation.name].layer = 2;
		mm.GetComponent(Animation).Play(legacyAnimationSet.getOnAnimation.name);
		yield WaitForSeconds(mm.GetComponent(Animation)[legacyAnimationSet.getOnAnimation.name].length);
		mm.GetComponent(Animation).Stop(legacyAnimationSet.getOnAnimation.name);
		mm.GetComponent(Animation)[legacyAnimationSet.ridingAnimation.name].layer = 2;
		mm.GetComponent(Animation).Play(legacyAnimationSet.ridingAnimation.name);
	}else{
		//Player Use Mecanim
		var anim : Animator = mm.GetComponent(Animator);
		anim.SetTrigger("riding");
		yield WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
	}
	if(cameraChangeTarget){
		SetCameraMode();
		Camera.main.GetComponent(ARPGcamera).target = this.transform;
	}
	freeze = false;
}

function GetOff(){
	onRiding = false;
	//Set Animation to Idle when player get off.
	if(!useMecanim){
		mainModel.GetComponent.<Animation>().CrossFade(legacyAnimationSet.idleAnimation.name);
	}else{
		mecanimAnimator.SetBool("run" , false);
	}
	
	player.GetComponent(CharacterMotor).enabled = true;
	freeze = true;
	var mm : GameObject = player.GetComponent(AttackTrigger).mainModel;
	//Play Animation
	if(!playerMecanim){
		//Player Use Legacy
		mm.GetComponent(Animation).Stop(legacyAnimationSet.ridingAnimation.name);
		mm.GetComponent(Animation)[legacyAnimationSet.getOffAnimation.name].layer = 2;
		mm.GetComponent(Animation).Play(legacyAnimationSet.getOffAnimation.name);
		yield WaitForSeconds(mm.GetComponent(Animation)[legacyAnimationSet.getOffAnimation.name].length);
		mm.GetComponent(Animation).Stop(legacyAnimationSet.getOffAnimation.name);
	}else{
		//Player Use Mecanim
		var anim : Animator = mm.GetComponent(Animator);
		anim.SetTrigger("getoff");
		yield WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
	}
	player.parent = null;
	//player.position = getOffPosition.position;
	onGetOff = true;
	yield WaitForSeconds(0.12);
	onGetOff = false;
	Physics.IgnoreCollision(GetComponent(Collider), player.GetComponent(Collider) , false);
	
	if(cameraChangeTarget){
		SetCameraMode();
		Camera.main.GetComponent(ARPGcamera).target = player;
	}
	freeze = false;
	GlobalCondition.freezePlayer = false;
}

function Deactivate(){
	//This function use when player Load on Quit Game while riding.
	onRiding = false;
	player.GetComponent(CharacterMotor).enabled = true;
	player.parent = null;
	if(cameraChangeTarget){
		SetCameraMode();
		Camera.main.GetComponent(ARPGcamera).target = player;
	}
	
	var mm : GameObject = player.GetComponent(AttackTrigger).mainModel;
	if(!playerMecanim){
		//Player Use Legacy
		mm.GetComponent(Animation).Stop(legacyAnimationSet.ridingAnimation.name);
	}else{
		//Player Use Mecanim
		var anim : Animator = mm.GetComponent(Animator);
		anim.SetTrigger("getoff");
	}
	freeze = false;
	GlobalCondition.freezePlayer = false;
	Destroy(gameObject);
}

function PlayerMoveTowardsTarget(targetPoint : Vector3){
	var cc = player.GetComponent(CharacterController);
	var offset = targetPoint - player.transform.position;
	
	if(offset.magnitude > 0.1f) {
		offset = offset.normalized * 15;
		cc.Move(offset * Time.deltaTime);
	}
}

function DestroySelf(){
	if(!onRiding){
		Destroy(gameObject);
	}
}

function SetCameraMode(){
	//Set Camera Mode to Normal
	if(!player.GetComponent(SwtichCameraAimMode)){
		return;
	}
	player.GetComponent(AttackTrigger).aimingType = AimType.Normal;
	Camera.main.GetComponent(ARPGcamera).target = this.transform;
	Camera.main.GetComponent(ARPGcamera).targetHeight = player.GetComponent(SwtichCameraAimMode).targetHeightNormal;
	Camera.main.GetComponent(ARPGcamera).lockOn = false;
}

@script RequireComponent(CharacterMotor)

class MountLegacy{
	var idleAnimation : AnimationClip;
	var movingAnimation : AnimationClip;

	var getOnAnimation : AnimationClip;
	var getOffAnimation : AnimationClip;
	var ridingAnimation : AnimationClip;
}

