using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// y高度为0的player坐标(便于僵尸追踪)
/// </summary>
public class FollowPos : MonoBehaviour 
{
	/// <summary>
	/// 玩家transform
	/// </summary>
	public Transform p_transform;
   
	void Update () 
	{
		Vector3 pos = new Vector3(p_transform.position.x,0,p_transform.position.z);
		transform.position = pos;
	}
}
