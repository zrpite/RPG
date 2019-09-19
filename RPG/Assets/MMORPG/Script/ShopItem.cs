using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 商店物品功能类
/// </summary>
public class ShopItem : MonoBehaviour 
{
	public int id = 0;
	public UISprite icon;
	public UILabel infoText;
	/// <summary>
	/// 设置对应物品信息
	/// </summary>
	/// <param name="id"></param>
	public void SetThisShopItemById(int id)
	{
		this.id = id;
		ObjectInfo info = ObjectsInfo.Instance().GetObjectInfoById(id);
		icon.spriteName = info.icon_name;
		string message = "";
		switch(info.type)
		{
			case ObjectInfo.ObjectType.Drug : message = GetDrugMessage(info); break;
			case ObjectInfo.ObjectType.Equip : message = GetEquipMessage(info); break;
		}
		infoText.text = message;
	}
	/// <summary>
	/// 获取药品描述
	/// </summary>
	/// <param name="info"></param>
	/// <returns></returns>
	public string GetDrugMessage(ObjectInfo info)
	{
		string str = "";
		str += info.name + "\n\n";
        str += "+HP: " + info.hp + "\n";
        str += "+MP: " + info.mp + "    ";
        str += "购买价: " + info.price_buy;
		return str;
	}
	/// <summary>
	/// 获取装备描述
	/// </summary>
	/// <param name="info"></param>
	/// <returns></returns>
	public string GetEquipMessage(ObjectInfo info)
	{
		string str = "";
		string strwear = "";
		string strsuit = "";
		switch(info.weartype)
		{
			case ObjectInfo.WearType.Headgear : strwear = "帽子"; break;
            case ObjectInfo.WearType.Armor : strwear = "护甲"; break;
            case ObjectInfo.WearType.Hand : strwear = "武器"; break;
            case ObjectInfo.WearType.Accessory : strwear = "配件"; break;
            case ObjectInfo.WearType.Shoe : strwear = "鞋"; break;
		}
		switch(info.suittype)
		{
			case PlayerStatusInfo.Playertype.Swordman : strsuit = "剑士" ; break;
        	case PlayerStatusInfo.Playertype.Magician : strsuit = "魔法师" ; break;
		}
		str += info.name + "\n";
        str += "攻击:" + info.attack + " ";
        str += "防御:" + info.def + " ";
		str += "速度:" + info.speed + "\n";
		str += "类型:" + strwear + " ";
		str += "角色:" + strsuit + "\n";
        str += "购买价：" + info.price_buy;
		return str;
	}
	/// <summary>
	/// Buy按钮回调
	/// </summary>
	public void BuyButtonClick()
	{
		int price = ObjectsInfo.Instance().GetObjectInfoById(id).price_buy;
		Debug.Log("价格为" + price);
		if(CoinManager.Instance().ReduceCoin(price))
		{
			//添加进背包
			BagBoard.Instance().PickOneItemById(id);
		}
		else
		{
			Debug.Log("金币不足");
		}
	}
}
