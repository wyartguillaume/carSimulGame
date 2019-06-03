using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Token_Collection : MonoBehaviour {

	public int score = 0;
	public Text scoreText;

	private void Update()
	{
		scoreText.text = string.Format("{0}", score);
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Token")
		{
			score++;
			Destroy(other);
		}
	}
}
