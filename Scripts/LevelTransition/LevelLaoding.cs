using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLaoding : MonoBehaviour {

	public int sceneNumber;

	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		coroutine = LaodingTime(3.0f);
		StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator LaodingTime(float WaitTime)
	{
	
		yield return new WaitForSeconds(WaitTime);
		print("waiting");
		SceneManager.LoadScene(sceneNumber);
		
	}



}
