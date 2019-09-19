using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 升级面板
/// </summary>
public class UpgradeBoard : MonoBehaviour 
{
	private static UpgradeBoard _instance = null;
	public static UpgradeBoard Instance()
	{
		return _instance;
	}
	private TweenScale tweenScale;
	void Awake()
	{
		_instance = this;
	}
	void Start () 
	{
		tweenScale = this.GetComponent<TweenScale>();
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
		tweenScale.PlayReverse();
	}
	/// <summary>
	/// 弹出升级信息面板
	/// </summary>
	public void Show()
	{
		this.gameObject.SetActive(true);
		StartCoroutine(WaitThenCloseAuto(3));
	}
	/// <summary>
	/// 自动关闭面板协程
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	IEnumerator WaitThenCloseAuto(float t)
	{
		yield return new WaitForSeconds(t);
		Hide();
	}
}
