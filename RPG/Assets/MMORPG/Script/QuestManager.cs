using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 任务状态、进度管理
/// </summary>
public class QuestManager : MonoBehaviour 
{
	/// <summary>
	/// 任务的进行状态
	/// </summary>
	public enum QuestState
	{
		NONE,
		ACCEPT,
		FINISH,
		GETREWARD
	}
	/// <summary>
	/// 更新僵尸id对应进度数的Label
	/// </summary>
	public UILabel[] quest_progress_num;
	/// <summary>
	/// 目前任务进度数
	/// </summary>
	public int nowProgressNum = 0;
	public QuestState questState = QuestState.NONE; 

	private static QuestManager _instance = null;
	public static QuestManager Instance()
	{
		return _instance;
	}
	void Awake()
	{
		_instance = this;
	}
	/// <summary>
	/// 根据僵尸的id来更新任务完成的进度
	/// </summary>
	/// <param name="index">僵尸的id</param>
	public void QuestStateUpdateByType(int index)
	{
		nowProgressNum = int.Parse(quest_progress_num[index].text);
		++nowProgressNum;
		nowProgressNum = Mathf.Clamp(nowProgressNum, 0, 1);
		quest_progress_num[index].text = nowProgressNum.ToString();
		if(nowProgressNum == 1)
		{
			QuestManager.Instance().questState = QuestManager.QuestState.FINISH;
		}
	}
}
