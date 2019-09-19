using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 不用
/// </summary>
public class DirectionDefine : MonoBehaviour {
	private static DirectionDefine _instance= null;
	public static DirectionDefine Instance()
	{
		return _instance;
	}
	public Vector3 goLeft;
	public Vector3 goRight;
	public Vector3 goForward;
	public Vector3 goBehind;
	public Transform cz_transform;
	void Awake()
	{
		_instance = this;
	}
	void Start () 
	{
		goLeft = Vector3.zero;
		goRight = Vector3.zero;
		goForward = Vector3.zero;
		goBehind = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 temp = cz_transform.localEulerAngles;

		goForward = cz_transform.localEulerAngles;

		goBehind = temp + Vector3.up * 180; 

		goLeft = temp + Vector3.up * (-90);

		goRight = temp + Vector3.up * 90;
	}

}
