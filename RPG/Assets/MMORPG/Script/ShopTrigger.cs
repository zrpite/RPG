using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// npc武器商店面板触发器
/// </summary>
public class ShopTrigger : MonoBehaviour {
void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			if(Input.GetKeyDown(KeyCode.Q))
			{
				BoardManager.Instance().SwitchShowBoard(BoardManager.BoardShow.SHOPBOARD);
			}
		}
	}
}
