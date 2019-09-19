using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 暂留技能cd控制器
/// </summary>
public class SkillCDController : MonoBehaviour {

	private static SkillCDController _instance = null;
	public static SkillCDController Instance()
	{
		return _instance;
	}
	void Awake()
	{
		_instance = this;
	}

	
}
