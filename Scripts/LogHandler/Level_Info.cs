using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Info : MonoBehaviour {

	public SavedPlayerInfo level;
	public string current_level;

	void Awake()
	{
		level = GameObject.FindObjectOfType<SavedPlayerInfo>();
		level.savedLevel = current_level;

	}

	

}
