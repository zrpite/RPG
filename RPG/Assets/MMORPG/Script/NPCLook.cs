using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// npc看向player
/// </summary>
public class NPCLook : MonoBehaviour 
{
	/// <summary>
	/// player对象
	/// </summary>
	public GameObject player;
	/// <summary>
	/// npc头部
	/// </summary>
	public GameObject head;
	private Quaternion initrotation;
	void Start()
	{
		initrotation = head.transform.rotation;
	}
	void OnTriggerStay(Collider other)
	{
		head.transform.LookAt(player.transform);
	}
	void OnTriggerExit(Collider other)
	{
		head.transform.rotation = initrotation;
	}
}