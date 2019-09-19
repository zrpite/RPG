using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能信息管理
/// </summary>
public class SkillsInfo : MonoBehaviour 
{
	private static SkillsInfo _instance = null;
	public static SkillsInfo Instance()
	{
		return _instance;
	}
	/// <summary>
	/// 存储从文本文件中读取的信息的字典
	/// </summary>
	/// <typeparam name="int">id</typeparam>
	/// <typeparam name="SkillInfo">SkillInfo对象</typeparam>
	/// <returns></returns>
	private Dictionary<int, SkillInfo> skillInfoDict = new Dictionary<int, SkillInfo>();
    /// <summary>
    /// 存储所有技能信息的文本文件
    /// </summary>
    public TextAsset skillsInfoTxt;
	void Awake()
	{
		_instance = this;
		ReadInfo();
	}
	/// <summary>
    /// 从文本文件中读取信息到字典中
    /// </summary>
	void ReadInfo()
	{
		string text = skillsInfoTxt.text;
		string[] lineArray = text.Split('\n');
		foreach(string line in lineArray)
		{
			string[] perlineArray = line.Split(',');
			SkillInfo skillinfo = new SkillInfo();
			int tempid = int.Parse(perlineArray[0]);
			string tempname = perlineArray[1];
			string tempicon_name = perlineArray[2];
			string tempdescription = perlineArray[3];
			SkillInfo.SkillType temptypeskill = SkillInfo.SkillType.Buff;
			switch(perlineArray[4])
			{
				case "Cure" :		  temptypeskill = SkillInfo.SkillType.Cure; 		break;
				case "Buff" :		  temptypeskill = SkillInfo.SkillType.Buff; 		break;
				case "SingleTarget" : temptypeskill = SkillInfo.SkillType.SingleTarget; break;
				case "MultiTarget" :  temptypeskill = SkillInfo.SkillType.MultiTarget;  break;
			}
			SkillInfo.SkillEffectType tempeffecttypeskill = SkillInfo.SkillEffectType.HP;
			switch(perlineArray[5])
			{
				case "HP" :		tempeffecttypeskill = SkillInfo.SkillEffectType.HP;		break;
				case "MP" :		tempeffecttypeskill = SkillInfo.SkillEffectType.MP;     break;
				case "ATTACK" : tempeffecttypeskill = SkillInfo.SkillEffectType.ATTACK; break;
				case "DEF" :    tempeffecttypeskill = SkillInfo.SkillEffectType.DEF;    break;
				case "SPEED" :  tempeffecttypeskill = SkillInfo.SkillEffectType.SPEED;  break;
			}
			int tempamount = int.Parse(perlineArray[6]);
			int temptimeduration = int.Parse(perlineArray[7]);
			int tempmpspend = int.Parse(perlineArray[8]);
			int tempcd = int.Parse(perlineArray[9]);
			PlayerStatusInfo.Playertype tempqualifyrole = PlayerStatusInfo.Playertype.Swordman;
			switch(perlineArray[10])
			{
				case "Swordman" : tempqualifyrole = PlayerStatusInfo.Playertype.Swordman; break;
				case "Magician" : tempqualifyrole = PlayerStatusInfo.Playertype.Magician; break;
			}
			int tempunlockrank = int.Parse(perlineArray[11]);
			skillinfo.id = tempid;
			skillinfo.name = tempname;
			skillinfo.icon_name = tempicon_name;
			skillinfo.description = tempdescription;
			skillinfo.skillType = temptypeskill;
			skillinfo.skillEffectType = tempeffecttypeskill;
			skillinfo.amount = tempamount;
			skillinfo.timeDuration = temptimeduration;
			skillinfo.mpSpend = tempmpspend;
			skillinfo.cd = tempcd;
			skillinfo.qualifyRole = tempqualifyrole;
			skillinfo.unlockRank = tempunlockrank;

			skillInfoDict.Add(tempid, skillinfo);
		}
	}
	/// <summary>
	/// 从字典中获取id对应的SkillInfo对象
	/// </summary>
	/// <param name="id">技能id</param>
	/// <returns>返回id对应的SkillInfo对象</returns>
	public SkillInfo GetSkillInfoById(int id)
	{
		SkillInfo info = null;
		skillInfoDict.TryGetValue(id, out info);
		return info;
	}
}
