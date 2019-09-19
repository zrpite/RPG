#pragma strict
import UnityEngine.UI;

var player : GameObject;

var moneyText : Text;

var itemIcons : Image[] = new Image[16];
var itemQty : Text[] = new Text[16];
var equipmentIcons : Image[] = new Image[16];

var weaponIcons : Image;
var subWeaponIcons : Image;
var armorIcons : Image;
var accIcons : Image;

var tooltip : GameObject;
var tooltipIcon : Image;
var tooltipName : Text;
var tooltipText1 : Text;
var tooltipText2 : Text;
var tooltipText3 : Text;

var usableTab : GameObject;
var equipmentTab : GameObject;

var database : GameObject;
private var db : ItemData; 

function Start(){
	db = database.GetComponent(ItemData);
}

function Update(){
	if(tooltip && tooltip.activeSelf == true){
		var tooltipPos : Vector2 = Input.mousePosition;
		tooltipPos.x += 7;
		tooltip.transform.position = tooltipPos;
	}
	if(!player){
		return;
	}
	//itemIcons[0].GetComponent(Image).sprite = db.usableItem[player.GetComponent(Inventory).itemSlot[0]].iconSprite;
	
	for(var a : int = 0; a < itemIcons.Length; a++){
		itemIcons[a].GetComponent(Image).sprite = db.usableItem[player.GetComponent(Inventory).itemSlot[a]].iconSprite;
		itemIcons[a].GetComponent(Image).color = db.usableItem[player.GetComponent(Inventory).itemSlot[a]].spriteColor;
	}
	
	for(var q : int = 0; q < itemQty.Length; q++){
		var qty : String = player.GetComponent(Inventory).itemQuantity[q].ToString();
		if(qty == "0"){
			qty = "";
		}
		itemQty[q].GetComponent(Text).text = qty;
	}
	
	for(var b : int = 0; b < equipmentIcons.Length; b++){
		equipmentIcons[b].GetComponent(Image).sprite = db.equipment[player.GetComponent(Inventory).equipment[b]].iconSprite;
		equipmentIcons[b].GetComponent(Image).color = db.equipment[player.GetComponent(Inventory).equipment[b]].spriteColor;
	}
	
	if(weaponIcons){
		weaponIcons.GetComponent(Image).sprite = db.equipment[player.GetComponent(Inventory).weaponEquip].iconSprite;
		weaponIcons.GetComponent(Image).color = db.equipment[player.GetComponent(Inventory).weaponEquip].spriteColor;
	}
	if(subWeaponIcons){
		subWeaponIcons.GetComponent(Image).sprite = db.equipment[player.GetComponent(Inventory).subWeaponEquip].iconSprite;
		subWeaponIcons.GetComponent(Image).color = db.equipment[player.GetComponent(Inventory).subWeaponEquip].spriteColor;
	}
	if(armorIcons){
		armorIcons.GetComponent(Image).sprite = db.equipment[player.GetComponent(Inventory).armorEquip].iconSprite;
		armorIcons.GetComponent(Image).color = db.equipment[player.GetComponent(Inventory).armorEquip].spriteColor;
	}
	if(accIcons){
		accIcons.GetComponent(Image).sprite = db.equipment[player.GetComponent(Inventory).accessoryEquip].iconSprite;
		accIcons.GetComponent(Image).color = db.equipment[player.GetComponent(Inventory).accessoryEquip].spriteColor;
	}
	if(moneyText){
		moneyText.GetComponent(Text).text = player.GetComponent(Inventory).cash.ToString();
	}
}

function ShowItemTooltip(slot : int){
	if(!tooltip || !player){
		return;
	}
	if(player.GetComponent(Inventory).itemSlot[slot] <= 0){
		HideTooltip();
		return;
	}
	
	tooltipIcon.GetComponent(Image).sprite = db.usableItem[player.GetComponent(Inventory).itemSlot[slot]].iconSprite;
	tooltipName.GetComponent(Text).text = db.usableItem[player.GetComponent(Inventory).itemSlot[slot]].itemName;
	
	tooltipText1.GetComponent(Text).text = db.usableItem[player.GetComponent(Inventory).itemSlot[slot]].description;
	tooltipText2.GetComponent(Text).text = db.usableItem[player.GetComponent(Inventory).itemSlot[slot]].description2;
	tooltipText3.GetComponent(Text).text = db.usableItem[player.GetComponent(Inventory).itemSlot[slot]].description3;
	
	tooltip.SetActive(true);
}

function ShowEquipmentTooltip(slot : int){
	if(!tooltip || !player){
		return;
	}
	if(player.GetComponent(Inventory).equipment[slot] <= 0){
		HideTooltip();
		return;
	}
	
	tooltipIcon.GetComponent(Image).sprite = db.equipment[player.GetComponent(Inventory).equipment[slot]].iconSprite;
	tooltipName.GetComponent(Text).text = db.equipment[player.GetComponent(Inventory).equipment[slot]].itemName;
	
	tooltipText1.GetComponent(Text).text = db.equipment[player.GetComponent(Inventory).equipment[slot]].description;
	tooltipText2.GetComponent(Text).text = db.equipment[player.GetComponent(Inventory).equipment[slot]].description2;
	tooltipText3.GetComponent(Text).text = db.equipment[player.GetComponent(Inventory).equipment[slot]].description3;
	
	tooltip.SetActive(true);
}

function ShowOnEquipTooltip(type : int){
	if(!tooltip || !player){
		return;
	}
	//0 = Weapon, 1 = Armor, 2 = Accessories , 3 = Sub Weapon
	var id : int = 0;
	if(type == 0){
		id = player.GetComponent(Inventory).weaponEquip;
	}
	if(type == 1){
		id = player.GetComponent(Inventory).armorEquip;
	}
	if(type == 2){
		id = player.GetComponent(Inventory).accessoryEquip;
	}
	if(type == 3){
		id = player.GetComponent(Inventory).subWeaponEquip;
	}
	
	if(id <= 0){
		HideTooltip();
		return;
	}
	
	tooltipIcon.GetComponent(Image).sprite = db.equipment[id].iconSprite;
	tooltipName.GetComponent(Text).text = db.equipment[id].itemName;
	
	tooltipText1.GetComponent(Text).text = db.equipment[id].description;
	tooltipText2.GetComponent(Text).text = db.equipment[id].description2;
	tooltipText3.GetComponent(Text).text = db.equipment[id].description3;
	
	tooltip.SetActive(true);
}

function HideTooltip(){
	if(!tooltip){
		return;
	}
	tooltip.SetActive(false);
}

function UseItem(itemSlot : int){
	if(!player){
		return;
	}
	player.GetComponent(Inventory).UseItem(itemSlot);
	ShowItemTooltip(itemSlot);
}

function EquipItem(itemSlot : int){
	if(!player){
		return;
	}
	player.GetComponent(Inventory).EquipItem(player.GetComponent(Inventory).equipment[itemSlot] , itemSlot);
	ShowEquipmentTooltip(itemSlot);
}

function UnEquip(type : int){
	//0 = Weapon, 1 = Armor, 2 = Accessories
	if(!player){
		return;
	}
	var id : int = 0;
	if(type == 0){
		id = player.GetComponent(Inventory).weaponEquip;
	}
	if(type == 1){
		id = player.GetComponent(Inventory).armorEquip;
	}
	if(type == 2){
		id = player.GetComponent(Inventory).accessoryEquip;
	}
	player.GetComponent(Inventory).UnEquip(id);
	ShowOnEquipTooltip(type);
}

function SwapWeapon(){
	if(!player){
		return;
	}
	player.GetComponent(Inventory).SwapWeapon();
	ShowOnEquipTooltip(3);
}

function CloseMenu(){
	Time.timeScale = 1.0;
	//Screen.lockCursor = true;
	Cursor.lockState = CursorLockMode.Locked;
	Cursor.visible = false;
	gameObject.SetActive(false);
}

function OpenUsableTab(){
	usableTab.SetActive(true);
	equipmentTab.SetActive(false);
}

function OpenEquipmentTab(){
	usableTab.SetActive(false);
	equipmentTab.SetActive(true);
}

