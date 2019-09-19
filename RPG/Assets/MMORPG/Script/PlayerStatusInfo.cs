using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家属性信息类
/// </summary>
public class PlayerStatusInfo : MonoBehaviour 
{
	private static PlayerStatusInfo _instance = null;
	public static PlayerStatusInfo Instance()
	{
		return _instance;
	}
	/// <summary>
	/// 角色类型
	/// </summary>
	public enum Playertype
	{
		Swordman,
		Magician
	}
	/// <summary>
	/// 目前默认为剑士
	/// </summary>
	public Playertype playertype = Playertype.Swordman;
	/// <summary>
	/// 玩家最大HP值
	/// </summary>
	public int  maxHp = 1000;
	/// <summary>
	/// 玩家最大MP值
	/// </summary>
	public int maxMp = 500;
	public int maxExp = 100;
	public int attack = 100;
	public int def = 0;
	public int speed = 100;
	public int point = 0;
	public int rank = 1;
	void Awake()
	{
		_instance = this;
	}

}
