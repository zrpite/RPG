#pragma strict
var eventId : int = 1;
var conditionValue : int = 1; //If eventVar[eventId] value Greater than or Equal this value. Condition is Pass
var targetObject : GameObject;
var eventAction : EvAction = EvAction.Spawn;
var sendMsgTo : String = "";

var keepUpdate : boolean = false;
var checkFromStart : boolean = true;
var destroyWhenPass : boolean = true;

static var eventVar : int[] = new int[20]; //Stored all event condition variable
static var freezeAll : boolean = false;
static var freezePlayer : boolean = false;
static var interacting : boolean = false;
static var playerId : int = 0; 
//ID List
//0 = Dungeon Gate
//2 = Hidden Dungeon Unlock

function Start(){
	if(checkFromStart){
		CheckCondition();
	}
}

function Update(){
	if(keepUpdate){
		CheckCondition();
	}
}

function CheckCondition(){
	if(eventVar[eventId] >= conditionValue){
		//Pass Condition
		if(eventAction == EvAction.Spawn){
			var ob : GameObject = Instantiate(targetObject , transform.position , transform.rotation);
			if(sendMsgTo != ""){
				ob.SendMessage(sendMsgTo , SendMessageOptions.DontRequireReceiver);
			}
		}
		if(eventAction == EvAction.Enable && targetObject){
			targetObject.SetActive(true);
			if(sendMsgTo != ""){
				targetObject.SendMessage(sendMsgTo , SendMessageOptions.DontRequireReceiver);
			}
		}
		if(eventAction == EvAction.Delete && targetObject){
			Destroy(targetObject);
		}
		if(eventAction == EvAction.None && targetObject){
			if(sendMsgTo != ""){
				targetObject.SendMessage(sendMsgTo , SendMessageOptions.DontRequireReceiver);
			}
		}
		if(destroyWhenPass){
			Destroy(gameObject);
		}
	}
}

enum EvAction{
	Spawn = 0,
	Enable = 1,
	Delete = 2,
	None = 3
}