using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 所有敌人公共继承类
/// </summary>
public class EnemyCommon : MonoBehaviour 
{
	private ZomebieControl zombie;
	/// <summary>
	/// 僵尸状态机
	/// </summary>
	public enum CurrentState
	{
		Parse,
		Follow,
		Attack,
		Die
	}
	/// <summary>
	/// 被伤害的数值显示prefab
	/// </summary>
	public GameObject damageObj;
	/// <summary>
	/// 被伤害的数值text
	/// </summary>
	public TextMesh damageText;
	/// <summary>
	/// 被打击的特效
	/// </summary>
	public GameObject beAttackFxOld;
	public GameObject beAttackFxNew;
	/// <summary>
	/// 特效位置
	/// </summary>
	public Transform effectPos;

	void Start () 
	{
		zombie = this.GetComponent<ZomebieControl>();
	}
	//被攻击逻辑
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Weapon")
		{
			Debug.Log("被攻击");
			switch(AttackController.Instance().atkState)
			{
				case AttackController.AttackState.FIRST:  BeDamage(PlayerStatusInfo.Instance().attack * AttackController.firstAtkPowerPercent);  break;
				case AttackController.AttackState.SECOND: BeDamage(PlayerStatusInfo.Instance().attack * AttackController.secondAtkPowerPercent); break;
				case AttackController.AttackState.THIRD1: BeDamage(PlayerStatusInfo.Instance().attack * AttackController.thirdAtkPower1Percent); break;
				case AttackController.AttackState.THIRD2: BeDamage(PlayerStatusInfo.Instance().attack * AttackController.thirdAtkPower2Percent); break;
				case AttackController.AttackState.THIRD3: BeDamage(PlayerStatusInfo.Instance().attack * AttackController.thirdAtkPower3Percent); break;
				case AttackController.AttackState.THIRD4: BeDamage(PlayerStatusInfo.Instance().attack * AttackController.thirdAtkPower4Percent); break;
			}
		}		
	}
	/// <summary>
	/// 根据受到攻击类型的不同决定受到的伤害数值
	/// </summary>
	/// <param name="index"></param>
	public void BeDamage(float num)
	{
		zombie.life -= (int)num; 
		ShowDamage((int)num);
		//一般情况下受到伤害才能造成死亡
		if(zombie.life <= 0)
		{
			zombie.zombieState = CurrentState.Die;
		}
	}
	/// <summary>
	/// 显示被伤害数值
	/// </summary>
	/// <param name="num"></param>
	public void ShowDamage(int num)
	{
		damageText.text = "-" + num.ToString();
		GameObject obj = Instantiate(damageObj,damageObj.transform.position,damageObj.transform.rotation) as GameObject;
		obj.transform.parent = this.transform;
		GameObject effect = null;
		if(Global.changeWeapon)
		{
			effect = Instantiate(beAttackFxNew,effectPos.position,beAttackFxNew.transform.rotation) as GameObject;
		}
		else{
			effect = Instantiate(beAttackFxOld,effectPos.position,beAttackFxOld.transform.rotation) as GameObject;
		}
	
		Destroy(effect,0.5f);
	
	}
}
