using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {


	//Counters
	private int pedestrianRHit = 0;
	private int pedestrianLHit = 0;

	private int animalRHit = 0;
	private int animalLHit = 0;

	private int objectsHit = 0;

	private int obstaclesR = 0;
	private int obstaclesL = 0;


	private int totalHit = 0;
	private string tag = "";
	public List<string> HitList = new List<string>();


	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "PedestrianR")
		{
			tag = "PedestrianR";
			HitList.Add(tag);
			pedestrianRHit ++;
			obstaclesR++;
			totalHit ++;		
			//Destroy(other.gameObject);
		}

		if (other.tag == "PedestrianL")
		{
			tag = "PedestrianL";
			HitList.Add(tag);
			pedestrianLHit++;
			obstaclesL++;
			totalHit++;
			//Destroy(other.gameObject);
		}

		if (other.tag == "AnimalCatR")
		{
			tag = "AnimalCatR";
			HitList.Add(tag);
			animalRHit++;
			obstaclesR++;
			totalHit++;	
			//Destroy(other.gameObject);
		}

		if (other.tag == "AnimalCatL")
		{
			tag = "AnimalCatL";
			HitList.Add(tag);
			animalLHit++;
			obstaclesL++;
			totalHit++;
			//Destroy(other.gameObject);
		}
	}

	public string obstaclesHit()
	{
		string obstacle = ",";
		foreach (string str in HitList)
		{
			obstacle = str + obstacle; 
		}
		return obstacle;
	}


	public int returnPedestrianRHit()
	{
		return pedestrianRHit;
	}

	public int returnPedestrianLhit()
	{
		return pedestrianLHit;
	}

	public int returnAnimalRHit()
	{
		return animalRHit;
	}

	public int returnAnimalLHit()
	{
		return animalLHit;
	}

	public int TotalObRHit()
	{
		return obstaclesR;
	}

	public int TotalObLHit()
	{
		return obstaclesL;
	}



	void OnApplicationQuit()
	{
		print("PED R = " + pedestrianRHit);
		print("PED L = " + pedestrianLHit);
		print("AnimalR = " + animalRHit);
		print("AnimalL = " + animalLHit);

		print("Total Obstacles L = " + obstaclesL);
		print("Total Obstacles R = " + obstaclesR);
		print("TOTAL Obstacles L/R = " + totalHit);
		print(HitList);
	}



}
