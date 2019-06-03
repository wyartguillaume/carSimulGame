using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel=1,sendInterval=0.1f)]
public class Client_Transform_Sync : NetworkBehaviour {

	[SyncVar]private Vector3 syncPos; //Used to sync our position from the server to the other clients
	[SyncVar]private Quaternion syncRot; //Used to sync our rotation from the server to the other clients
	[SerializeField]Transform CarTransform; //Reference to our transform
	[SerializeField]private Vector3 lastpos; //Used to compare our currentposition with a older position, if those differ to much than the treshold allows. Update the syncpos variable. This is so we dont update our position constantly when standing still
	[SerializeField]private Quaternion lastrot; //^ same principle but then for the rotation
	[SerializeField]private float PostionLerpRate = 20f; //Position smoothing factor
	[SerializeField]private float RotationLerpRate = 20f; //Rotation smoothing factor
	private float postreshold = 0.05f;
	private float rottreshold = 0.05f;

	void FixedUpdate()
	{
		TransmitPosition();
		TransmitRotation();
		LerpTransform();
	}

	void LerpTransform()
	{
		if(!isLocalPlayer)
		{
			CarTransform.rotation = Quaternion.Lerp (CarTransform.rotation, syncRot, Time.deltaTime * RotationLerpRate);
			CarTransform.position = Vector3.Lerp(CarTransform.position, syncPos, Time.deltaTime * PostionLerpRate);
		}
	}

	[Command]
	void CmdProvidePositionToServer(Vector3 pos)
	{
		syncPos = pos;
	}

	[ClientCallback]
	void TransmitPosition()
	{
		if (isLocalPlayer)
		{
			if(Vector3.Distance(CarTransform.transform.position, lastpos)>postreshold)
			{
				CmdProvidePositionToServer(CarTransform.position);
				lastpos = CarTransform.position;
			}
		}
	}

	[Command]
	void CmdProvideRotationToServer(Quaternion rot)
	{
		syncRot = rot;
	}

	[ClientCallback]
	void TransmitRotation()
	{
		if (isLocalPlayer)
		{
			if(Quaternion.Angle(CarTransform.transform.rotation, lastrot)>rottreshold)
			{
				CmdProvideRotationToServer(CarTransform.rotation);
				lastrot = CarTransform.rotation;
			}
		}
	}
}
