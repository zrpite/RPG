using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 掉落物品的信息类
/// </summary>
public class ItemDropedInfo : MonoBehaviour 
{
	/// <summary>
	/// 物品id
	/// </summary>
	public int id = 0;
	/// <summary>
	/// 显示图标的组件
	/// </summary>
	public SpriteRenderer sprender;
	/// <summary>
	/// 存储可选择的物品图标
	/// </summary>
	public Sprite[] iconname;

	/// <summary>
	/// 设置当前掉落物品的id和显示的icon
	/// </summary>
	/// <param name="id">掉落物品的id</param>
	/// <param name="name">对应id的物品icon名</param>
	public void SetIconAndId(int id, string name)
	{
		this.id = id;
		switch(name)
		{
			case "icon-potion1" : sprender.sprite = iconname[0]; break;
			case "icon-potion2" : sprender.sprite = iconname[1]; break;
			case "icon-potion3" : sprender.sprite = iconname[2]; break;
		}
	}
	//这里设置成从物品上走开再拾取,避免物品刚下落就被捡起看不到物品
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			BagBoard.Instance().PickOneItemById(id);
			this.GetComponent<BoxCollider>().enabled = false;
			Destroy(this.gameObject,0.1f);
		}
	}
}
