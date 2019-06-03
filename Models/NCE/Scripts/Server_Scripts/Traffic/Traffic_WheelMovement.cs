using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic_WheelMovement : MonoBehaviour {

	public WheelCollider TargetCollider;
	private Vector3 WheelPos = new Vector3();
	private Quaternion WheelRot = new Quaternion();

	private void Update()
	{
		TargetCollider.GetWorldPose (out WheelPos, out WheelRot);
		transform.position = WheelPos;
		transform.rotation = WheelRot;
	}
}
