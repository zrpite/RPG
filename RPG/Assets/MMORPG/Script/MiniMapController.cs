using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 小地图控制
/// </summary>
public class MiniMapController : MonoBehaviour 
{
	/// <summary>
	/// 渲染小地图的相机
	/// </summary>
	public Camera minimapCamera;
	private Transform p_transform;
	private Vector3 offset = new Vector3(0, 25, 0);
	private Vector3 c_rotation = new Vector3(90, 0, 0);
	/// <summary>
	/// 相机高度
	/// </summary>
	private float nowSize = 6.0f;
	/// <summary>
	/// 相机最小视野
	/// </summary>
	private float minSize = 3.0f;
	/// <summary>
	/// 相机最大视野
	/// </summary>
	private float maxSize = 25.0f;
	/// <summary>
	/// 相机可改视野标志位
	/// </summary>
	public bool canResize = false;
	void Start()
	{
		canResize = false;
		p_transform = GameObject.FindWithTag("Player").transform;
	}
	void Update()
	{
		UpdatePosition();
		//调节相机视野
		if(canResize)
		{
			nowSize -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * 50 * Mathf.Abs(nowSize);
			nowSize = Mathf.Clamp(nowSize, minSize, maxSize);
			minimapCamera.orthographicSize = nowSize;
		}
	}
	/// <summary>
	/// 更新小地图相机位置(跟随玩家)
	/// </summary>
	public void UpdatePosition()
	{
		minimapCamera.transform.position = p_transform.position + offset;
		minimapCamera.transform.eulerAngles = c_rotation;
	}
	/// <summary>
	/// 鼠标移入小地图回调
	/// </summary>
	public void OnMouseHover()
	{
		canResize = true;
	}
	/// <summary>
	/// 鼠标移出小地图回调
	/// </summary>
	public void OnMouseOut()
	{
		canResize = false;
	}
}
