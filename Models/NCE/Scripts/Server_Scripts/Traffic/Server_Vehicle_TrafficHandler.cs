using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Server_Vehicle_TrafficHandler : MonoBehaviour {

	[Header ("Movement Variables")]
	public Transform CurrentNode; //Current node this vehicle is moving to
	//public GameObject UsedSpawnPoint; //The spawnpoint we used for this vehicle
	public float MaxSteerAngle; //Max steering angle for this vehicle
	public WheelCollider[] WheelColliders; //Reference to our wheelcolliders
	public float MaxTorque = 800f; //Max torque for our wheels
	private float CurrentTorque;
	public float MaxBrakeTorque = 2150f; //Max brake torque
	private float CurrentBrakeTorque;
	public float CurrentSpeed; 
	public float MaxSpeed; //Max speed this vehicle is supposed to go
	public bool isBraking = false; 
	public bool isSlowingDown = false;
	private float BrakeDistance; 
	public GameObject[] BackLights; //Reference to both backlights

	[Header ("Sensor Variables")]
	public float SensorLength;
	[SerializeField]private Transform FrontMiddleSensor;
	[SerializeField]private Transform FrontLeftSensor;
	[SerializeField]private Transform FrontRightSensor;

	private ReactionTime startTimer;


	void Start()
	{
		startTimer = GameObject.FindObjectOfType<ReactionTime>();
		startTimer.TimerStart();

		//CurrentNode = UsedSpawnPoint.GetComponent<Traffic_SpawnPoint> ().FirstNode; //Get the first node from our used spawnpoint

	}

	// Update is called once per frame
	void FixedUpdate () {
		 //Call these function only if we are the host client
			Sensors ();
			VehicleMove ();
			CheckForWayPoints ();
		
	}

	void Sensors()
	{
		BrakeDistance = 5f; 
		RaycastHit Hit;
		//FrontMiddleSensor
		if (Physics.Raycast (FrontMiddleSensor.transform.position, FrontMiddleSensor.transform.forward, out Hit, SensorLength)) {
			if (Hit.collider.transform.root.tag == "TrafficVehicle" || Hit.collider.transform.root.tag == "Player") { //If our sensor hits a player or other traffic vehicle
					isSlowingDown = true; //Slow our speed down
					StartCoroutine (SlowDownVehicle (1f)); 
				if (Vector3.Distance (Hit.point,FrontMiddleSensor.position) < BrakeDistance && !isBraking) { //If we are getting close to the other vehicle, brake
					isBraking = true; //Brake
					StartCoroutine (StopVehicle (1f, false)); //Tell our vehicle to stop
				}
			}
		}
		Debug.DrawLine (FrontMiddleSensor.position, FrontMiddleSensor.position + FrontMiddleSensor.transform.forward * SensorLength); //Draw our sensor raycast line when in the editor
		//FrontLeftSensor
		if (Physics.Raycast (FrontLeftSensor.transform.position, FrontLeftSensor.transform.forward, out Hit, SensorLength)) {
			if (Hit.collider.transform.root.tag == "TrafficVehicle" || Hit.collider.transform.root.tag == "Player") { //If our sensor hits a player or other traffic vehicle
				isSlowingDown = true; //Slow our speed down
				StartCoroutine (SlowDownVehicle (1f));
				if (Vector3.Distance (Hit.point,FrontLeftSensor.position) < BrakeDistance && !isBraking) { //If we are getting close to the other vehicle, brake
					isBraking = true; //Brake
					StartCoroutine (StopVehicle (1f, false)); //Tell our vehicle to stop
				}
			}
		}
		Debug.DrawLine (FrontLeftSensor.position, FrontLeftSensor.position + FrontLeftSensor.transform.forward * SensorLength); //Draw our sensor raycast line when in the editor
		//FrontRightSensor
		if (Physics.Raycast (FrontRightSensor.transform.position, FrontRightSensor.forward, out Hit, SensorLength)) {
			if (Hit.collider.transform.root.tag == "TrafficVehicle" || Hit.collider.transform.root.tag == "Player") { //If our sensor hits a player or other traffic vehicle
				isSlowingDown = true; //Slow our speed down
				StartCoroutine (SlowDownVehicle (1f));
				if (Vector3.Distance (Hit.point,FrontRightSensor.position) < BrakeDistance && !isBraking) { //If we are getting close to the other vehicle, brake
					isBraking = true; //Brake
					StartCoroutine (StopVehicle (1f, false)); //Tell our vehicle to stop
				}
			}
		}
		Debug.DrawLine (FrontRightSensor.position, FrontRightSensor.position + FrontRightSensor.transform.forward * SensorLength); //Draw our sensor raycast line when in the editor
	}


	void VehicleMove()
	{
		//Calulate the current speed
		CurrentSpeed = 2 * Mathf.PI * WheelColliders[2].radius * WheelColliders[2].rpm * 60 / 1000;


		if (CurrentSpeed < MaxSpeed && !isBraking && !isSlowingDown) { //If this vehicle isnt yet at his top speed and isnt braking or slowing down
			CurrentTorque = MaxTorque;
		} else { 
			CurrentTorque = 0f;
		}

		//Apply torque to the back wheels
		WheelColliders [2].motorTorque = CurrentTorque; 
		WheelColliders [3].motorTorque = CurrentTorque;

		Vector3 relativeVector = transform.InverseTransformPoint (CurrentNode.position); //Calculate our relative vector between our position and the next node
		float newSteer = (relativeVector.x / relativeVector.magnitude) * MaxSteerAngle; //Turn the steering wheels towards the next node while keeping the max steer angle in mind
		//Apply the new steer angle
		WheelColliders[0].steerAngle = newSteer;
		WheelColliders[1].steerAngle = newSteer;

		RpcRemoteVehicleMove (CurrentTorque, newSteer);
	}

	
	public void RpcRemoteVehicleMove(float torque, float steer) //Apply the motortorque and steerangle on this vehicle for all clients so the vehicle behaves better on their instances 
	{
		WheelColliders [2].motorTorque = torque;
		WheelColliders [3].motorTorque = torque;

		WheelColliders[0].steerAngle = steer;
		WheelColliders[1].steerAngle = steer;
	}

	void CheckForWayPoints ()
	{
		if(Vector3.Distance (transform.position, CurrentNode.position) < 5f && !isBraking){ //If we are a set distance from the next node and we arent braking, get the next node to move to
			GetNextNode ();
		}
	}

	void GetNextNode() //Get the next node to move to
	{
		if (CurrentNode.GetComponent<Traffic_Node> ().Stop == true) { //If this is a stopping position
			isBraking = true; 
			StartCoroutine (StopVehicle (5f, true));
			return; //Dont go further into this function
		}
		//CurrentNode = CurrentNode.GetComponent<Traffic_Node> ().NextNodes [CurrentNode.GetComponent<Traffic_Node> ().NextNodes.Count]; //Get the next node
		for (int i = 0; i < CurrentNode.GetComponent<Traffic_Node>().NextNodes.Count; i++)
		{
			CurrentNode = CurrentNode.GetComponent<Traffic_Node>().NextNodes[i];
		}

	}

	IEnumerator StopVehicle(float WaitTime, bool SetNextNode)
	{
		CurrentBrakeTorque = MaxTorque;
		WheelColliders [0].brakeTorque = CurrentBrakeTorque;
		WheelColliders [1].brakeTorque = CurrentBrakeTorque;
		WheelColliders [2].brakeTorque = CurrentBrakeTorque;
		WheelColliders [3].brakeTorque = CurrentBrakeTorque;
		//startTimer.TimerStart();
		print("car stopped");

		foreach (GameObject HL in BackLights) {  //Get every backlight from the backlights array
			HL.SetActive (true); //Set them as active
		}
		RpcRemoteStopVehicle (CurrentBrakeTorque, true);
		yield return new WaitForSeconds (WaitTime); //Stop time
		if(SetNextNode == true)
		{
			//CurrentNode = CurrentNode.GetComponent<Traffic_Node> ().NextNodes [CurrentNode.GetComponent<Traffic_Node> ().NextNodes.Count]; //Get the next node

			for (int i = 0; i < CurrentNode.GetComponent<Traffic_Node>().NextNodes.Count; i++)
			{
				CurrentNode = CurrentNode.GetComponent<Traffic_Node>().NextNodes[i];
			}
		}
		isBraking = false; //We dont need to be braking anymore
		foreach (GameObject HL in BackLights) {  //Get every backlight from the backlights array
			HL.SetActive (false); //Set them as active
		}
		CurrentBrakeTorque = 0f;
		//Apply the brake torque on all wheels
		WheelColliders [0].brakeTorque = CurrentBrakeTorque;
		WheelColliders [1].brakeTorque = CurrentBrakeTorque;
		WheelColliders [2].brakeTorque = CurrentBrakeTorque;
		WheelColliders [3].brakeTorque = CurrentBrakeTorque;
		RpcRemoteStopVehicle (CurrentBrakeTorque, false);
	}

	
	public void RpcRemoteStopVehicle(float brakeTorque, bool BackLight) //Apply the braketorque and backlights stance to all clients so braking looks better and other clients can see the brake lights on traffic vehicles
	{
		WheelColliders [0].brakeTorque = brakeTorque;
		WheelColliders [1].brakeTorque = brakeTorque;
		WheelColliders [2].brakeTorque = brakeTorque;
		WheelColliders [3].brakeTorque = brakeTorque;
		foreach (GameObject HL in BackLights) {  //Get every backlight from the backlights array
			HL.SetActive (BackLight); //Invert the current stance of the brakelights
		}
	}

	IEnumerator SlowDownVehicle(float SlowDownTime)
	{
		MaxSpeed = 5; //Cap the maxspeed
		yield return new WaitForSeconds (SlowDownTime); //Slowdown time
		isSlowingDown = false;
		MaxSpeed = 30f; //Reset the capspeed to its initial value
	}
}
