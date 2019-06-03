using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//This script manages all client function which involve networking
public class Client_Vehicle_Manager : NetworkBehaviour {
	[Header("General")]
	public NCE_NetworkManager networkManager; //Reference to our networkmanager
	[SerializeField]private Client_Vehicle_Local Client_Local; //Reference to our Client_Local
	[SerializeField]private CarUserControl_Unet Client_Controls;
	[SerializeField]private CarController_Unet CarController;
	public CarAudio_Unet Car_Audio;
	public Rigidbody Vehicle_Rigidbody; //Our vehicle rigidbody

	[Header("Name Variables")]
	[SyncVar(hook="OnNameChange")]public string syncPlayerName; //This makes sure the Playername is synced from the server towards all clients. When a name change occur, the function OnNameChange will be called
	public Text PlayerNamePlate; //Reference to our player's name plate

	[Header("Damage Variables")]
	[SyncVar(hook="OnDmgStateChange")]public int syncPlayerDmgState = 3; //This makes sure the Player's damage state is synced from the server towards all clients. When a state change occur, the function OnDmgStateChange will be called
	[SerializeField]private int PlayerHealth = 100; //Internal health for determining the damage state, this is not synced to other players
	public GameObject[] DamageEffects; //Array with our damage fx's which sit on our vehicle prefab. 0 = smoke, 1 = fire, 2 = explosion, please assign these in this exact order
	public GameObject[] DisableOnDeath; //Array for everything what has to be disabled when we explode
	public GameObject NormalCarChassis; //Normal car chassis model
	public GameObject DestroyedCarChassis; //Destroyed car chassis model
	public AudioClip ExplosionSoundEffect; //Explosion soundeffect clip
	public AudioSource VehicleExplosion_AudioSource; //AudioSource for explosions

	[Header("Player Action Variables")]
	[SyncVar(hook="OnPlayerAction")]public string syncPlayerAction = "[HL]0,[Horn]0,[EA]0"; //All player action in 1 string, so we dont need a syncvar for each action 
	private int HeadlightInt = 0; //Local int for determining if the headlights are on or off
	private int HornInt = 0; //local int for determining if we are honking or not
	private int EmptyInt = 0; //local int to add a playeraction for
	public GameObject[] HeadLights; //Reference to both headlights
	public AudioSource VehicleHorn_AudioSource; //AudioSource for horn
	public AudioClip HornSoundEffect; //Horn soundeffect clip

	//Datatranslator
	private static string HEADLIGHT_SYMBOL = "[HL]";
	private static string HORN_SYMBOL = "[Horn]";
	private static string EmptyAction_SYMBOL = "[EA]";

	#region Client Start functions
	public override void OnStartClient()
	{
		OnNameChange (syncPlayerName); //Calling the hook function so it updates itself when our name changes. If we dont do this, new clients wont receive our name.
		OnDmgStateChange(syncPlayerDmgState); //Calling the hook function so it updates itself when our dmgstate changes. If we dont do this, new clients wont receive our dmgstate value
		OnPlayerAction(syncPlayerAction); //Calling the hook function so it updates itself when a playeraction changes. If we dont do this, new clients wont receive our which actions are currently active
	}

	public override void OnStartLocalPlayer()
	{
		base.OnStartLocalPlayer (); //Applies the default implentation of the OnStartLocalPlayer function 
		syncPlayerName = PlayerPrefs.GetString ("PlayerName"); //Apply our name on the client, which we chose in the main menu
		Client_Local.enabled = true; //Local functions have only to be applied to our local client
		CmdSendServerMyName(syncPlayerName); //Get our playername we set in the main menu and send it to the server for syncing
		//PlayerNamePlate.gameObject.SetActive(false); //Disable our name plate in our instance of the game
		Client_Controls.enabled = true; //Enable the controls on our game instance
	}

	public void OnDisconnectPlayer() 
	{
		DisableControls ();
		NetworkManager.singleton.StopHost ();
	}
	#endregion

	#region Player Name Functions
	[Command]
	void CmdSendServerMyName(string MyName)
	{
		syncPlayerName = MyName; //The syncvar is now set for us on the server, so other clients also get the information that this is our name
	}

	public void OnNameChange(string newNameValue)
	{
		syncPlayerName = newNameValue; //Update the syncPlayerName variable on all clients
		PlayerNamePlate.text = syncPlayerName;  //Change the name plate text to our name
	} 
	#endregion

	#region Respawning and Vehicle Damage
	[Command]
	void CmdSendServerMyNewHealth(int NewDamageState)
	{
		syncPlayerDmgState = NewDamageState; //The syncvar is now set for us on the server, so other clients also get the information that this is our current damage state
	}

	public void OnDmgStateChange(int newDmgStateValue)
	{
		syncPlayerDmgState = newDmgStateValue; //Update the syncPlayerDmgState variable on all clients
		SetEffectForDamageState(newDmgStateValue); //Set the effect according to the damage state
	}

	private void OnCollisionEnter(Collision thisCollision)
	{
		if (hasAuthority) {
			PlayerHealth -= Mathf.RoundToInt(thisCollision.relativeVelocity.magnitude); //Converts the impactforce to a int
			if(PlayerHealth <= 40 && syncPlayerDmgState > 2) 
			{
				CmdSendServerMyNewHealth(2);
			}
			if(PlayerHealth <= 10 && syncPlayerDmgState != 1 && syncPlayerDmgState != 0)
			{
				CmdSendServerMyNewHealth(1);
				StartCoroutine (WaitForExplosion ());
			}
		}

	}

	private IEnumerator WaitForExplosion()
	{
		if (hasAuthority) {
			yield return new WaitForSeconds (5f);
			StartCoroutine(ExplodeVehicle ());
		}
	}


	IEnumerator ExplodeVehicle()
	{
		CmdSendServerMyNewHealth(0); //Burning time is up. Explode this vehicle!
		DisableControls();
		Client_Local.OnPlayerDiedLocal ();
		if (Client_Local.CanRespawnAfterExplosion == true) {
			yield return new WaitForSeconds (3f);
			int SelectedVehicleInt = PlayerPrefs.GetInt ("ChosenVehicle");
			CmdTellServerToRespawn (SelectedVehicleInt);
		}
	}

	public void RespawnVehicle()
	{
		if (hasAuthority) {
			StartCoroutine(ExplodeVehicle ());
		}
	}

	public void DisableControls()
	{
		CarController.Move(0f,0f,0f,0f); //Disable any further input
		CarController.enabled = false; 
		Client_Controls.enabled = false; //Disable our controls
	}

	public void SetEffectForDamageState(int stage)
	{
		if (stage == 3) {
			DamageEffects [0].SetActive (false); //Dont emit smoke
			DamageEffects [1].SetActive (false); //Dont emit fire
			DamageEffects [2].SetActive (false); //Dont emit explosion
		}
		if (stage == 2) {
			DamageEffects [0].SetActive (true); //Emit smoke
			DamageEffects [1].SetActive (false); //Dont emit fire
			DamageEffects [2].SetActive (false); //Dont emit explosion
		}
		if (stage == 1) {
			DamageEffects [0].SetActive (false); //Dont emit smoke
			DamageEffects [1].SetActive (true); //Emit fire
			DamageEffects [2].SetActive (false); //Dont emit explosion
		}
		if (stage == 0) {
			DamageEffects [0].SetActive (false); //Dont emit smoke
			DamageEffects [1].SetActive (true); //Emit fire
			DamageEffects [2].SetActive (true); //Emit explosion
			//These lines define what happens on other clients when someone explodes
			foreach (GameObject disableObject in DisableOnDeath) { //Disable all objects which need to be disabled on all clients
				disableObject.SetActive (false);
			}
			if (DestroyedCarChassis != null) {
				NormalCarChassis.SetActive (false);
				DestroyedCarChassis.SetActive (true);
			}
			Car_Audio.StopSound (); //Stop all engine, skidmark soundsources
			Car_Audio.enabled = false; //Disable the audio script so the sounds wont enable themselves again
			AudioClip ExplosionClip = ExplosionSoundEffect;
			VehicleExplosion_AudioSource.PlayOneShot(ExplosionClip);
			Vehicle_Rigidbody.drag = 2;
			Vehicle_Rigidbody.angularVelocity = new Vector3 (-3, 1, 1);
		}
	}

	[Command]
	void CmdTellServerToRespawn(int SelectVehicle)
	{
		NCE_NetworkManager networkMan = GameObject.Find("NCE_NetworkManager").GetComponent<NCE_NetworkManager>(); //Get a reference of the nce_networkmanager on the server
		Transform SpawnPoint = NetworkManager.singleton.GetStartPosition (); //Get a random spawnpoint for respawning
		//int RandomPrefabInt = Random.Range (0, networkMan.PlayerPrefabs.Count); //Get a random vehicle from our playerprefabs list
		GameObject PlayerObject = Instantiate (networkMan.PlayerPrefabs[SelectVehicle], SpawnPoint.position, SpawnPoint.rotation) as GameObject; //Instantiate the new playerobject
		NetworkServer.DestroyPlayersForConnection(this.connectionToClient); //Destroys the playerobject for our connectio
		NetworkServer.ReplacePlayerForConnection (this.connectionToClient, PlayerObject, this.playerControllerId); //Set the newly spawned object as ours and give the authority over it on the network
	}
	#endregion

	#region Player Actions
	public void RequestPlayerActionSyncing(string ActionName)
	{
		if (ActionName == "HeadLights") {
			if (HeadlightInt == 0 && hasAuthority) {
				HeadlightInt = 1;
				CmdSendServerMyPlayerAction (ValuesToData (HeadlightInt, HornInt, EmptyInt));
			}
			else if (HeadlightInt == 1 && hasAuthority) {
				HeadlightInt = 0;
				CmdSendServerMyPlayerAction (ValuesToData (HeadlightInt, HornInt, EmptyInt));
			}
		}

		if (ActionName == "StartHorn") 
		{
			if (hasAuthority) {
				HornInt = 1;
				CmdSendServerMyPlayerAction (ValuesToData (HeadlightInt, HornInt, EmptyInt));
			}
		}
		if (ActionName == "StopHorn") 
		{
			if (hasAuthority) {
				HornInt = 0;
				CmdSendServerMyPlayerAction (ValuesToData (HeadlightInt, HornInt, EmptyInt));
			}
		}
	}

	[Command]
	void CmdSendServerMyPlayerAction(string syncAction)
	{
		syncPlayerAction = syncAction;
	}

	public void OnPlayerAction(string newAction)
	{
		syncPlayerAction = newAction;
		ApplyPlayerActions (newAction);
	}

	public void ApplyPlayerActions(string Action)
	{
		int HLInt = DataToHeadLights (Action);
		if (HLInt == 0) { //If our headlights are off
			foreach (GameObject HL in HeadLights) { //Get every headlight from the headlights array
				HL.SetActive (false); //Set them as inactive
			}
		}
		else if (HLInt == 1) { //If our headlights are on
			foreach (GameObject HL in HeadLights) {  //Get every headlight from the headlights array
				HL.SetActive (true); //Set them as active
			}
		}
		int HORNInt = DataToHorn (Action);
		if (HORNInt == 1) { //If we have the G key pressed
			if (!VehicleHorn_AudioSource.isPlaying) { //Check if we arent already playing the horn soundeffect
				VehicleHorn_AudioSource.loop = true; //Loop this soundeffect when we are holding the G key
				VehicleHorn_AudioSource.Play (); //Play this soundeffect
			}
		}
		else if (HORNInt == 0) { //If we dont have the G key pressed
			VehicleHorn_AudioSource.Stop (); //Stop playing this soundeffect 
		}
	}

	#region DataTranslator
	public static string ValuesToData(int HL, int Horn, int EmptyAction)
	{
		return HEADLIGHT_SYMBOL + HL + "," + HORN_SYMBOL + Horn + "," + EmptyAction_SYMBOL + EmptyAction; //Makes the seperate int into the correct format for the syncplayeraction string
	}

	public static int DataToHeadLights(string data)
	{
		return int.Parse (DataToValue (data, HEADLIGHT_SYMBOL)); //Receive our headlight state int from the syncplayeraction string
	}

	public static int DataToHorn(string data)
	{
		return int.Parse (DataToValue (data, HORN_SYMBOL)); //Receive our horn state int from the syncplayeraction string
	}

	public static int DataToEmpty(string data)
	{
		return int.Parse (DataToValue (data, EmptyAction_SYMBOL)); //Receive our emptyaction int from the syncplayeraction string
	}

	private static string DataToValue(string data, string symbol) //Extracts the int from the data string (Dont modify this function if you dont know what you are doing!)
	{
		string[] datapieces = data.Split(new string[] { "," }, System.StringSplitOptions.None); //Cuts the string in pieces at the comma's
		foreach (string piece in datapieces) { //Check for each string piece if it has the symbol piece in it
			if (piece.StartsWith (symbol)) { //If it has found the piece, extract the number from it
				return piece.Substring (symbol.Length); //Removes the symbol part of the string piece so we have only a number left (still in string format tho)
			}
		}
		Debug.LogError ("Error retrieving data!");
		return "";
	}

	#endregion

	#endregion
}
