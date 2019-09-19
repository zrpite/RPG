using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 人物属性系统
/// </summary>
public class StatusBoard : TweenPosCtrl
{
	private static StatusBoard _instance = null;
	public static StatusBoard Instance()
	{
		return _instance;
	}
	public UILabel hpNum,mpNum,atkNum,defNum,spNum,pointNum;
	/// <summary>
	/// 加点按钮对象
	/// </summary>
	public GameObject[] addPointButtons;
	void Awake()
	{
		_instance = this;
        base.Init();
	}
	void Start () 
	{
		UpdateStatusShow();
	}
	/// <summary>
	/// 更新一次面板信息显示
	/// </summary>
	public void UpdateStatusShow()
	{
		hpNum.text = PlayerStatusInfo.Instance().maxHp.ToString();
		mpNum.text = PlayerStatusInfo.Instance().maxMp.ToString();
		atkNum.text = PlayerStatusInfo.Instance().attack.ToString();
		defNum.text = PlayerStatusInfo.Instance().def.ToString();
		spNum.text = PlayerStatusInfo.Instance().speed.ToString();
		pointNum.text = PlayerStatusInfo.Instance().point.ToString();
		
		if(PlayerStatusInfo.Instance().point > 0)
		{
			foreach(GameObject eachButton in addPointButtons)
			{
				eachButton.SetActive(true);
			}
		}
		else
		{
			foreach(GameObject eachButton in addPointButtons)
			{
				eachButton.SetActive(false);
			}
		}
	}
	/// <summary>
	/// 加点按钮回调
	/// </summary>
	/// <param name="go">按钮对象</param>
	public void PointAddButtonClick(GameObject go)
	{
		PlayerStatusManager.Instance().AddProperties(go.name);
		PlayerStatusManager.Instance().ReducePoint(1);
		UpdateStatusShow();
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
