using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单一物品属性类
/// </summary>
public class ObjectInfo : MonoBehaviour 
{
	public enum ObjectType 
    {
    	Drug,
    	Equip
	}
    public enum WearType
    {
        //帽子
        Headgear, 
        //盔甲
        Armor,
        //手持
        Hand,
        //饰品配件
        Accessory,
        //鞋
        Shoe

    }	
    /// <summary>
    /// 物品id
    /// </summary>
    public int id;
    /// <summary>
    /// 物品名
    /// </summary>
    public string name;
    // <summary>
    /// 物品对应图标名
    /// </summary>
    public string icon_name;
    // <summary>
    /// 物品类型
    /// </summary>
    public ObjectType type;
    // <summary>
    /// 药品恢复血量值
    /// </summary>
    public int hp;
    /// <summary>
    /// 药品回复魔法值
    /// </summary>
    public int mp;
    /// <summary>
    /// 物品售价
    /// </summary>
    public int price_sell;
    /// <summary>
    /// 物品购入价
    /// </summary>
    public int price_buy;


    /// <summary>
    /// 攻击力
    /// </summary>
    public int attack;
    /// <summary>
    /// 防御力
    /// </summary>
    public int def;
    /// <summary>
    /// 速度
    /// </summary>
    public int speed;
    /// <summary>
    /// 穿戴部位
    /// </summary>
    public WearType weartype;
    /// <summary>
    /// 适用角色
    /// </summary>
    public PlayerStatusInfo.Playertype suittype;

}
