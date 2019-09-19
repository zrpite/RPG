using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 伤害数值显示
/// </summary>
public class DamageShow : MonoBehaviour 
{

	public TweenPosition tweenPos;
	public TweenScale tweenScale;
	void Start () 
	{
		Destroy(this.gameObject,0.5f);
	}
}
