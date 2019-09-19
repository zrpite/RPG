#pragma strict
class TextDialogue{
	var textLine1 : String = "";
	var textLine2 : String = "";
	var textLine3 : String = "";
	var textLine4 : String = "";
}
var message : TextDialogue[] = new TextDialogue[1];

var mainModel : Transform;
var button : Texture2D;
var textWindow : Texture2D;
@HideInInspector
var enter : boolean = false;
private var showGui : boolean = false;
@HideInInspector
var s : int = 0;
@HideInInspector
var player : GameObject;

@HideInInspector
var talkFinish : boolean = false;

var sendMessageWhenDone : String = "";

var textStyle : GUIStyle;
//-------------------------
private var str : String[] = new String[4];
private var line : int = 0;

private var wait : float = 0;
var delay : float = 0.05;
private var begin : boolean = false;
private var i: int = 0;
private var wordComplete : String[] = new String[4];
var freezeTime : boolean = false;
var lookAtNpc : boolean = false;
var npcLookPlayer : boolean = false;
private var rot : boolean = false;

var activateSelf : boolean = true;

function Update(){
	if(lookAtNpc && rot && player){
		var destinya : Vector3 = mainModel.position;
	    destinya.y = player.transform.root.position.y;
  	
		var targetRotation = Quaternion.LookRotation(destinya - player.transform.root.position);
		player.transform.root.rotation = Quaternion.Slerp(player.transform.root.rotation, targetRotation, 8 * Time.unscaledDeltaTime);
	}
	if(npcLookPlayer && rot && player){
		var destinyb : Vector3 = player.transform.position;
	    destinyb.y = mainModel.position.y;
  	
		var targetRotationa = Quaternion.LookRotation(destinyb - mainModel.position);
		mainModel.rotation = Quaternion.Slerp(mainModel.rotation, targetRotationa, 8 * Time.unscaledDeltaTime);
	}
	
	if(Input.GetKeyDown("e") && enter && activateSelf){
		if(s == 0 && GlobalCondition.interacting){
			return;
		}
		NextPage();
	}
	if(begin){
	  	if(wait >= delay){
	  		if(wordComplete[line].Length > 0)
	  			str[line] += wordComplete[line][i++];
	        wait = 0;
	        if(i >= wordComplete[line].Length && line > 2){
	        	begin = false;
	        }else if(i >= wordComplete[line].Length){
	        	i = 0;
	        	line++;
	        }
	     }else{
	      	//wait += Time.deltaTime;
	      	wait += Time.unscaledDeltaTime;
	     }
	 
	 }
}

function ForceRotation(){
	rot = true;
	if(!freezeTime){
		yield WaitForSeconds(1);
	}else{
		yield WaitForSeconds(0.1);
	}
	if(lookAtNpc){
		LookAtMe();
	}
	if(npcLookPlayer){
		LookPlayer();
	}
	rot = false;
}

function AnimateText(strComplete : String , strComplete2 : String , strComplete3 : String , strComplete4 : String){
	begin = false;
	i = 0;
	str[0] = "";
	str[1] = "";
	str[2] = "";
	str[3] = "";
	line = 0;
	wordComplete[0] = strComplete;
	wordComplete[1] = strComplete2;
	wordComplete[2] = strComplete3;
	wordComplete[3] = strComplete4;
	begin = true;
}

function OnTriggerEnter(other : Collider) {
	if(other.tag == "Player"){
		s = 0;
		talkFinish = false;
		player = other.gameObject;
		enter = true;
	}

}

function OnTriggerExit(other : Collider) {
	if(other.tag == "Player"){
		s = 0;
		enter = false;
		CloseTalk();
	}

}

function CloseTalk(){
	showGui = false;
	Time.timeScale = 1.0;
	//Screen.lockCursor = true;
	Cursor.lockState = CursorLockMode.Locked;
	Cursor.visible = false;
	GlobalCondition.freezeAll = false;
	s = 0;
}

function NextPage(){
	if(!enter){
		return;
	}
	if(s == 0 && player){
		ForceRotation();
	}
	if(begin){
		str[0] = wordComplete[0];
		str[1] = wordComplete[1];
		str[2] = wordComplete[2];
		str[3] = wordComplete[3];
		begin = false;
		return;
	}
	s++;
	if(s > message.Length){
		showGui = false;
		talkFinish = true;
		CloseTalk();
		if(sendMessageWhenDone != ""){
			gameObject.SendMessage(sendMessageWhenDone , SendMessageOptions.DontRequireReceiver);
		}
	}else{
		if(freezeTime){
			Time.timeScale = 0.0;
		}else{
			GlobalCondition.freezeAll = true;
		}
		talkFinish = false;
		//Screen.lockCursor = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		
		showGui = true;
		AnimateText(message[s-1].textLine1 , message[s-1].textLine2 , message[s-1].textLine3 , message[s-1].textLine4);
	}
}

function OnGUI(){
	if(!player){
		return;
	}
	if(enter && !showGui && !GlobalCondition.interacting && activateSelf){
		//GUI.DrawTexture(Rect(Screen.width / 2 - 130, Screen.height - 120, 260, 80), button);
		if (GUI.Button (Rect(Screen.width / 2 - 130, Screen.height - 180, 260, 80), button)){
			NextPage();
		}
	}
	
	if(showGui && s <= message.Length){
		GUI.DrawTexture(Rect(Screen.width /2 - 308, Screen.height - 255, 615, 220), textWindow);
		GUI.Label (Rect (Screen.width /2 - 263, Screen.height - 220, 500, 200), str[0] , textStyle);
		GUI.Label (Rect (Screen.width /2 - 263, Screen.height - 190, 500, 200), str[1] , textStyle);
		GUI.Label (Rect (Screen.width /2 - 263, Screen.height - 160, 500, 200), str[2] , textStyle);
		GUI.Label (Rect (Screen.width /2 - 263, Screen.height - 130, 500, 200), str[3] , textStyle);
		if (GUI.Button (Rect (Screen.width /2 + 160,Screen.height - 100,100,30), "Next")) {
			NextPage();
		}
	}

}

function LookAtMe(){
	var lookTo : Vector3 = mainModel.position;
    lookTo.y = player.transform.root.position.y;
  	player.transform.root.LookAt(lookTo);
}

function LookPlayer(){
	var lookTo : Vector3 = player.transform.position;
    lookTo.y = mainModel.position.y;
  	mainModel.LookAt(lookTo);
}