using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 暂不用的镜头旋转脚本(不用)
/// </summary>
public class MouseRotate : MonoBehaviour 
{
	public Transform target;
	public float distance = 4.0f;
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;
	private  float x = 0.0f;
	private  float y = 0.0f;
	void Start () {
		Cursor.visible = false;
   		Vector3 angles = transform.eulerAngles;
   		x = angles.y;
   		y = angles.x;
		// Make the rigid body not change rotation
   		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

	void LateUpdate () {
    	if (target) {
        	x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        	y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
 		
 			y = ClampAngle(y, yMinLimit, yMaxLimit);
 		       
        	Quaternion rotation = Quaternion.Euler(y, x, 0);
        	Vector3 position = rotation * new Vector3(0.0f, 1, -distance) + target.position;
			
            	transform.rotation = rotation;
            	transform.position = position;
    	}
	}

	static float ClampAngle (float angle, float min, float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
