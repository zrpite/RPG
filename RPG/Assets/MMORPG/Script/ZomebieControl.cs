using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 僵尸逻辑控制类
/// </summary>
public class ZomebieControl : MonoBehaviour 
{
	/// <summary>
	/// 此僵尸的id
	/// </summary>
	public int thisZombieIndex = 0;
	public EnemyCommon.CurrentState zombieState = EnemyCommon.CurrentState.Parse;
	/// <summary>
	/// 僵尸和player的距离
	/// </summary>
	public float dis;
	//public AttackController atkC;
	/// <summary>
	/// 要跟随的玩家坐标
	/// </summary>
	private Transform followpos;
	/// <summary>
	/// 巡逻路径
	/// </summary>
	public Transform[] paths;
	/// <summary>
	/// iTween参数
	/// </summary>
	private Hashtable args;

	public Animator anim;
	
	/// <summary>
	/// 僵尸生命值
	/// </summary>
	public int life = 900;
	/// <summary>
	/// 僵尸攻击力
	/// </summary>
	public int attackPower = 80;
	/// <summary>
	/// 死亡提供的经验值
	/// </summary>
	public int provideExp = 50;

	/// <summary>
	/// 控制任务进度更新和物品掉落只执行一次
	/// </summary>
	private bool onceDropAndAchievement = true;
	void Start () 
	{
		thisZombieIndex = 0;
		onceDropAndAchievement = true;
		followpos = GameObject.FindWithTag("Player").GetComponentInChildren<FollowPos>().transform;
		dis = Vector3.Distance(transform.position,followpos.position);
		zombieState  = EnemyCommon.CurrentState.Parse;
		//iTween参数初始化
		args = new Hashtable(); 
		args.Add("path",paths);
		args.Add("easeType","Linear");
		args.Add("speed",2);
		args.Add("movetopath",true);
		args.Add("orienttopath",true);
		args.Add("loopType","Loop");
		args.Add("delay",0.1f);
		iTween.MoveTo(gameObject,args);
	}
	void Update () 
	{
		updateDistance();
		switch(zombieState)
		{
			case EnemyCommon.CurrentState.Parse : DoParse(); break;
			case EnemyCommon.CurrentState.Follow : DoFollow(); break;
			case EnemyCommon.CurrentState.Attack : DoAttack(); break;
			case EnemyCommon.CurrentState.Die : DoDie(); break;
		}
	}
	/// <summary>
	/// 更新僵尸与player的距离
	/// </summary>
	void updateDistance(){
		dis = Vector3.Distance(transform.position,followpos.position);
	}
	/// <summary>
	/// 巡逻
	/// </summary>
	void DoParse()
	{
		if(dis > 1.0f && dis < 6.0f)
		{
			iTween.Stop();			
			zombieState = EnemyCommon.CurrentState.Follow;
		}
	}
	/// <summary>
	/// 追踪操作
	/// </summary>
	void DoFollow(){
		anim.SetBool("Attack",false);
		transform.LookAt(followpos);
		transform.position = Vector3.MoveTowards(transform.position,followpos.position,Time.deltaTime);
		if(life <= 0)
		{
			zombieState = EnemyCommon.CurrentState.Die;
		}
		if(dis > 10.0f)
		{
			//iTween.Resume();
			iTween.MoveTo(gameObject,args);
			zombieState = EnemyCommon.CurrentState.Parse;
		}
		else if(dis < 1.6f)
		{
			zombieState = EnemyCommon.CurrentState.Attack;
		}
	}
	/// <summary>
	/// 攻击操作
	/// </summary>
	void DoAttack(){
		transform.LookAt(followpos);
		anim.SetBool("Attack",true);
		if(dis > 1.6f && dis < 6.0f)
		{
			zombieState = EnemyCommon.CurrentState.Follow;
		}
	}
	/// <summary>
	/// 死亡操作
	/// </summary>
	void DoDie()
	{
		//死亡后加一次对应任务进度,掉落一个物品
		if(onceDropAndAchievement)
		{
			if(QuestManager.Instance().questState != QuestManager.QuestState.GETREWARD)
			{
				QuestManager.Instance().QuestStateUpdateByType(thisZombieIndex);
			}
			anim.SetBool("Die",true);
			this.GetComponent<CapsuleCollider>().enabled = false;
			PlayerStatusManager.Instance().AddExp(provideExp);
			ItemDropManager.Instance().CreateRandomDropItem(transform.position);
			onceDropAndAchievement = false;
			Destroy(this.gameObject,3);
		}
		
	}
	/// <summary>
	/// 在zombie的attack动画中添加event片段,执行此函数,对player造成等同于zombie攻击力数值的伤害
	/// </summary>
	public void Attack()
	{
		if(PlayerStatusManager.Instance().currentHp > 0)
		{
			PlayerStatusManager.Instance().ReduceHp(attackPower - (int)(PlayerStatusInfo.Instance().def * 0.2f));
		}
	}
}
