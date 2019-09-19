using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 装备系统
/// </summary>
public class EquipBoard : TweenPosCtrl 
{

	private static EquipBoard _instance = null;
	public static EquipBoard Instance()
	{
		return _instance;
	}
	public GridOfEquip headgear,armor,hand,accessory,shoe;
	public GameObject newEquip;
	
	void Awake()
	{
		_instance = this;
        base.Init();
	}
	
	/// <summary>
	/// 穿戴情况处理
	/// </summary>
	/// <param name="id">要穿戴装备id</param>
	/// <returns>成功穿戴return true</returns>
	public bool WearEquipmentById(int id)
	{
		ObjectInfo info = ObjectsInfo.Instance().GetObjectInfoById(id);
		if(info.suittype != PlayerStatusInfo.Instance().playertype)
		{
			return false;
		}
		GridOfEquip tempGrid = null;
		switch(info.weartype)
		{
			case ObjectInfo.WearType.Headgear :  tempGrid = headgear;  												 break;
			case ObjectInfo.WearType.Armor : 	 tempGrid = armor; 	  												 break;
			case ObjectInfo.WearType.Hand : 	 tempGrid = hand;    AppearChange.Instance().SwitchWeaponAppear(id); break; //目前只有武器可更换外观
			case ObjectInfo.WearType.Accessory : tempGrid = accessory; 												 break;
			case ObjectInfo.WearType.Shoe : 	 tempGrid = shoe;      												 break;
		}
		//对应装备栏已经有装备的情况,先卸下装备,使装备栏空且物品栏加入改装备
		if(tempGrid.id != 0)
		{
			tempGrid.UnloadThisGridEquip();
			Debug.Log("脱");
		}
		//向空的装备栏中添加装备
		Debug.Log("穿");
		GameObject equipNewItem = NGUITools.AddChild(tempGrid.gameObject,newEquip);
		equipNewItem.transform.localPosition = Vector3.zero;
		equipNewItem.GetComponent<UISprite>().depth = 2;
		tempGrid.SetThisGridEquipById(id, info);
		return true;
	}
	/// <summary>
	/// 关闭面板回调
	/// </summary>
	public void CloseButtonClick()
	{
		BoardManager.Instance().SwitchShowBoard(BoardManager.BoardShow.NONE);
		Hide();
	}
	public void Hide()
	{
		Global.playerState = Global.State.NormalState;
		tweenPos.PlayReverse();
	}
	public void Show()
	{
		Global.playerState = Global.State.TipState;
		tweenPos.PlayForward();
	}
}
