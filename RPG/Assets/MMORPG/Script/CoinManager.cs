using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 金币管理器
/// </summary>
public class CoinManager : MonoBehaviour 
{
	/// <summary>
	/// 所有金币数量
	/// </summary>
	public int allCoinNum = 0;
	/// <summary>
	/// 所有显示金币的UILabel
	/// </summary>
	public UILabel[] coinNumText;

	private static CoinManager _instacne = null;
	public static CoinManager Instance()
	{
		return _instacne;
	}
	void Awake()
	{
		_instacne = this;
	}
	/// <summary>
	/// 增加金币
	/// </summary>
	/// <param name="addNum">增加数量</param>
	public void AddCoin(int addNum)
	{
		allCoinNum += addNum;
		UpdateAllCoinText();
	}
	/// <summary>
	/// 消耗金币
	/// </summary>
	/// <param name="reduceNum">减少数量</param>
	public bool ReduceCoin(int reduceNum)
	{
		if(allCoinNum >= reduceNum)
		{
			//allCoinNum -= reduceNum;
			allCoinNum = Mathf.Clamp(allCoinNum -= reduceNum, 0, 999999999);
			UpdateAllCoinText();
			return true;
		}
		else
		{
			Debug.Log("钱不够");
			return false;
		}	
	}
	/// <summary>
	/// 更新所有金币显示
	/// </summary>
	public void UpdateAllCoinText()
	{
		foreach(UILabel temp in coinNumText)
		{
			temp.text = allCoinNum.ToString();
			Debug.Log(temp.text);
		}
	}
}
