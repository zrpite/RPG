using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearChange : MonoBehaviour {
	private static AppearChange _instance = null;
	public static AppearChange Instance()
	{
		return _instance;
	}
	public WeaponTrail weapontrail;
	public MeshRenderer meshrender;
	public Material oldmat,newmat;
	public GameObject line;
	void Awake()
	{
		_instance = this;
	}

	void Start () {
		
	}
	
	void Update () {
		
	}
	public void SwitchWeaponAppear(int id)
	{
		if(id == 2022)
		{
			Global.changeWeapon = true;
			meshrender.material = newmat;
			line.SetActive(true);
			weapontrail.startColor = Color.cyan;
		}
		else
		{
			Global.changeWeapon = false;
			meshrender.material = oldmat;
			line.SetActive(false);
			weapontrail.startColor = Color.red;
		}
	}
}
