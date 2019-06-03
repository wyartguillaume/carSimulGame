using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenKeyBoard : MonoBehaviour {

	TouchScreenKeyboard keyboard;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void openKeyboard()
	{
		keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
		print("touched");
	}

	public void printsomething()
	{
		print("touched printsomfing");
	}
}
