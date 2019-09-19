using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// npc任务面板触发器
/// </summary>
public class NPCQuestTrigger : MonoBehaviour 
{

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			if(Input.GetKeyDown(KeyCode.Q))
			{
				BoardManager.Instance().SwitchShowBoard(BoardManager.BoardShow.QUESTBOARD_FOX);
			}
		}
	}
	
}
