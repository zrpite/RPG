using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// player攻击控制类
/// </summary>
public class AttackController : AnimatorMotor
{
	private static AttackController _instance = null;
	public static AttackController Instance()
	{
		return _instance;
	}
	/// <summary>
	/// player攻击状态机
	/// </summary>
	public enum AttackState
	{
		NONE,
		FIRST,
		SECOND,
		THIRD1,
		THIRD2,
		THIRD3,
		THIRD4
	}
	/// <summary>
	///  刀光拖尾
	/// </summary>
	public WeaponTrail trail;
	/// <summary>
	/// 攻击触发盒
	/// </summary>
	public BoxCollider atkCollider;
	public  AttackState atkState = AttackState.NONE;
	/// <summary>
	/// 第一段攻击的标志位,避免循环在第一段攻击
	/// </summary>
	public bool isFirstAttack = true;
	//不同段攻击的攻击力数值
	public static float firstAtkPowerPercent = 0.6f;
	public static float secondAtkPowerPercent = 0.5f;
	public static float thirdAtkPower1Percent = 0.7f;
	public static float thirdAtkPower2Percent = 0.8f;
	public static float thirdAtkPower3Percent = 0.9f;
	public static float thirdAtkPower4Percent = 1.0f;
	void Awake()
	{
		_instance = this;
	}
	void Start() 
	{
		anim = this.GetComponent<Animator>();
		atkCollider.enabled = false;
		atkState = AttackState.NONE;
	}
	void FixedUpdate()
	{
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		//只有在攻击状态中才展示刀光拖尾特效
		if(currentBaseState.fullPathHash == atk1State
		|| currentBaseState.fullPathHash == atk2State
		|| currentBaseState.fullPathHash == atk3State)
		{
			trail.Itterate(Time.time);
			trail.UpdateTrail(Time.time,Time.fixedDeltaTime);
		}
		else{
			trail.Itterate(0);
			trail.UpdateTrail(Time.time,Time.fixedDeltaTime);
		}
		//可攻击控制
		if(GetFire(0) && CanAttack())
		{
			if(isFirstAttack)
			{
				anim.SetBool("Attack1",true);
				isFirstAttack = false;
			}
		}
		//一段攻击内容和切换条件		
		if(currentBaseState.fullPathHash == atk1State)
		{
			if(currentBaseState.normalizedTime >= 0.4f)
			{
				if(GetFire(0))
				{
					anim.SetBool("Attack2",true);
					anim.SetBool("Attack1",false);
				}
			}
			if(currentBaseState.normalizedTime >= 0.9f)
			{
				ResetState();
			}
		}
		//二段攻击内容和切换条件	
		if(currentBaseState.fullPathHash == atk2State)
		{
			if(currentBaseState.normalizedTime >= 0.5f)
			{
				if(GetFire(0))
				{
					anim.SetBool("Attack3",true);
					anim.SetBool("Attack2",false);
				}
				
			}
			if(currentBaseState.normalizedTime >= 0.9f)
			{
				ResetState();
			}
		}
		//三段攻击内容和切换条件	
		if(currentBaseState.fullPathHash == atk3State)
		{
			if(currentBaseState.normalizedTime >= 0.9f)
			{
				ResetState();
			}
			
		}
		
	}
	/// <summary>
	/// 判断当前player的状态是否可以进行攻击
	/// </summary>
	public bool CanAttack()
	{
		if(currentBaseState.fullPathHash != jumpState 
		&& currentBaseState.fullPathHash != idle_aState 
		&& currentBaseState.fullPathHash != backState 
		&& currentBaseState.fullPathHash != rotateState
		&& Global.CanControl)
		{
			return true;
		}
		else return false;
	}
	/// <summary>
	/// 鼠标事件
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	private bool GetFire(int index)
	{
		return Input.GetMouseButtonDown(index);
	}
	/// <summary>
	/// 重设动画状态和相关参数到未攻击状态
	/// </summary>
	private void ResetState()
	{
		anim.SetBool("Attack1",false);
		anim.SetBool("Attack2",false);
		anim.SetBool("Attack3",false);
		isFirstAttack = true;
		Debug.Log("回去");
	}

	//以下三段为攻击效果执行，在unity中player的attack动画时间轴中某点触发以下各Event
	public void FirstAttack()
	{
		Debug.Log("一段攻击");
		atkState = AttackState.FIRST;
		StartCoroutine(OpenAtkTrigger(0.1f));
		
	}
	public void SecondAttack()
	{
		Debug.Log("二段攻击");
		atkState = AttackState.SECOND;
		StartCoroutine(OpenAtkTrigger(0.1f));
	}
	public void ThirdAttack()
	{
		Debug.Log("三段攻击");
		atkState = AttackState.THIRD1;
		StartCoroutine(OpenAtkTrigger(0.1f));
	}
	public void FourthAttack()
	{
		Debug.Log("四段攻击");
		atkState = AttackState.THIRD2;
		StartCoroutine(OpenAtkTrigger(0.1f));	
	}
	public void FifthAttack()
	{
		Debug.Log("五段攻击");
		atkState = AttackState.THIRD3;
		StartCoroutine(OpenAtkTrigger(0.1f));
	}
		public void SixthAttack()
	{
		Debug.Log("六段攻击");
		atkState = AttackState.THIRD4;
		StartCoroutine(OpenAtkTrigger(0.1f));
	}
	/// <summary>
	/// 开启攻击触发盒的协程
	/// </summary>
	/// <param name="t">开启时间</param>
	/// <returns></returns>
	IEnumerator OpenAtkTrigger(float t)
	{
		atkCollider.enabled = true;
		yield return new WaitForSeconds(t);
		atkCollider.enabled = false;
	}
}
