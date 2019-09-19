using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一些游戏测试
/// </summary>
public class TestGame : MonoBehaviour 
{
	int c = 1001,z = 2001;
	void Start ()
	{
		c = 1001;
		z = 2001;
	}
	void Update ()
	{
#if UnityEditor
        //背包加药品
        if(Input.GetKeyDown(KeyCode.P))
		{
			int x = 0;
			BagBoard.Instance().PickOneItemById(x = Random.Range(1001,1004));
			Debug.Log(x);
		}
		//背包加装备
		if(Input.GetKeyDown(KeyCode.O))
		{
			int x = 0;
			BagBoard.Instance().PickOneItemById(x = Random.Range(2001,2023));
			Debug.Log(x);
		}
		//商店加药品
		if(Input.GetKeyDown(KeyCode.L))
		{
			ShopBoard.Instance().AddOneShopItemById(c++);
			Debug.Log(c);
		}
		//商店加装备
		if(Input.GetKeyDown(KeyCode.K))
		{
			ShopBoard.Instance().AddOneShopItemById(z++);
			Debug.Log(z);
		}
		//扣血
		if(Input.GetKeyDown(KeyCode.M))
		{
			PlayerStatusManager.Instance().ReduceHp(50);
		}
		//加钱
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			CoinManager.Instance().AddCoin(50);
		}
		//扣钱
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			CoinManager.Instance().ReduceCoin(50);
		}
		//加点
		if(Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			PlayerStatusManager.Instance().AddPoint();
		}
		//加技能
		if(Input.GetKeyDown(KeyCode.B))
		{
			SkillBoard.Instance().AddOneSkillById(4001);
			SkillBoard.Instance().AddOneSkillById(4002);
			SkillBoard.Instance().AddOneSkillById(4003);
		}
#endif
	}
}
