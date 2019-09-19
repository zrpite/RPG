using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能信息类
/// </summary>
public class SkillItem : MonoBehaviour 
{	
	/// <summary>
	/// 技能cd状态
	/// </summary>
	public enum SkillCDState
	{
		Ready,
		Cold
	}
	/// <summary>
	/// 技能cd状态对象(默认Ready)
	/// </summary>
	public SkillCDState skillCdState = SkillCDState.Ready;
	public int id = 0;
	public  SkillInfo info;
	public UISprite icon;
	public UILabel infoText;
	public SkillDragController skillDragController;
	/// <summary>
	/// 技能CD
	/// </summary>
	public float maxCD;
	/// <summary>
	/// 倒数CD
	/// </summary>
	public float currentCD;

	void Update()
	{
		if(skillCdState == SkillCDState.Cold)
		{
			currentCD -= Time.deltaTime;
			if(currentCD <= 0)
			{
				currentCD = maxCD;
				skillCdState = SkillCDState.Ready;
			}
		}
	}
	/// <summary>
	/// 设置当前技能信息
	/// </summary>
	/// <param name="id"></param>
	public void SetThisSkillById(int id)
	{
		this.id = id;
		info = SkillsInfo.Instance().GetSkillInfoById(id);
		maxCD = info.cd;
		currentCD = maxCD;
		icon.spriteName = info.icon_name;
		skillDragController.id = id;
		string message = "";
		switch(info.skillType)
		{
			case SkillInfo.SkillType.Cure :	 	   message = GetCureMessage(); 		  break;
			case SkillInfo.SkillType.Buff : 	   message = GetBuffMessage(); 		  break; 
			case SkillInfo.SkillType.MultiTarget : message = GetMultiAttackMessage(); break;
		}
		infoText.text = message;
	}
	/// <summary>
	/// 获取Cure类技能描述
	/// </summary>
	/// <returns></returns>
	public string GetCureMessage()
	{
		string str = "";
		str += "名称:  " + info.name + "\n";
		str += "效果:  " + info.description + "\n";
		str += "CD:  " + info.cd + "s   " + "魔法消耗:  " + info.mpSpend + "\n";
		str += "职业:  ";
		switch(info.qualifyRole)
		{
			case PlayerStatusInfo.Playertype.Swordman : str += "剑士   ";   break;
			case PlayerStatusInfo.Playertype.Magician : str += "魔法师   "; break;
		}
		str += "解锁:  " + info.unlockRank + "级";
		return str;
	}
	/// <summary>
	/// 获取Buff类技能描述
	/// </summary>
	/// <returns></returns>
	public string GetBuffMessage()
	{
		string str = "";
		str += "名称:  " + info.name + "\n";
		str += "效果:  " + info.description + "   持续时间:  " + info.timeDuration + "s\n";
		str += "CD:  " + info.cd + "s   " + "魔法消耗:  " + info.mpSpend + "\n";
		str += "职业:  ";
		switch(info.qualifyRole)
		{
			case PlayerStatusInfo.Playertype.Swordman : str += "剑士   ";   break;
			case PlayerStatusInfo.Playertype.Magician : str += "魔法师   "; break;
		}
		str += "解锁:  " + info.unlockRank + "级";
		return str;
	}
	/// <summary>
	/// 获取MultiAttack类技能描述
	/// </summary>
	/// <returns></returns>
	public string GetMultiAttackMessage()
	{
		string str = "";
		str += "名称:  " + info.name + "\n";
		str += "效果:  " + info.description + "   范围:  多\n";
		str += "CD:  " + info.cd + "s   " + "魔法消耗:  " + info.mpSpend + "\n";
		str += "职业:  ";
		switch(info.qualifyRole)
		{
			case PlayerStatusInfo.Playertype.Swordman : str += "剑士   ";   break;
			case PlayerStatusInfo.Playertype.Magician : str += "魔法师   "; break;
		}
		str += "解锁:  " + info.unlockRank + "级";
		return str;
	}
	/// <summary>
	/// 设置当前技能为冷却状态
	/// </summary>
	public void SetSkillColdState()
	{
		skillCdState = SkillCDState.Cold;
	}
	/// <summary>
	/// 获取当前技能是否Ready状态
	/// </summary>
	/// <returns>Ready返回true</returns>
	public bool GetSkillReadyState()
	{
		return (skillCdState == SkillCDState.Ready ? true : false);
	}
}
