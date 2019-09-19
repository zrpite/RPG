using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能拖动控制
/// </summary>
public class SkillDragController : UIDragDropItem 
{
	/// <summary>
	/// 存放clone的拖动技能的GameObject(新的Panel,保证能技能能拖出去)
	/// </summary>
	public GameObject skillDragging;
	public int id = 0;
	private Vector3 regionPos = new Vector3(-142, 0, 0);
	void Start()
	{
		skillDragging = GameObject.Find("SkillDragging");
	}
	protected override void OnDragDropStart()
	{
		base.OnDragDropStart();

		this.transform.parent = skillDragging.transform;
		this.GetComponent<UISprite>().depth = 2;
	}
	protected override void OnDragDropRelease(GameObject surface)
	{
		base.OnDragDropRelease(surface);

		if(surface.tag == "ShotCutGrid" || surface.tag == "ShotCut")
		{
			ShotCutBoard.Instance().SetOneGridShotSkillByIdAndName(id, surface);
		}
		else
		{
			ResetPos();
		}
	}
	/// <summary>
	/// 拖放失败重设位置
	/// </summary>
	private void ResetPos()
	{
		this.transform.localPosition = regionPos;
	}
}
