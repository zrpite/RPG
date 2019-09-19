using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一些全局的状态机和标志位
/// </summary>
public static class Global 
{
	/// <summary>
	/// player的状态(一般,界面交互,死亡)
	/// </summary>
	public enum State
	{
		NormalState,
		TipState,
		DieState
	}
	public static State playerState = State.NormalState;

	/// <summary>
	/// C#属性,依据player的状态决定是否可控(界面交互,死亡,不可控)
	/// </summary>
	/// <returns>可移动返回true,else false</returns>
	public static bool CanControl
	{
		get
		{
			if(playerState == State.TipState || playerState == State.DieState)
			{
				canControl = false;
			}
			else
			{
				canControl = true;
			}
			return canControl;
		}
		set
		{
			canControl = value;
		}
	}
	/// <summary>
	/// 镜头旋转是否带动player旋转标志(不用)
	/// </summary>
	public static bool needTurn = false;
	private static bool canControl = true;	
	/// <summary>
	/// 是否换上新武器标志位
	/// </summary>
	public static bool changeWeapon = false;
}
