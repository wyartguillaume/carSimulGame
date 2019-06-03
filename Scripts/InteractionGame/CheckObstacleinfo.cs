using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObstacleinfo : MonoBehaviour {

	public List<string> ObstInfo = new List<string>();
	public List<string> ObstBubInfo = new List<string>();


	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Bubble")
		{
			ObstBubInfo.Add(other.transform.parent.name );
			Destroy(other);
		}
		if (other.tag == "Obstacle")
		{
			ObstInfo.Add(other.transform.name);
		}
	}

	public string ObstacleBubbleHitMiss()
	{
		string obstacle = "";
		foreach (string str in ObstBubInfo)
		{
			obstacle = str + obstacle;
		}
		return obstacle;
	}

	public string ObstacleHitMiss()
	{
		string obstacle = "";
		foreach (string str in ObstInfo)
		{
			obstacle = str + obstacle;
		}
		return obstacle;
	}





}
