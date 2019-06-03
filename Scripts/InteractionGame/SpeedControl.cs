using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedControl : MonoBehaviour {
	public GameObject vehicle;
	
	private VehicleControllerMSACC speed;
	private SpeedControl script;
	public int speedLimit;
	public GameObject warningToActivate;

	public GUIStyle customStyle;


	// Use this for initialization
	void Start () {

		speed = vehicle.GetComponent<VehicleControllerMSACC>();
		warningToActivate.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		EnteringSpeedLimit();
	}


	private void EnteringSpeedLimit()
	{
		if ((Mathf.RoundToInt(speed.KMh) > speedLimit))
		{
			warningToActivate.SetActive(true);
		}
		else
		{
			warningToActivate.SetActive(false);
		}
	}

	
}
