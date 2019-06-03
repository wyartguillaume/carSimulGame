using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Client Rigidbody Sync
/// This script sends client rigidbody force values to the server and smooths out other clients rigidbody movements.
/// </summary>

[NetworkSettings(channel=1,sendInterval=0.02f)]
public class Client_Rigidbody_Sync : NetworkBehaviour 
{
	[SyncVar]public Vector3 syncVelocity; //Used to sync our velocity from the server towards other clients
	[SyncVar]public Vector3 syncAngularVelocity; //Used to sync our angularvelocity from the server towards other clients
	[SerializeField]Rigidbody CarRigidbody; //Our rigidbody
	[SerializeField]private float Smoothingfactor = 20f; //Smoothing factor

	void FixedUpdate()
	{
		AssignVelocity ();
		AssignAngularVelocity ();
		TransmitVelocity();
	}

	void AssignVelocity()
	{
		if(!isLocalPlayer) //If this object isnt our player object apply to following
		{
			CarRigidbody.velocity = Vector3.Lerp(CarRigidbody.velocity, syncVelocity, Time.deltaTime * Smoothingfactor); //Smooths out the remote clients velocity so their object appears smoother in our game
		}
	}

	void AssignAngularVelocity()
	{
		if(!isLocalPlayer) //If this object isnt our player object apply to following
		{
			CarRigidbody.angularVelocity = Vector3.Lerp(CarRigidbody.angularVelocity, syncAngularVelocity, Time.deltaTime * Smoothingfactor); //Smooths out the remote clients angularvelocity so their object appears smoother in our game
		}
	}

	[Command]
	void CmdProvideVelocityToServer(Vector3 vel, Vector3 avel)
	{
		syncVelocity = vel; //Update our velocity on the server and on other players
		syncAngularVelocity = avel; //Update our angularvelocity on the server and on other players
	}

	[ClientCallback]
	void TransmitVelocity()
	{
		if (isLocalPlayer && CarRigidbody != null) //Send this information only if this is our player
		{
			CmdProvideVelocityToServer(CarRigidbody.velocity, CarRigidbody.angularVelocity); //Send our velocity and angularvelocity to the server so they can be used to smooth our rigidbody movement out on other clients instances of the  game
		}
	}
}
