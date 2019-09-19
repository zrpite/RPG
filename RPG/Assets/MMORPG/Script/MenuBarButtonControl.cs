using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 菜单栏按钮回调
/// </summary>
public class MenuBarButtonControl : MonoBehaviour 
{
	/// <summary>
	/// 背包面板按钮回调
	/// </summary>
	public void BagButtonClick()
	{
		SetTipState();
		BoardManager.Instance().SwitchShowBoard(BoardManager.BoardShow.BAGBOARD);
	}
	/// <summary>
	/// 装备面板按钮回调
	/// </summary>
	public void EquipButtonClick()
	{
		SetTipState();
		BoardManager.Instance().SwitchShowBoard(BoardManager.BoardShow.EQUIPBOARD);
	}
	/// <summary>
	/// 技能面板按钮回调
	/// </summary>
	public void SkillButtonClick()
	{
		SetTipState();
		BoardManager.Instance().SwitchShowBoard(BoardManager.BoardShow.SKILLBOARD);
	}
	/// <summary>
	/// 信息面板按钮回调
	/// </summary>
	public void StatusButtonClick()
	{
		SetTipState();
		BoardManager.Instance().SwitchShowBoard(BoardManager.BoardShow.STATUSBOARD);
	}
	public void SettingButtonClick()
	{
		
	}

	public void SetTipState()
	{
		Global.playerState = Global.State.TipState;
	}
}
