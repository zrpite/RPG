using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家属性信息管理类
/// </summary>
public class PlayerStatusManager : AnimatorMotor
{
	private static PlayerStatusManager _instance = null;
	public static PlayerStatusManager Instance()
	{
		return _instance;
	}
	/// <summary>
	/// 玩家当前HP值
	/// </summary>
	public int currentHp = 0;
	/// <summary>
	/// 玩家当前MP值
	/// </summary>
	public int currentMp = 0;
	/// <summary>
	/// 玩家当前Exp值
	/// </summary>
	public int currentExp = 0;
	/// <summary>
	/// 显示受伤害数值的prefab
	/// </summary>
	public GameObject damageObj,damageObj2;
	/// <summary>
	/// 受伤害数值的标签
	/// </summary>
	public TextMesh damageText,damageText2;
	/// <summary>
	/// 玩家血条
	/// </summary>
	public UISlider hpSlider;
	/// <summary>
	/// 玩家Hp标签
	/// </summary>
	public UILabel hpNum;
	/// <summary>
	/// 玩家蓝条
	/// </summary>
	public UISlider mpSlider;
	/// <summary>
	/// 玩家Mp标签
	/// </summary>
	public UILabel mpNum;
	/// <summary>
	/// 玩家经验条
	/// </summary>
	public UISlider expSlider;
	/// <summary>
	/// 玩家Exp标签
	/// </summary>
	public UILabel expNum;
	/// <summary>
	/// 玩家等级标签
	/// </summary>
	public UILabel rankNum;
	public CapsuleCollider colli;
	public CharacterController cc;
	void Awake()
	{
		_instance = this;
	}
	void Start () {
		anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
		currentHp = PlayerStatusInfo.Instance().maxHp;
		currentMp = PlayerStatusInfo.Instance().maxMp;
		currentExp = 0;
	}
	
	void Update () {
		//根据当前Hp/Mp/Exp和最大Hp/Mp/Exp百分比控制hp/Mp/Exp条百分比
		hpSlider.value = ((float)currentHp/(float)PlayerStatusInfo.Instance().maxHp);
		hpNum.text = currentHp.ToString();
		mpSlider.value = ((float)currentMp/(float)PlayerStatusInfo.Instance().maxMp);
		mpNum.text = currentMp.ToString();
		expSlider.value = ((float)currentExp/(float)PlayerStatusInfo.Instance().maxExp);
		expNum.text = currentExp.ToString();

		rankNum.text = PlayerStatusInfo.Instance().rank.ToString();
	}
	/// <summary>
	/// 加Hp
	/// </summary>
	/// <param name="num">加Hp数量</param>
	public void AddHp(int num)
	{
		currentHp = Mathf.Clamp(currentHp += num, 0, PlayerStatusInfo.Instance().maxHp);
		damageText.text = "HP+" + num.ToString();
		damageText.color = Color.green;
		GameObject obj = Instantiate(damageObj,damageObj.transform.position,damageObj.transform.rotation);
		obj.transform.parent = colli.transform;
	}
	/// <summary>
	/// 加Mp
	/// </summary>
	/// <param name="num">加Mp数量</param>
	public void AddMp(int num)
	{
		currentMp = Mathf.Clamp(currentMp += num, 0, PlayerStatusInfo.Instance().maxMp);
		damageText2.text = "MP+" + num.ToString();
		damageText2.color = Color.blue;
		GameObject obj = Instantiate(damageObj2,damageObj2.transform.position,damageObj2.transform.rotation);
		obj.transform.parent = colli.transform;
	}
	/// <summary>
	/// 扣Hp
	/// </summary>
	/// <param name="num">扣Hp数量</param>
	public void ReduceHp(int num)
	{
		currentHp = Mathf.Clamp(currentHp -= num, 0, PlayerStatusInfo.Instance().maxHp);
		damageText.text = "HP-" + num.ToString();
		damageText.color = Color.white;
		GameObject obj = Instantiate(damageObj,damageObj.transform.position,damageObj.transform.rotation);
		obj.transform.parent = colli.transform;
		//一般情况下只有扣血时才会死
		if(currentHp <= 0)
		{
			Global.playerState = Global.State.DieState;
			colli.enabled = false;
			cc.enabled = false;
			anim.SetBool("Die",true);	
		}
	}
	/// <summary>
	/// 扣Mp
	/// </summary>
	/// <param name="num">扣Mp数量</param>
	public bool ReduceMp(int num)
	{
		if(currentMp - num < 0)
		{
			Debug.Log("蓝不够");
			return false;
		}
		currentMp -= num;
		damageText2.text = "MP-" + num.ToString();
		damageText2.color = Color.blue;
		GameObject obj = Instantiate(damageObj2,damageObj2.transform.position,damageObj2.transform.rotation);
		obj.transform.parent = colli.transform;
		return true;
	}
	/// <summary>
	/// 增加Exp
	/// </summary>
	/// <param name="num">增加Exp数目</param>
	public void AddExp(int num)
	{
		currentExp += num;
		//升级
		if(currentExp >= PlayerStatusInfo.Instance().maxExp)
		{
			AddRankAndProp(1);
			BoardManager.Instance().SwitchShowBoard(BoardManager.BoardShow.UPGRADEBOARD);
			currentExp -= PlayerStatusInfo.Instance().maxExp;
		}
	}
	/// <summary>
	/// 增加等级,属性,点数
	/// </summary>
	/// <param name="num">升级数目(默认1)</param>
	public void AddRankAndProp(int num = 1)
	{
		PlayerStatusInfo.Instance().rank += num;
		SkillBoard.Instance().ListenToUnlockSkill();
		PlayerStatusInfo.Instance().attack += 5;
		PlayerStatusInfo.Instance().def += 3;
		PlayerStatusInfo.Instance().speed += 1;
		AddPoint(5);
	}
	/// <summary>
	/// 使用物品加Hp和Mp
	/// </summary>
	/// <param name="info">使用的物品的ObjectInfo</param>
	public void AddHpAndMp(ObjectInfo info)
	{
		currentHp = Mathf.Clamp(currentHp += info.hp,0,PlayerStatusInfo.Instance().maxHp);
		currentMp = Mathf.Clamp(currentMp += info.mp,0,PlayerStatusInfo.Instance().maxMp);
	}
	/// <summary>
	/// 增加玩家属性值(装备)
	/// </summary>
	/// <param name="info"></param>
	public void AddProperties(ObjectInfo info)
	{
		PlayerStatusInfo.Instance().attack += info.attack;
		PlayerStatusInfo.Instance().def += info.def;
		PlayerStatusInfo.Instance().speed += info.speed;
		//更新一次玩家信息界面显示
		StatusBoard.Instance().UpdateStatusShow();
	}
	/// <summary>
	/// 增加玩家属性值(加点)
	/// </summary>
	/// <param name="selectName">点击的加点按钮明名称</param>
	/// <param name="num">默认一次加10点</param>
	public void AddProperties(string selectName, int num = 10)
	{
		switch(selectName)
		{
			case "AtkButton" : PlayerStatusInfo.Instance().attack += num; break;
			case "DefButton" : PlayerStatusInfo.Instance().def += num; break;
			case "SpButton" : PlayerStatusInfo.Instance().speed += num; break;
		}
		//更新一次玩家信息界面显示
		StatusBoard.Instance().UpdateStatusShow();
	}
	/// <summary>
	/// 增加玩家属性值(技能)
	/// </summary>
	/// <param name="type">技能加持buff类型</param>
	/// <param name="num">buff数值</param>
	public void AddProperties(SkillInfo.SkillEffectType type, int num)
	{
		Debug.Log(type.ToString());
		switch(type)
		{
			case SkillInfo.SkillEffectType.ATTACK : PlayerStatusInfo.Instance().attack += num; break;
			case SkillInfo.SkillEffectType.DEF : 	PlayerStatusInfo.Instance().def += num;    break;
			case SkillInfo.SkillEffectType.SPEED :  PlayerStatusInfo.Instance().speed += num;  break;
		}
		//更新一次玩家信息界面显示
		StatusBoard.Instance().UpdateStatusShow();
	}
	/// <summary>
	/// 减少玩家属性值(装备)
	/// </summary>
	/// <param name="info"></param>
	public void ReduceProperties(ObjectInfo info)
	{
		PlayerStatusInfo.Instance().attack -= info.attack;
		PlayerStatusInfo.Instance().def -= info.def;
		PlayerStatusInfo.Instance().speed -= info.speed;
		//更新一次玩家信息界面显示
		StatusBoard.Instance().UpdateStatusShow();
	}
	/// <summary>
	/// 减少玩家属性值(技能)
	/// </summary>
	/// <param name="type">技能加持buff类型</param>
	/// <param name="num">buff数值</param>
	public void ReduceProperties(SkillInfo.SkillEffectType type, int num)
	{
		switch(type)
		{
			case SkillInfo.SkillEffectType.ATTACK : PlayerStatusInfo.Instance().attack -= num; break;
			case SkillInfo.SkillEffectType.DEF : 	PlayerStatusInfo.Instance().def -= num;    break;
			case SkillInfo.SkillEffectType.SPEED :  PlayerStatusInfo.Instance().speed -= num;  break;
		}
		//更新一次玩家信息界面显示
		StatusBoard.Instance().UpdateStatusShow();
	}
	/// <summary>
	/// 增加升级点数
	/// </summary>
	/// <param name="num">增加数量(默认5)</param>
	public void AddPoint(int num = 5)
	{
		PlayerStatusInfo.Instance().point += num;
		//更新一次玩家信息界面显示
		StatusBoard.Instance().UpdateStatusShow();
	}
	public void ReducePoint(int num = 1)
	{
		PlayerStatusInfo.Instance().point -= num;
		//更新一次玩家信息界面显示
		StatusBoard.Instance().UpdateStatusShow();
	}

}
