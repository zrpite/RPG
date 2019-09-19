#pragma strict
var checkDistance : float = 1.5;
var player : Transform;
private var showUi : boolean = false;
var textStyle : GUIStyle;

function Start(){
	if(!player){
		player = transform.root;
	}
}

function Update(){
	if(GlobalCondition.freezePlayer){
		return;
	}
	if(Input.GetKeyDown("f")){
		ActivateMount();
	}
	
	var fwd: Vector3 = transform.TransformDirection(Vector3.forward);
	var hit : RaycastHit;
	
	if(Physics.Raycast(transform.position, fwd, hit , checkDistance)){
		if(hit.transform.tag == "Mount"){
			showUi = true;
		}else{
			showUi = false;
		}
	}else{
		showUi = false;
	}
}

function OnGUI(){
	if(showUi && !GlobalCondition.freezePlayer){
		GUI.Label(Rect(Screen.width /2 - 250, Screen.height - 65, 500, 50), "Press [F] to ride." , textStyle);
	}
}

function ActivateMount(){
	var fwd: Vector3 = transform.TransformDirection(Vector3.forward);
	var hit : RaycastHit;
	
	if(Physics.Raycast(transform.position, fwd, hit , checkDistance)){
		print(hit.transform.name);
		if(hit.transform.tag == "Mount"){
			hit.transform.GetComponent(MountController).GetOn(player);
			showUi = false;
		}
	}
}