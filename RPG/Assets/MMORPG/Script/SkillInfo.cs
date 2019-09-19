using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单个技能信息类
/// </summary>
public class SkillInfo : MonoBehaviour 
{
	/// <summary>
	/// 技能类型(治愈,加成,单体攻击,群攻)
	/// </summary>
	public enum SkillType
	{
		Cure,
		Buff,
		SingleTarget,
		MultiTarget
	}
	/// <summary>
	/// 技能作用效果类型(加血,加蓝,加攻,加防,加速)
	/// </summary>
	public enum SkillEffectType
	{
		HP,
		MP,
		ATTACK,
		DEF,
		SPEED
	}
	/// <summary>
	/// 技能id
	/// </summary>
	public int id;
	/// <summary>
	/// 技能名
	/// </summary>
	public string name;
	/// <summary>
	/// 技能对应图标名
	/// </summary>
	public string icon_name;
	/// <summary>
	/// 技能描述
	/// </summary>
	public string description;
	/// <summary>
	/// 技能类型(治愈,加成,单体攻击,群攻)
	/// </summary>
	public SkillType skillType;
	/// <summary>
	/// 技能作用效果类型(加血,加蓝,加攻,加防,加速)
	/// </summary>
	public SkillEffectType skillEffectType;
	/// <summary>
	/// 技能加成或攻击数值
	/// </summary>
	public int amount;
	/// <summary>
	/// 技能持续时间
	/// </summary>
	public int timeDuration;
	/// <summary>
	/// 技能耗蓝量
	/// </summary>
	public int mpSpend;
	/// <summary>
	/// 技能CD
	/// </summary>
	public int cd;
	/// <summary>
	/// 技能限定的角色类型
	/// </summary>
	public PlayerStatusInfo.Playertype qualifyRole;
	/// <summary>
	/// 技能解锁等级
	/// </summary>
	public int unlockRank;
}
