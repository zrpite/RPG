#pragma strict
import UnityEngine.UI;

var hpBar : Image;
var mpBar : Image;
var expBar : Image;
var hpText : Text;
var mpText : Text;
var lvText : Text;
var player : GameObject;

function Start(){
	DontDestroyOnLoad(transform.gameObject);
	if(!player){
		player = GameObject.FindWithTag("Player");
	}
}

function Update(){
	if(!player){
		Destroy(gameObject);
		return;
	}
	var stat : Status = player.GetComponent(Status);
	
	var maxHp : int = stat.totalMaxHealth;
	var hp : float = stat.health;
	var maxMp : int = stat.totalMaxMana;
	var mp : float = stat.mana;
	var exp : int = stat.exp;
	var maxExp : float = stat.maxExp;
	//float target = (float)cur_hp / (float)cur_mhp;
	var curHp : float = hp/maxHp;
	var curMp : float = mp/maxMp;
	var curExp : float = exp/maxExp;
	
	//HP Gauge
	if(curHp > hpBar.fillAmount){
		hpBar.fillAmount += 1 / 1 * Time.unscaledDeltaTime;
		if(hpBar.fillAmount > curHp){
			hpBar.fillAmount = curHp;
		}
	}	
	if(curHp < hpBar.fillAmount){
		hpBar.fillAmount -= 1 / 1 * Time.unscaledDeltaTime;
		if(hpBar.fillAmount < curHp){
			hpBar.fillAmount = curHp;
		}
	}
	
	//MP Gauge
	if(curMp > mpBar.fillAmount){
		mpBar.fillAmount += 1 / 1 * Time.unscaledDeltaTime;
		if(mpBar.fillAmount > curMp){
			mpBar.fillAmount = curMp;
		}
	}	
	if(curMp < mpBar.fillAmount){
		mpBar.fillAmount -= 1 / 1 * Time.unscaledDeltaTime;
		if(mpBar.fillAmount < curMp){
			mpBar.fillAmount = curMp;
		}
	}
	
	//EXP Gauge
	if(expBar){
		expBar.fillAmount = curExp;
	}
	if(lvText){
		lvText.text = stat.level.ToString();
	}
	if(hpText){
		hpText.text = hp.ToString() + "/" + maxHp.ToString();
	}
	if(mpText){
		mpText.text = mp.ToString() + "/" + maxMp.ToString();
	}
	
}