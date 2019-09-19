#pragma strict
import UnityEngine.UI;

class Usable {
	var itemName : String = "";
	var icon : Texture2D;
	var iconSprite : Sprite;
	var spriteColor : Color = Color.white;
	var model : GameObject;
	var description : String = "";
	var description2 : String = "";
	var description3 : String = "";
	var price : int = 10;
	var hpRecover : int = 0;
	var mpRecover : int = 0;
	var atkPlus : int = 0;
	var defPlus : int = 0;
	var matkPlus : int = 0;
	var mdefPlus : int = 0;
	var unusable : boolean = false;
	var sendMsg : String = "";
} 

class Equip {
	var itemName : String = "";
	var icon : Texture2D;
	var iconSprite : Sprite;
	var spriteColor : Color = Color.white;
	var model : GameObject;
	var assignAllWeapon : boolean = true;
	var description : String = "";
	var description2 : String = "";
	var description3 : String = "";
	var price : int = 10;
	var weaponType : int = 0; //Use for Mecanim Weapon ID
	var attack : int = 5;
	var defense : int = 0;
	var magicAttack : int = 0;
	var magicDefense : int = 0;
	var hpBonus : int = 0;
	var mpBonus : int = 0;
		
	enum EqType {
		Weapon = 0,
		Armor = 1,
		Accessory = 2
	}
	var EquipmentType : EqType = EqType.Weapon; 
		
		//Ignore if the equipment type is not weapons
	var attackPrefab : GameObject;
	var attackCombo : AnimationClip[] = new AnimationClip[3];
	var idleAnimation : AnimationClip;
	var runAnimation : AnimationClip;
	var rightAnimation : AnimationClip;
	var leftAnimation : AnimationClip;
	var backAnimation : AnimationClip;
	var jumpAnimation : AnimationClip;
	var jumpUpAnimation : AnimationClip;
	enum whileAtk{
		MeleeFwd = 0,
		Immobile = 1,
		WalkFree = 2
	}
	var whileAttack : whileAtk = whileAtk.MeleeFwd;
	var attackSpeed : float = 0.18;
	var attackDelay : float = 0.12;
	
	var statusResist : Resist;
	var canDoubleJump : boolean = false;
	@Range(0 , 100)
	var autoGuard : int = 0;
	@Range(0 , 100)
	var drainTouch : int = 0;
	@Range(0 , 100)
	var mpReduce : int = 0;
} 


var usableItem : Usable[] = new Usable[3];
var equipment : Equip[] = new Equip[3];
