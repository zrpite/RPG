using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物品功能类
/// </summary>
public class Item : UIDragDropItem 
{
	/// <summary>
	/// 此item的id
	/// </summary>
	public int id;
	/// <summary>
	/// 此item的icon
	/// </summary>
	public UISprite icon;
	/// <summary>
	/// 鼠标是否悬停item上的标志位
	/// </summary>
	public  bool isHover = false;
	/// <summary>
	/// 物品属性展示栏
	/// </summary>
	public GameObject infoFrame;
	/// <summary>
	/// 物品属性展示栏显示信息的Label组件
	/// </summary>
	public UILabel infoText;
	/// <summary>
	/// 判定是否是装备栏中穿着的物品,卸下装备的标志位
	/// </summary>
	public bool equiping = false;
	private GridOfItem gridOfItem;
 
	void Start () 
	{
		base.Start();
		gridOfItem = this.GetComponentInParent<GridOfItem>();
		infoFrame.SetActive(false);
		
	}
	void Update()
	{
		//鼠标悬停其上时显示物品属性展示栏
		if(isHover)
		{
			ShowItemInfoById(id);
			//一般情况下只有鼠标悬停于item上才可右击使用药品
			if(Input.GetMouseButtonDown(1))
			{
				switch(ObjectsInfo.Instance().GetObjectInfoById(id).type)
				{
					case ObjectInfo.ObjectType.Drug : gridOfItem.UseDrug(); break;
					//装备栏中和物品栏中右击装备的操作是不一样的
					case ObjectInfo.ObjectType.Equip : if(!equiping) {gridOfItem.WearEquip();} 
													   else {this.GetComponentInParent<GridOfEquip>().UnloadThisGridEquip();} 
													   break;
				}
			}
		}
		else
		{
			infoFrame.SetActive(false);
		}
	}
	/// <summary>
	/// NGUI内置函数,放下拖拽的物品时会记录下物品放下位置下面的物体
	/// </summary>
	/// <param name="surface">物品放下位置下面的物体</param>
	protected override void OnDragDropRelease(GameObject surface)
	{
		base.OnDragDropRelease(surface);
		if(surface != null)
		{
			//如果放到空格子上
			if(surface.tag == "Grid")
			{
				//如果空格子是自己的格子
				if(surface == this.transform.parent.gameObject)
				{
					ResetPos();
				}
				//如果是别的空格子(就把物品放过去同时清空自己的格子)
				else
				{
					GridOfItem oldGrid = this.GetComponentInParent<GridOfItem>();
					this.transform.parent = surface.transform;
					ResetPos();
					GridOfItem newGrid = surface.GetComponent<GridOfItem>();
					newGrid.SetThisGridItemById(oldGrid.id, oldGrid.num);
					oldGrid.ClearItem();
				}
			}
			//如果放到有物品的格子上,就交换两Grid中物品(由于item在Grid上层,所以surface是item)
			else if(surface.tag == "Item")
			{
				GridOfItem oldGrid = this.GetComponentInParent<GridOfItem>();
				GridOfItem newGrid = surface.GetComponentInParent<GridOfItem>();
				int tempid = oldGrid.id;
				int tempnum = oldGrid.num;
				oldGrid.SetThisGridItemById(newGrid.id, newGrid.num);
				newGrid.SetThisGridItemById(tempid, tempnum);
				//避免交换后会认为原有item仍然处于hover状态导致infoFrame不会消失
				oldGrid.GetComponentInChildren<Item>().isHover = false;
				
			}
			//保留(放到技能栏上的情况)
			else if(false)
			{

			}
		}
		ResetPos();
	}
	/// <summary>
	/// 设置此物品的id和对应显示的icon
	/// </summary>
	/// <param name="id">此物品的id</param>
	/// <param name="name">此物品id对应显示的icon_name</param>
	public void SetIconAndId(int id, string name)
	{
		this.id = id;
		this.icon.spriteName = name;
	}
	/// <summary>
	/// item归位(Grid正中心)
	/// </summary>
	public void ResetPos()
	{
		this.transform.localPosition = Vector3.zero;
	}
	/// <summary>
	/// 在EventTrigger中引用,鼠标悬停时触发
	/// </summary>
    public void OnHoverOver() {
        isHover = true;
    }
	/// <summary>
	/// 在EventTrigger中引用,鼠标离开时触发
	/// </summary>
    public void OnHoverOut() {
        isHover = false;
    }
	/// <summary>
	/// 显示id对应的物品信息和展示栏
	/// </summary>
	/// <param name="id">item的id</param>
	public void ShowItemInfoById(int id)
	{
		infoFrame.SetActive(true);
		ObjectInfo info = ObjectsInfo.Instance().GetObjectInfoById(id);
		string message = "";
		switch(info.type)
		{
			case ObjectInfo.ObjectType.Drug : message = GetDrugMessage(info); break;
			case ObjectInfo.ObjectType.Equip : message = GetEquipMessage(info); break;
		}
		infoText.text = message;
	}
	/// <summary>
	/// 获取药品的info显示信息
	/// </summary>
	/// <param name="info">传入的是对应id的物品ObjectInfo对象</param>
	/// <returns></returns>
	public string GetDrugMessage(ObjectInfo info)
	{
		string str = "";
		str += "名称：" + info.name + "\n\n";
        str += "+HP : " + info.hp + "\n\n";
        str += "+MP：" + info.mp + "\n\n";
        str += "出售价：" + info.price_sell;
		return str;
	}
	/// <summary>
	/// 获取装备的info显示信息
	/// </summary>
	/// <param name="info">传入的是对应id的物品ObjectInfo对象</param>
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
		str += "名称：" + info.name + "\n\n";
        str += "攻击力: " + info.attack + "\n\n";
        str += "防御力：" + info.def + "\n\n";
		str += "速度: " + info.speed + "\n\n";
		str += "类型: " + strwear + "\n\n";
		str += "适用角色：" + strsuit + "\n\n";
        str += "出售价：" + info.price_sell;
		return str;
	}
}
