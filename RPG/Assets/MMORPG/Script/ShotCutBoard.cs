using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 快捷技能系统
/// </summary>
public class ShotCutBoard : MonoBehaviour 
{
	private static ShotCutBoard _instance = null;
	public static ShotCutBoard Instance()
	{
		return _instance;
	}
	public List<ShotCutGrid> shotCutGrids = new List<ShotCutGrid>();
	
	public GameObject newSkill;	

	void Awake()
	{
		_instance = this;
	}
	/// <summary>
	/// 向特定快捷栏中添加一个快捷技能
	/// </summary>
	/// <param name="id">技能id</param>
	/// <param name="surface">快捷栏对象</param>
	public void SetOneGridShotSkillByIdAndName(int id, GameObject surface)
	{
		ShotCutGrid findGrid = null;
		if(surface.tag == "ShotCutGrid")
		{
			findGrid = surface.GetComponent<ShotCutGrid>();
		}
		else
		{
			findGrid = surface.GetComponentInParent<ShotCutGrid>();
		}
		Debug.Log(findGrid.name);
		//如果该快捷栏中已经有技能了(先移除技能)
		if(findGrid.id != 0)
		{
			findGrid.UnloadThisGridShotCut();
			Debug.Log("除");
		}
		Debug.Log("添");
		GameObject createNewSkill = NGUITools.AddChild(findGrid.gameObject, newSkill);
		createNewSkill.GetComponent<UISprite>().depth = 1;
		findGrid.SetThisGridShotCutById(id);
	}
	/// <summary>
	/// 使用一个快捷栏中的技能
	/// </summary>
	/// <param name="index">快捷栏编号(+1的)</param>
	public void UseOneGridSkill(int index)
	{
		if(shotCutGrids[index - 1].UseSkill())
		{
			Debug.Log("施放了");
		}
		else
		{
			Debug.Log("没施放");
		}
	}
}
