using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NCE_NetworkManager : NetworkManager {
	public List<GameObject> PlayerPrefabs = new List<GameObject> ();


	public override void OnClientConnect(NetworkConnection conn)
	{
		//base.OnClientConnect(conn); //Commented this out otherwise we try to setup another connection which we already have
	}

	public override void OnClientSceneChanged(NetworkConnection conn)
	{
		ChosenVehicleMessage message = new ChosenVehicleMessage(); //Create a new ChosenVehicleMessage
		message.ChosenVehicle = PlayerPrefs.GetInt ("ChosenVehicle"); //Change the int in the chosenvehicle message to the int we set in the main menu
		ClientScene.AddPlayer(conn, 0, message); //Tell the server to add our player object with the chosenvehicle message included
	}


	public class ChosenVehicleMessage : MessageBase {
		public int ChosenVehicle;
	}
		
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader ChosenVehicleReader)
	{
		ChosenVehicleMessage VehicleMessage = ChosenVehicleReader.ReadMessage<ChosenVehicleMessage>(); //Read the ChosenVehicleMessage
		int ChosenVehicleInt = VehicleMessage.ChosenVehicle; //Store the int from the chosenvehicle message in a variable
		//int RandomPrefabInt = Random.Range (0, PlayerPrefabs.Count);
		Transform Spawn = NCE_NetworkManager.singleton.GetStartPosition (); //Get a random spawnpoint for our vehicle
		GameObject player = Instantiate(PlayerPrefabs[ChosenVehicleInt], Spawn.transform.position, Spawn.transform.rotation) as GameObject;
		NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
	}
}
