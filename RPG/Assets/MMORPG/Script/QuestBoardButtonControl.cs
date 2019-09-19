using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 任务面板的按钮功能控制类
/// </summary>
public class QuestBoardButtonControl : TweenPosCtrl 
{
	/// <summary>
	/// 任务相关的僵尸编号
	/// </summary>
	public int questZombieIndex = 0;
	/// <summary>
	/// 任务相关的金钱数
	/// </summary>
	public int rewards_coin_num = 1000;
	public int rewards_exp_num = 80;
	//以下为界面组成
	public GameObject quest_fox_content1;
	public GameObject quest_fox_content2;
	public GameObject finishLabel;
	public GameObject okButton;
	public GameObject againButton;
	public GameObject acceptButton;
	public GameObject cancelButton;
	
	private static QuestBoardButtonControl _instance = null;
	public static QuestBoardButtonControl Instance()
	{
		return _instance;
	}

	void Awake()
	{
		_instance = this;
        base.Init();
		questZombieIndex = 0;
	}
	/// <summary>
	/// Accept按钮单击回调
	/// </summary>
	public void AcceptButtonClick()
	{
		QuestManager.Instance().questState = QuestManager.QuestState.ACCEPT;
		if(QuestManager.Instance().questState == QuestManager.QuestState.ACCEPT)
		{
			ZombieSpawner.Instance().SpawnZombieByType(questZombieIndex);
			quest_fox_content1.SetActive(false);
			quest_fox_content2.SetActive(true);
			acceptButton.SetActive(false);
			cancelButton.SetActive(false);
			okButton.SetActive(true);
		}
	}
	/// <summary>
	/// cancle按钮单击回调
	/// </summary>
	public void CancelButtonClick()
	{
		BoardManager.Instance().SwitchShowBoard(BoardManager.BoardShow.NONE);
		Hide();
	}
	/// <summary>
	/// ok按钮单击回调
	/// </summary>
	public void OkButtonClick()
	{
		if(QuestManager.Instance().questState == QuestManager.QuestState.FINISH)
		{
			finishLabel.SetActive(true);
			CoinManager.Instance().AddCoin(rewards_coin_num);
			PlayerStatusManager.Instance().AddExp(rewards_exp_num);
			QuestManager.Instance().questState = QuestManager.QuestState.GETREWARD;
		}
		//BoardManager.Instance().SwitchShowBoard(BoardManager.BoardShow.NONE);
		Hide();
		if(QuestManager.Instance().questState == QuestManager.QuestState.GETREWARD)
		{
			againButton.SetActive(true);
		}
	}
	/// <summary>
	/// Again按钮单击回调
	/// </summary>
	public void AgainButtonClick()
	{
		ZombieSpawner.Instance().SpawnZombieByType(questZombieIndex);
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
