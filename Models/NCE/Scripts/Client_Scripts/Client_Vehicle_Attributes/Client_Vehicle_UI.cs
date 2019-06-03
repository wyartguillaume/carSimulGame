using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client_Vehicle_UI : MonoBehaviour {

	public Client_Vehicle_Manager Client_Manager;
	public Client_Vehicle_Local Client_Local;
	public GameObject PauseMenuPanel; //Reference to our pausemenupanel
	public Text CameraStanceIntText; //Reference to our camerastanceinttextobject;
	public Text CenterMessageText;
	public Text TopCenterMessageText;

	#region Pause Menu
	public void PauseMenu(bool active)
	{
		PauseMenuPanel.SetActive (active); //Close or open our pausemenu when this is called
	}

	public void DisconnectFromGame()
	{
		Client_Manager.OnDisconnectPlayer ();
	}

	public void OnRespawnPlayer()
	{
		Client_Manager.RespawnVehicle ();
		Client_Local.IsPaused = false;
		PauseMenu (false);
	}
	#endregion

	#region Camera Stance
	public void ChangeCameraStanceText(string TextToChange)
	{
		CameraStanceIntText.text = TextToChange;
	}
	#endregion

	#region Game Messages
	public void ShowMessageToPlayer(string Message, string location, float duration, int FontSize)
	{
		if (location == "Center") {
			CenterMessageText.text = Message;
			CenterMessageText.fontSize = FontSize;
			StartCoroutine (ClearMessage (CenterMessageText,duration));
		}
		if (location == "TopCenter") {
			TopCenterMessageText.text = Message;
			TopCenterMessageText.fontSize = FontSize;
			StartCoroutine (ClearMessage (TopCenterMessageText,duration));
		}
	}

	IEnumerator ClearMessage(Text MessageTextToClear, float duration)
	{
		yield return new WaitForSeconds (duration);
		MessageTextToClear.text = "";
	}
	#endregion
}
