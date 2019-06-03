using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Server_TrafficManager : MonoBehaviour {

	public Transform Nodes;
	public GameObject[] TrafficVehiclePrefabs;
	public Transform[] TrafficSpawns;

	// Use this for initialization
	void Start()
	{
		
			SpawnVehicles ();
		
	}

	//This code is only ran on the host client
	void SpawnVehicles()
	{
		foreach (Transform TrafficSpawn in TrafficSpawns) { //For each trafficspawn in our level, spawn a traffic vehicle there
			int RandomTrafficPrefabInt = Random.Range (0, TrafficVehiclePrefabs.Length);
			GameObject TrafficVehicle = Instantiate(TrafficVehiclePrefabs[RandomTrafficPrefabInt], TrafficSpawn.transform.position, TrafficSpawn.transform.rotation) as GameObject; 
			//NetworkServer.Spawn (TrafficVehicle); //Spawn this vehicle into our networked game!
			//TrafficVehicle.GetComponent<Server_Vehicle_TrafficHandler> ().UsedSpawnPoint = TrafficSpawn.gameObject; //Set a reference to the used spawn in the traffichandler on the vehicle
		}
	}
}
