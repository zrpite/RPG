using UnityEngine;
using System.Collections;
/// <summary>
/// player移动控制
/// </summary>
public class MoveController : AnimatorMotor
{
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
	}
	void FixedUpdate()
	{
		//水平、竖直方向的输入轴值(有平滑过渡的(-1~1))
		float ver = Input.GetAxis("Vertical");
		float hor = Input.GetAxis("Horizontal");
		anim.speed = animSpeed;
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		//基于可控制的先决条件
		if(Global.CanControl)
		{
			anim.SetFloat("Speed",ver);
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
			

			if(currentBaseState.fullPathHash == idle_cState || currentBaseState.fullPathHash == idle_aState)
			{
				//Debug.Log("战斗姿势");
				if(hor > 0.5f || hor < -0.5f)
				{
					if(Global.CanControl)
					anim.SetBool("Turn",true);
				}
			}
			if(currentBaseState.fullPathHash == rotateState)
			{
				//Debug.Log("站立旋转状态");
				if(Global.CanControl)
				{
					anim.SetFloat("Direction",hor);
					transform.RotateAroundLocal(Vector3.up,Time.fixedDeltaTime * hor * 3);
					if(hor < 0.1f && hor > -0.1f)
					{
						anim.SetBool("Turn",false);
					}
					//保证站立旋转时按前进或后退可切换动作
					if(ver > 0.05f || ver < -0.05f)
					{
						anim.SetFloat("Speed",ver);
					}
				}
				else
				{
					anim.SetBool("Turn",false);
				}
			}
			if(currentBaseState.fullPathHash == locoState)
			{
				//Debug.Log("行走状态");
				
				if(Global.CanControl)
				{
					Vector3 direct = transform.forward * ver * Time.fixedDeltaTime * runSpeed;
					cc.Move(direct);
					//transform.Translate(new Vector3(0,0,ver * Time.fixedDeltaTime * runSpeed));
					anim.SetFloat("Direction",hor);
					transform.RotateAroundLocal(Vector3.up,Time.fixedDeltaTime * hor * 3);
				}
				
			}
			if(currentBaseState.fullPathHash == backState)
			{
				//Debug.Log("后退状态");
				if(Global.CanControl)
				{
					Vector3 direct = transform.forward * ver * Time.fixedDeltaTime * walkSpeed;
					cc.Move(direct);
					//transform.Translate(new Vector3(0,0,ver * Time.fixedDeltaTime * walkSpeed));
					anim.SetFloat("Direction",hor);
					transform.RotateAroundLocal(Vector3.up,-Time.fixedDeltaTime * hor * 3);
				}
			}
			// RaycastHit hit;
        	// if(Physics.Raycast(transform.position, -Vector3.up, out hit))
			// {
			// 	Debug.Log(hit.distance);
			// 	if(hit.distance <= 0.3f)
			// 	{
			// 		anim.SetBool("Jump",false);
			// 	}
			// }
			
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Wall")
		{
			print("接触地面");
		}
	}

}
