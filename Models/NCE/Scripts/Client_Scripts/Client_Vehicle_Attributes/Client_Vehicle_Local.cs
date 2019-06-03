using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client_Vehicle_Local : MonoBehaviour {

	public Client_Vehicle_Manager Client_Manager;
	public Client_Vehicle_UI Client_UI;
	public GameObject SceneCamera; //Our Camera
	public Transform CameraPoint; //The point on which the camera is going to sit when drving in 3rd person
	public Transform FirstPersonCameraPoint;  //The point on which the camera is going to sit when drving in 1st person
	public Transform CameraAnchor;
	public MouseRotator Mouserotator;
	public CameraFollow Smoothfollow;
	public int CameraStance = 0;
	private int OldCameraStance = 0;
	public bool isAlive = true;
	public bool IsPaused = false;

	[Header("Debug Variables")]
	public bool CanRespawnAfterExplosion = true; //Used to keep vehicle in the blown up state when set to false

	void OnEnable()
	{
		SceneCamera = GameObject.Find("SceneCamera"); //Find our scenecamera and store it in a variable
		Client_UI = GameObject.Find("Client_UI").GetComponent<Client_Vehicle_UI>();
		Client_UI.Client_Manager = GetComponent<Client_Vehicle_Manager> (); //Supply our vehicle_client_manager to the client_UI
		Client_UI.Client_Local = GetComponent<Client_Vehicle_Local> (); //Supply our vehicle_client_local to the client_UI
		CameraStance = 0;
	}
	
	// Update is called once per frame
	void Update () {
		HandlePlayerInput ();
	}

	void HandlePlayerInput()
	{
		#region Camera Stance Input
		if (Input.GetKeyDown (KeyCode.C) && isAlive && IsPaused == false) { //Only switch when alive and not in the pausemenu
			if (CameraStance == 2) {
				CameraStance = 0;
			} else {
				CameraStance++;
			}
		}
		#endregion

		#region PauseMenu Input
		if (Input.GetKeyDown (KeyCode.Escape) && isAlive) {
			IsPaused = !IsPaused; 
			if (IsPaused == true) {
				OldCameraStance = CameraStance; //Store the current camerastance into a placeholder variable
				CameraStance = 5; //Set the camera stance to a paused mode
			} else if (IsPaused == false) {
				CameraStance = OldCameraStance; //Apply our old camerastance before we paused
			}
			Client_UI.PauseMenu (IsPaused); //Call our pause function
		}
		#endregion

		#region PlayerAction Input
		if (Input.GetKeyDown (KeyCode.H) && isAlive) {
			Client_Manager.RequestPlayerActionSyncing ("HeadLights");
		}

		if (Input.GetKeyDown (KeyCode.G) && isAlive) { //If we hold the G key down
			Client_Manager.RequestPlayerActionSyncing ("StartHorn"); //Tell the client_manager to start horning
		}
		else if (Input.GetKeyUp (KeyCode.G) || !isAlive) { //If we release the G key
			Client_Manager.RequestPlayerActionSyncing ("StopHorn"); //Tell the client_manager to stop horning
		}
		#endregion
	}

	void LateUpdate()
	{
		CameraBehaviour ();	
	}

	void CameraBehaviour()
	{
		if (CameraStance == 0) { //Just follow our player
			Mouserotator.enabled = false;
			Smoothfollow.enabled = false;
			CameraAnchor.localEulerAngles = Vector3.zero;
			SceneCamera.transform.position = CameraPoint.position;
			SceneCamera.transform.rotation = CameraPoint.rotation;
			Client_UI.ChangeCameraStanceText ("Chase Cam");
		}
		if (CameraStance == 1) { //Player can move the camera with the mouse
			Mouserotator.enabled = true;
			Smoothfollow.enabled = false;
			SceneCamera.transform.position = CameraPoint.position;
			SceneCamera.transform.rotation = CameraPoint.rotation;
			Client_UI.ChangeCameraStanceText ("Free Look");
		}
		if (CameraStance == 2) { //Firstpersonview
			Mouserotator.enabled = false;
			Smoothfollow.enabled = false;
			SceneCamera.transform.position = FirstPersonCameraPoint.position;
			SceneCamera.transform.rotation = FirstPersonCameraPoint.rotation;
			Client_UI.ChangeCameraStanceText ("First Person");
		}
		if (CameraStance == 4) { //Enabled when player died
			SceneCamera.transform.LookAt(this.transform);
			Client_UI.ChangeCameraStanceText ("");
		}
		if (CameraStance == 5) {
			Mouserotator.enabled = false;
			Smoothfollow.enabled = false;
			CameraAnchor.localEulerAngles = Vector3.zero;
			SceneCamera.transform.position = CameraPoint.position;
			SceneCamera.transform.rotation = CameraPoint.rotation;
			Client_UI.ChangeCameraStanceText ("Game Paused!");
		}
	}
		

	public void OnPlayerDiedLocal() //Add here stuff what needs to be done on the local client when our player is death
	{
		Client_UI.ShowMessageToPlayer ("Respawning..","Center",3f, 100);
		isAlive = false;
		CameraStance = 4;
	}
}
