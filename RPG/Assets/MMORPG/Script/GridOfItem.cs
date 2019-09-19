using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包栏管理
/// </summary>
public class GridOfItem : MonoBehaviour 
{
	/// <summary>
	/// 当前Grid中物品的id(默认值为0,可以判别是否是空Grid)
	/// </summary>
	public int id = 0;
	/// <summary>
	/// 当前Grid中物品的数量
	/// </summary>
	public int num = 0;
	/// <summary>
	/// 当前Grid中物品的ObjectInfo
	/// </summary>
	private ObjectInfo info = null;
	/// <summary>
	/// 当前Grid中物品数量的显示标签(默认关闭,仅当有物品才开启)
	/// </summary>
	public UILabel numLabel;

	/// <summary>
	/// 设置当前Grid中的item(记录了id和ObjectInfo即物品全部信息)
	/// </summary>
	/// <param name="id">item的id</param>
	/// <param name="num">item的数量</param>
	public void SetThisGridItemById(int id, int num = 1)
	{
		this.id = id;
		this.num = num;
		this.info = ObjectsInfo.Instance().GetObjectInfoById(id);
		Item item = this.GetComponentInChildren<Item>();
		item.SetIconAndId(id,this.info.icon_name);
		numLabel.enabled = true;
		numLabel.text = this.num.ToString();
	}
	/// <summary>
	/// 使用药品
	/// </summary>
	public void UseDrug()
	{
		//死人是用不了药的
		if(Global.playerState != Global.State.DieState)
		{
			PlayerStatusManager.Instance().AddHpAndMp(info);
			ReduceItemNum(1);
		}
		
	}
	/// <summary>
	/// 穿戴装备成功后自身处理
	/// </summary>
	public void WearEquip()
	{
		//如果成功穿上了
		if(EquipBoard.Instance().WearEquipmentById(this.id))
		{
			ReduceItemNum();
		}
		
	}
	/// <summary>
	/// 增加当前Grid中的物品数量
	/// </summary>
	/// <param name="num">增加数量</param>
	public void AddItemNum(int num = 1)
	{
		this.num += num;
		numLabel.text = this.num.ToString();
	}
	/// <summary>
	/// 减少当前Grid中的物品数量
	/// </summary>
	/// <param name="num">减少数量</param>
	public void ReduceItemNum(int num = 1)
	{
		this.num -= num;
		//物品用完就删掉它
		if(this.num <= 0)
		{
			ClearItem();
			DestroyImmediate(this.GetComponentInChildren<Item>().transform.gameObject);
		}
		numLabel.text = this.num.ToString();
	}
	/// <summary>
	/// 清空当前Grid中的物品信息
	/// </summary>
	public void ClearItem()
	{
		this.id = 0;
		this.info = null;
		this.num = 0;
		numLabel.enabled = false;
	}
}
