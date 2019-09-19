using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 背包系统
/// </summary>
public class BagBoard : TweenPosCtrl 
{

	private static BagBoard _instance = null;
	public static BagBoard Instance()
	{
		return _instance;
	}
	public List<GridOfItem> gridList = new List<GridOfItem>();
	
	/// <summary>
	/// item的prefab
	/// </summary>
	public GameObject newItem;
	public GameObject page1, page2;
	void Awake()
	{
		_instance = this;
        base.Init();
	}
	void Start()
	{
		page1.SetActive(true);
		page2.SetActive(false);
	}
	/// <summary>
	/// 当捡取物品时,根据所捡物品id将其添加入背包中
	/// </summary>
	/// <param name="id">物品id</param>
	/// <param name="num">物品数量</param>
	public void PickOneItemById(int id, int num = 1)
	{
		//存储捡到的物品的id对应所在的Grid对象(分有无情况)
		GridOfItem findGrid = null;
		//遍历所有Grid判断是否已经存在该物品
		foreach(GridOfItem tempGrid in gridList)
		{
			if(tempGrid.id == id)
			{
				findGrid = tempGrid; break;
			}
		}
		//如果已经存在(物品数量 + num)
		if(findGrid != null)
		{
			findGrid.AddItemNum(num);
		}
		else//如果不存在(查找空的[id为0]Grid,再创建一个新的item放进Grid[设为子物体])
		{
			//寻找id为0的Grid(空Grid)
			foreach(GridOfItem tempGrid in gridList)
			{
				if(tempGrid.id == 0)
				{
					findGrid = tempGrid; break;
				}
			}
			//实例化一个item设为空Grid子物体
			if(findGrid != null)
			{
				GameObject createNewItem = NGUITools.AddChild(findGrid.gameObject,newItem);
				createNewItem.transform.localPosition = Vector3.zero;
				createNewItem.GetComponent<UISprite>().depth = 2;
				findGrid.SetThisGridItemById(id,num);
			}
		}
	}
	/// <summary>
	/// 关闭按钮回调
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
	/// <summary>
	/// 切换背包页面回调
	/// </summary>
	public void SwitchPageButtonClick(GameObject go)
	{
		if(go.name == "Left")
		{
			page1.SetActive(true);
			page2.SetActive(false);
		}
		if(go.name == "Right")
		{
			page1.SetActive(false);
			page2.SetActive(true);
		}
	}
}

