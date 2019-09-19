using UnityEngine;
using System.Collections;
/// <summary>
/// 摄像机功能类
/// </summary>
public class ARPGcameraC : MonoBehaviour 
{
	/// <summary>
	/// 跟随对象
	/// </summary>
	public Transform target;
	public Transform targetBody;
	/// <summary>
	/// 对象高度
	/// </summary>
	public float targetHeight = 1.2f;
	/// <summary>
	/// 默认镜头距离
	/// </summary>
	public float distance = 4.0f;
	/// <summary>
	/// 最大镜头距离
	/// </summary>
	public float maxDistance = 6;
	/// <summary>
	/// 最小镜头距离
	/// </summary>
	public float minDistance = 1.0f;
	/// <summary>
	/// x方向旋转速度
	/// </summary>
	public float xSpeed = 250.0f;
	/// <summary>
	/// y方向旋转速度
	/// </summary>
	public float ySpeed = 120.0f;
	/// <summary>
	/// y方向下边界
	/// </summary>
	public float yMinLimit = -40;
	/// <summary>
	/// y方向上边界
	/// </summary>
	public float yMaxLimit = 70;
	/// <summary>
	/// 镜头缩放速率
	/// </summary>
	public float zoomRate = 70;
	private float x = 20.0f;
	private float y = 0.0f;
	public Quaternion aim;
	public float aimAngle = 8;
	public bool  lockOn = false;
	RaycastHit hit;
	
	void Start()
	{
		if(!target)
		{
			target = GameObject.FindWithTag("Player").transform;
		}
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	void LateUpdate()
	{
		//按住Alt释放鼠标
		if(Input.GetKeyDown(KeyCode.LeftAlt))
		{
			Global.playerState = Global.State.TipState;
		}
		//松开Alt锁定鼠标
		if(Input.GetKeyUp(KeyCode.LeftAlt))
		{
			Global.playerState = Global.State.NormalState;
		}
		//根据状态被动决定鼠标状态
		if(!Global.CanControl)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		if(!target) return;
		if(!targetBody)
		{
      		targetBody = target;
      	}
		//如果暂停,镜头不转
		if(Time.timeScale == 0.0f)
		{
			return;
		}
		if(Global.CanControl)
		{
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
		}
		
		
		distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
		distance = Mathf.Clamp(distance, minDistance, maxDistance);
		
		y = ClampAngle(y, yMinLimit, yMaxLimit);
		
		//旋转镜头
		Quaternion rotation = Quaternion.Euler(y, x, 0);
		if(Global.CanControl)
		{
			transform.rotation = rotation;
			aim = Quaternion.Euler(y- aimAngle, x, 0);
		}
	
		//移动镜头
		Vector3 positiona = target.position - (rotation * Vector3.forward * distance + new Vector3(0.0f,-targetHeight,0.0f));
		if(Global.CanControl)
		{
			transform.position = positiona;
		}

		Vector3 trueTargetPosition = target.transform.position - new Vector3(0.0f,-targetHeight,0.0f);

		//确保镜头不穿透地表
		if (Physics.Linecast (trueTargetPosition, transform.position, out hit)) 
		{
			if(hit.transform.tag == "Wall")
			{
				float tempDistance = Vector3.Distance (trueTargetPosition, hit.point) - 0.28f;
				
				positiona = target.position - (rotation * Vector3.forward * tempDistance + new Vector3(0,-targetHeight,0));
				transform.position = positiona;
			}
			
		}
	}
	/// <summary>
	/// 确保旋转在-360~360之间
	/// </summary>
	static float ClampAngle(float angle , float min , float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}