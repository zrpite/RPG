using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 使角色始终面向摄像机正前方的脚本(不用)
/// </summary>
public class JudgeDirection : MonoBehaviour 
{
	/// <summary>
	/// camera面向方向在水平方向的投影对象引用
	/// </summary>
	public Transform cameraZHor;
	/// <summary>
	/// player与投影对象所成角度
	/// </summary>
	private float angles = 0;
	// Update is called once per frame
	void Update () {
		JudgeAngles();
		//如果旋转标识开启,并且player可控,那么就将player向camera面向方向过渡
		if(Global.needTurn && Global.CanControl){
			transform.rotation = Quaternion.Lerp(transform.rotation,cameraZHor.rotation,0.1f);
		}else{

		}
	}
	/// <summary>
	/// 计算player与投影对象所成角度
	/// </summary>
	void JudgeAngles(){
		angles = Quaternion.Angle(transform.rotation,cameraZHor.rotation);
		// if(angles <= 30){
		// 	needTurn = false;
		// }
		// else
		// 	needTurn = true;
	}
}
