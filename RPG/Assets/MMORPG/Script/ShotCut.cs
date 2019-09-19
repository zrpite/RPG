using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 快捷技能信息
/// </summary>
public class ShotCut : MonoBehaviour {
	public int id = 0;
	public UISprite icon;
	
	public void SetIconAndId(int id, string name)
	{
		this.id = id;
		this.icon.spriteName = name;
	}
}
