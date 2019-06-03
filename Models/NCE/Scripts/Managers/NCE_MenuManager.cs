using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NCE_MenuManager : MonoBehaviour {

	[Header ("Menu Variables")]
	public NCE_NetworkManager NetworkManager; //Reference to our networkmanager
	public InputField IpAdressInputField; 
	public InputField PlayerNameInputField;
	public Text PlayerNameText; //Reference to the bold name text in the main menu

	[Header ("Garage Variables")]
	public GameObject[] Preview_Prefabs;
	public Transform Preview_SpawnPoint;
	public GameObject Current_PreviewPrefab;

	#region Menu
	void OnEnable() { //When is script is enabled
		ReferenceChecks();
		OnMenuLoaded ();
	}

	void ReferenceChecks()
	{
		if (PlayerPrefs.GetString ("PlayerName") == "")
		{
			PlayerNameText.text = "Name not set!"; //Set the bold name text to this string
			PlayerNameInputField.text = "Set a name!"; //Set the inputfield to this string
		}
	}

	public void QuitGame()
	{
		Application.Quit (); //Works only when playing in a standalone build
	}

	#region Network Features
	public void StartupHost()
	{
		if (PlayerPrefs.GetString ("PlayerName") == "") { //If our name doesnt exist or is 0 characters long
			Debug.LogError ("Cant host a game when no name is chosen!"); //Throw a error
			return; //Go out of this function so we dont start a host when having no name
		}
		SetPort();
		NetworkManager.StopClient ();
		NetworkManager.StartHost();
	}

	public void JoinGame()
	{
		if (PlayerPrefs.GetString ("PlayerName") == "") { //If our name doesnt exist or is 0 characters long
			Debug.LogError ("Cant join a game when no name is chosen!"); //Throw a error
			return; //Go out of this function so we dont join a game when having no name
		}
		SetIPAddress();
		SetPort();
		NetworkManager.StartClient();
	}

	void SetIPAddress()
	{
		string ipAddress = IpAdressInputField.text;
		NetworkManager.networkAddress = ipAddress;
	}

	void SetPort()
	{
		NetworkManager.networkPort = 7777; 
	}
	#endregion

	#region Menureferences
	void OnMenuLoaded()
	{
		NetworkManager = GameObject.Find ("NCE_NetworkManager").GetComponent<NCE_NetworkManager>();
		InstatiatePreviewPrefab (PlayerPrefs.GetInt ("ChosenVehicle")); //Spawns our selected vehicle when the menu scene opens
		AssignplayerName ();
	}

	void AssignplayerName()
	{
		PlayerNameText.text = PlayerPrefs.GetString ("PlayerName"); //Set the bold name text to that name
		PlayerNameInputField.text = PlayerPrefs.GetString ("PlayerName"); //Set the text inside the inputfield to that name
	}

	public void OnNameEntered(InputField Input) //When we are done entering our name, save the contents of the inputfield into our playerprefs so they are saved between sessions
	{
		PlayerPrefs.SetString ("PlayerName", Input.text); //Set the PlayerName string to the contents of the playernameinputfield
		PlayerNameText.text = PlayerPrefs.GetString ("PlayerName"); //Get that same playername from the playerprefs and set the bold name text
	}

	#region Garage Function
	public void SelectVehicle(int Selected)
	{
		PlayerPrefs.SetInt ("ChosenVehicle", Selected);
		InstatiatePreviewPrefab (Selected);
	}

	public void InstatiatePreviewPrefab(int SelectedPreview)
	{
		if (Current_PreviewPrefab != null) { //If there is already a preview object
			Destroy (Current_PreviewPrefab); //Destroy this object
		}
		Current_PreviewPrefab = GameObject.Instantiate (Preview_Prefabs [SelectedPreview], Preview_SpawnPoint.position, Preview_SpawnPoint.rotation) as GameObject; //Spawn the desired preview object
	}
	#endregion
	#endregion
	#endregion
}
	
