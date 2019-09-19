using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 快捷栏管理
/// </summary>
public class ShotCutGrid : MonoBehaviour 
{
	public int id = 0;
	public SkillInfo info;
	/// <summary>
	/// 向此快捷栏中添加技能
	/// </summary>
	/// <param name="id">技能id</param>
	public void SetThisGridShotCutById(int id)
	{
		this.id = id;
		this.info = SkillsInfo.Instance().GetSkillInfoById(id);
		ShotCut shotCut = this.GetComponentInChildren<ShotCut>();
		shotCut.SetIconAndId(id, info.icon_name);
		shotCut.transform.localPosition = Vector3.zero;
	}
	/// <summary>
	/// 从此快捷栏中移除技能
	/// </summary>
	public void UnloadThisGridShotCut()
	{
		this.id = 0;
		this.info = null;
		DestroyImmediate(this.GetComponentInChildren<ShotCut>().transform.gameObject);
	}
	/// <summary>
	/// 使用此快捷栏中的技能
	/// </summary>
	/// <returns>使用成功返回 true</returns>
	public bool UseSkill()
	{
		//没有技能的情况
		if(this.id == 0)
		{
			Debug.Log("没有快捷技能");
			return false;
		}
		//cd没好的情况
		else if(!SkillBoard.Instance().FindSkillById(id).GetSkillReadyState())
		{
			Debug.Log("CD未冷却");
			return false;
		}
		//mp不够的情况
		else if(!PlayerStatusManager.Instance().ReduceMp(this.info.mpSpend))
		{
			Debug.Log("蓝不够");
			return false;
		}
		//没有以上情况则释放技能
		else
		{
			//使技能进入冷却
			SkillBoard.Instance().FindSkillById(id).SetSkillColdState();
			//技能发射器发射技能
			SkillShoot.Instance().ShootSkill(this.id,this.info);
			return true;
		}
	}
	
}
