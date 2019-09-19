#pragma strict
var spawnPrefab : Transform;
var randomSpawnPoint : Transform[] = new Transform[1];
var continuous : int = 3;
var continuousDelay : float = 0.5;

var setRotation : boolean = false;
private var shooterTag : String = "Player";
private var shooter : GameObject;

private var wait : float = 0;
private var sp : int = 0;
private var playerAttack : int = 5;

function Start () {
	if(setRotation){
		transform.eulerAngles = new Vector3(0 , transform.eulerAngles.y , 0);
	}
	shooterTag = GetComponent(BulletStatus).shooterTag;
	shooter = GetComponent(BulletStatus).shooter;
	playerAttack = GetComponent(BulletStatus).playerAttack;
}

function Update () {
	if(wait >= continuousDelay){
		SpawnBullet();
		wait = 0;
	}else{
		wait += Time.deltaTime;
	}
}

function SpawnBullet(){
	var ran : int = Random.Range(0 , randomSpawnPoint.Length);
	
	var bulletShootout : Transform = Instantiate(spawnPrefab, randomSpawnPoint[ran].position , randomSpawnPoint[ran].rotation);
	bulletShootout.GetComponent(BulletStatus).Setting(playerAttack , playerAttack , shooterTag , shooter);
	
	sp++;
	if(sp >= continuous){
		Destroy(gameObject);
	}
}

@script RequireComponent(BulletStatus)