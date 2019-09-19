using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 已释放的技能触发器
/// </summary>
public class SkillController : MonoBehaviour {
	/// <summary>
	/// 技能造成伤害值
	/// </summary>
	public int damageNum = 0;
	
	void OnTriggerEnter(Collider other)
	{
		//通知所有受到技能的敌人,BeDamage
		if(other.tag == "Enemy")
		{
			other.gameObject.SendMessage("BeDamage",damageNum,SendMessageOptions.DontRequireReceiver);
		}
	}
}
