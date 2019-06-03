using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObstacle : MonoBehaviour {

	
	public GameObject obstacleToActivate;
	private VehicleControllerMSACC isObstacleZone;
	private ReactionTime startTimer;

	// Use this for initialization
	void Start () {

		isObstacleZone = GameObject.FindObjectOfType<VehicleControllerMSACC>();
		startTimer = GameObject.FindObjectOfType<ReactionTime>();

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			obstacleToActivate.SetActive(true);
			isObstacleZone.isObstacleZone = true;
			//print("speed not calculated");
			startTimer.TimerStart();
			

		}
	}
}
