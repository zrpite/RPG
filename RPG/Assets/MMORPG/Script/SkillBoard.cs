using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能系统
/// </summary>
public class SkillBoard : TweenPosCtrl 
{
	private static SkillBoard _instance = null;
	public static SkillBoard Instance()
	{
		return _instance;
	}

	/// <summary>
	/// 技能的prefab
	/// </summary>
	public SkillItem skillItem;
	public GameObject uigrid;
	private Vector3 thispos = new Vector3(43, 48, 0);
	void Awake()
	{
		_instance = this;
        base.Init();
	}

	/// <summary>
	/// 升级解锁技能监听
	/// </summary>
	public void ListenToUnlockSkill()
	{
		switch(PlayerStatusInfo.Instance().rank)
		{
			case 2 : AddOneSkillById(4001); AddOneSkillById(4002); AddOneSkillById(4003); break;
		}
	}
	/// <summary>
	/// 添加技能
	/// </summary>
	/// <param name="id">技能id</param>
	public void AddOneSkillById(int id)
	{
		skillItem.SetThisSkillById(id);
		GameObject thisItem = NGUITools.AddChild(uigrid,skillItem.gameObject);
		thisItem.transform.localPosition = thispos;
		thispos += new Vector3(0, -119, 0);
	}
	/// <summary>
	/// 从技能列表中寻找施放的对应技能
	/// </summary>
	/// <param name="id"></param>
	public SkillItem FindSkillById(int id)
	{
		SkillItem[] tempSkillItems = this.GetComponentsInChildren<SkillItem>();
		foreach(SkillItem tempSkill in tempSkillItems)
		{
			if(tempSkill.id == id)
			{
				return tempSkill;
			}
		}

		Debug.Log("没找到对应技能");
		return null;
	}
	/// <summary>
	/// 关闭面板按钮回调
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
