using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour {
	public GameObject trigger;
	private SpeedControl speedLimit;
	public int newSpeedLimit;

	private SpeedControl script;

	void Start()
	{
		script = trigger.GetComponent<SpeedControl>();
		speedLimit = trigger.GetComponent<SpeedControl>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			speedLimit.speedLimit = newSpeedLimit;
			print(speedLimit);
		}

	}
}
