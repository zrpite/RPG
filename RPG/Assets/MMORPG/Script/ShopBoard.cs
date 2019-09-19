using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 商店系统
/// </summary>
public class ShopBoard : TweenPosCtrl 
{
	private static ShopBoard _instance = null;
	public static ShopBoard Instance()
	{
		return _instance;
	}

	/// <summary>
	/// 商店物品prefab
	/// </summary>
	public ShopItem shopItem;
	public GameObject uigrid;
	private Vector3 thispos = new Vector3(43, 48, 0);
	void Awake()
	{
		_instance = this;
        base.Init();
		thispos = new Vector3(43, 48, 0);
	}

	/// <summary>
	/// 向商店中添加一个物品
	/// </summary>
	/// <param name="id">物品id</param>
	public void AddOneShopItemById(int id)
	{
		shopItem.SetThisShopItemById(id);
		GameObject thisItem = NGUITools.AddChild(uigrid,shopItem.gameObject);
		thisItem.transform.localPosition = thispos;
		thispos += new Vector3(0, -119, 0);
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
	/// <summary>
	/// 弹出商店面板
	/// </summary>
	public void Show()
	{
		Global.playerState = Global.State.TipState;
		tweenPos.PlayForward();
	}
}
