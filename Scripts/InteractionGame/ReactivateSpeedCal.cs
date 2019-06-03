using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactivateSpeedCal : MonoBehaviour {

	public GameObject obstacleToDestroy;
	private VehicleControllerMSACC isObstacleZone;
	private ReactionTime stopTimer;
	private string obstacleName;



	public List<string> ObstInfo = new List<string>();




	// Use this for initialization
	void Start ()
	{
		isObstacleZone = GameObject.FindObjectOfType<VehicleControllerMSACC>();
		stopTimer = GameObject.FindObjectOfType<ReactionTime>();
		obstacleName = transform.parent.name;
	}

	
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			isObstacleZone.isObstacleZone = false;
			stopTimer.TimerStop();
			//Destroy(obstacleToDestroy);
			//ObstInfo.Add(obstacleToDestroy.name + " : " + entranceInBubble.BubbleEntrance() );

		}
	}

	

}
