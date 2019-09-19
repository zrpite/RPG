using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 集中声明一些player要用的对象,定义一些动画状态
/// </summary>
public class AnimatorMotor : MonoBehaviour {
	protected Rigidbody rb;
	protected Vector3 velocity;
	protected Vector3 horizontiy;
	protected Animator anim;						
	protected AnimatorStateInfo currentBaseState;	
	///动画状态定义(用于判定player所处状态)
	//static int deadState = Animator.StringToHash("Base Layer.Dead");
	//static int ss_cState = Animator.StringToHash("Base Layer.SS_C");
	static int dieState = Animator.StringToHash("Base Layer.die");
	protected static int atk1State = Animator.StringToHash("Base Layer.atk_1");
	protected static int atk2State = Animator.StringToHash("Base Layer.atk_2");
	protected static int atk3State = Animator.StringToHash("Base Layer.atk_3");
	protected static int idle_cState = Animator.StringToHash("Base Layer.Idle_C");
	protected static int idle_aState = Animator.StringToHash("Base Layer.Idle_A");
	protected static int idle_akState = Animator.StringToHash("Base Layer.idle_atk");
	protected static int locoState = Animator.StringToHash("Base Layer.Locomotion");
	protected static int jumpState = Animator.StringToHash("Base Layer.Jump");
	protected static int backState = Animator.StringToHash("Base Layer.Walk_back_skirt");
	protected static int rotateState = Animator.StringToHash("Base Layer.Rotation");
	void Awake()
	{
		//Cursor.visible = false;
	}













	
}
