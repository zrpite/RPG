using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 不用
/// </summary>
public class UpdatePlayerMove : AnimatorMotor
{

    /// <summary>
    /// 奔跑类型(直跑还是转弯跑)
    /// </summary>
	public enum RunType
	{
		STRAIGHT,
		TURN
	}
	public RunType runtype = RunType.STRAIGHT;
	/// <summary>
	/// 慢走速度
	/// </summary>
	public float walkSpeed = 2.0f;
	/// <summary>
	/// 奔跑速度
	/// </summary>
	public float runSpeed = 4.0f;
	/// <summary>
	/// 跳跃力
	/// </summary>
	public float jumpPower = 8.0f;
	/// <summary>
	/// 动画播放速度
	/// </summary>
	public float animSpeed = 1.3f;
	public CharacterController cc;
	public bool canJump = true;
	public Vector3 direct = Vector3.zero;

	void Awake()
	{
		walkSpeed = 2.0f;
		runSpeed = 4.0f;
		animSpeed = 1.3f;
	}
	void Start()
	{
		anim = this.GetComponent<Animator>();
		RunType runtype = RunType.STRAIGHT;
		//rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate()
	{
		//水平、竖直方向的输入轴值(有平滑过渡的(-1~1))
		float ver = Input.GetAxis("Vertical");
		float hor = Input.GetAxis("Horizontal");
		float mix = Mathf.Max(ver,hor);
		anim.speed = animSpeed;
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		//基于可控制的先决条件
		if(Global.CanControl)
		{
			if(ver > 0.05f || ver < -0.05f || hor > 0.05f || hor < -0.05f)
			{
				anim.SetBool("Run",true);
			}
			else{
				anim.SetBool("Run",false);
			}
				
			//跳跃控制
			if(Input.GetButtonDown("Jump") && canJump)
			{
				anim.SetBool("Jump",true);
				direct = Vector3.up * jumpPower;
			}
		}
		else
		{
			anim.SetFloat("Speed",0);
		}
			if(currentBaseState.fullPathHash == jumpState)
			{
				//Debug.Log("跳跃状态");
				if(!anim.IsInTransition(0))
				{
					canJump = false;
					if(direct.magnitude >= Vector3.zero.magnitude)
					{
						direct -= Vector3.up * Time.fixedDeltaTime * 14;
					}
					else
					{
						direct = Vector3.up;
					}
					if(direct.magnitude >= Vector3.zero.magnitude)
					{
						cc.Move(direct * Time.fixedDeltaTime );
					}
					if(currentBaseState.normalizedTime >= 0.6)
					{
						Debug.Log("跳跃结束");
						anim.SetBool("Jump",false);
						canJump = true;
					}
					Vector3 dir = transform.forward * ver * Time.fixedDeltaTime * runSpeed;
					cc.Move(dir);
				}
			}
			
			if(currentBaseState.fullPathHash == locoState)
			{
				//Debug.Log("行走状态");
				if(Global.CanControl)
				{
					DetermineRunType(ver, hor);
					switch(runtype)
					{
						case RunType.STRAIGHT : RunStraight(ver,hor); break;
						case RunType.TURN : RunTurn(ver,hor); break; 
					}
					
				}
				
			}
			
	}
	public void DetermineRunType(float v, float h)
	{
		bool vflag = false;
		bool hflag = false;
		if(v > 0.05f || v < -0.05f)
		{
			vflag = true;
		}
		else
		{
			vflag = false;
		}

		if(h > 0.05f || h < -0.05f)
		{
			hflag = true;
		}
		else
		{
			hflag = false;
		}
		if((vflag && !hflag) || (!vflag && hflag))
		{
			runtype = RunType.STRAIGHT;
		}
		if(vflag && hflag)
		{
			runtype = RunType.TURN;
		}
	}
	public void RunStraight(float v, float h)
	{
		float temp = 0;
		if(v > 0.05f)
		{
			transform.localEulerAngles = DirectionDefine.Instance().goForward;
			temp = v;
		}
		if(v < -0.05f)
		{
			transform.localEulerAngles = DirectionDefine.Instance().goBehind;
		
			temp = -v;
		}
		if(h > 0.05f)
		{
			transform.localEulerAngles = DirectionDefine.Instance().goRight;
			
			temp = h;
		}
		if(h < -0.05f)
		{
			transform.localEulerAngles = DirectionDefine.Instance().goLeft;
			
			temp = -h;
		}
		anim.SetFloat("Speed",temp);
		Vector3 direct = transform.forward * temp * Time.fixedDeltaTime * runSpeed;
		cc.Move(direct);
	}
	public void RunTurn(float v, float h)
	{
		
		// Vector3 direct = transform.forward * ver * Time.fixedDeltaTime * runSpeed;
		// cc.Move(direct);
		// anim.SetFloat("Direction",hor);
		// transform.RotateAroundLocal(Vector3.up,Time.fixedDeltaTime * hor * 3);
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Wall")
		{
			print("接触地面");
		}
	}
}
