using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 僵尸生成器
/// </summary>
public class ZombieSpawner : MonoBehaviour 
{
	/// <summary>
	/// 要生成的僵尸数组
	/// </summary>
	public GameObject[] zombie;
	/// <summary>
	/// 此生成的僵尸
	/// </summary>
	public GameObject thisSpawnedZombie = null; 
	private static ZombieSpawner _instance = null;
	public static ZombieSpawner Instance()
	{
		return _instance;
	}
	void Awake()
	{
		_instance = this;
	}
	/// <summary>
	/// 根据僵尸id生成僵尸
	/// </summary>
	/// <param name="index">僵尸id</param>
	public void SpawnZombieByType(int index)
	{
		thisSpawnedZombie = Instantiate(zombie[index], transform.position, transform.rotation) as GameObject;
	}
}
