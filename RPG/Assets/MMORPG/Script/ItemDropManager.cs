using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物品掉落功能管理类
/// </summary>
public class ItemDropManager : MonoBehaviour
 {
	private static ItemDropManager _instance = null;
	public static ItemDropManager Instance()
	{
		return _instance;
	}
	/// <summary>
	/// 要生成的物品
	/// </summary>
	public ItemDropedInfo item;
	void Awake()
	{
		_instance = this;
	}
	/// <summary>
	/// 随机掉落一个物品
	/// </summary>
	/// <param name="pos">掉落位置</param>
	public void CreateRandomDropItem(Vector3 pos)
	{
		int kind = Random.Range(1,3);
		int id = 0;
		switch(kind)
		{
			case 1 : id = Random.Range(1001,1004); break;
			case 2 : id = Random.Range(2001,2023); break;
		}
		item.SetIconAndId(id,ObjectsInfo.Instance().GetObjectInfoById(id).icon_name);
		Instantiate(item.gameObject,pos + Vector3.up * 0.3f,Quaternion.identity);
	}
}
