using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionTime : MonoBehaviour {

	//public Text ReactionTimeText;
	private float startTime;
	private float stopTime;
	private float timerTimer;
	private float seconds;
	private bool isRunning = false;
	public int numberOfObstacles;
	public float averageReactionTime;
	public bool obstacleLevel;
	

	private VehicleControllerMSACC VehicleController;



	// Use this for initialization
	void Start () {
		startTime = Time.time;
		VehicleController = GameObject.FindObjectOfType<VehicleControllerMSACC>();
		obstacleLevel = false;
	}

	// Update is called once per frame
	void Update () {

		//averageReactionTime = 0;

		
			float timerTime = stopTime + (Time.time - startTime);
			//string minutes = ((int)timerTime / 60).ToString("f0");
			seconds = (timerTime % 60);

			if ((Mathf.RoundToInt(VehicleController.KMh)) == 0)
			{
				TimerStop();
			}
		

		//else if(obstacleLevel == false)
		//{
		//	averageReactionTime = 0;
		//}
		
	}

	public void TimerStart()
	{
		if (!isRunning)
		{
			//print("TimerStart");
			isRunning = true;
			startTime = Time.time;
		}		
		
	}

	public void TimerStop()
	{
		if (isRunning)
		{
			//print("TimerStop");
			isRunning = false;
			startTime = Time.time;

		}

	}

	public float returnReactionTime()
	{
		averageReactionTime = seconds / numberOfObstacles;

		return averageReactionTime;
	}

	private void OnApplicationQuit()
	{
		
		print(seconds);
		print ("average reaction time" + seconds / numberOfObstacles);
		
	}


}
