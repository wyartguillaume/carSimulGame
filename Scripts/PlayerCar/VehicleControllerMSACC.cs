using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]
public class VehicleControllerMSACC : MonoBehaviour {

	public WheelCollider rightFrontWheelCollider;
	public Transform rightFrontWheelMesh;
	[Space(5)]
	public WheelCollider leftFrontWheelCollider;
	public Transform leftFrontWheelMesh;
	[Space(5)]
	public WheelCollider rightRearWheelCollider;
	public Transform rightRearWheelMesh;
	[Space(5)]
	public WheelCollider leftRearWheelCollider;
	public Transform leftRearWheelMesh;
	[Space(30)][Range(0.2f,1.5f)]
	public float torqueForceWheel = 1.0f;
	//public Transform centerOfMass;

	public GUIStyle customStyle;
	//public GUIStyle boxStyle;

	public Rigidbody rb;
	public float motorTorque = 0.0f;
	public float brakeTorque = 0.0f;
	public float KMh;
	float angle = 0.0f;
	float direction = 0.0f;
	bool handBrake;
    float MaxSpeed = 200f;


	public List<float> SpeedList = new List<float>();
	public List<float> SpeedListOZ = new List<float>();

	public float sum = 0;
	public float average = 0;

	public float sumOZ = 0;
	public float averageOZ = 0;

	public bool isObstacleZone;
	


	private ActivateObstacle obstacleTag;

	public GameObject wheel;

	

	

	void Start () {
		rb = GetComponent<Rigidbody> ();
		isObstacleZone = false;

		obstacleTag = GameObject.FindObjectOfType<ActivateObstacle>();

		if (rb.mass < 1000.0f) {
			rb.mass = 1000.0f;
		}
		rb.interpolation = RigidbodyInterpolation.Interpolate;
		rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		/*if (centerOfMass != null) {
			rb.centerOfMass = transform.InverseTransformPoint(centerOfMass.position);
		} else {
			rb.centerOfMass = Vector3.zero;
		}*/
		if (rightFrontWheelCollider != null && leftFrontWheelCollider != null && rightRearWheelCollider != null && leftRearWheelCollider != null) {
			WheelCollider ColisoresDasRodas = GetComponentInChildren<WheelCollider>();
			ColisoresDasRodas.ConfigureVehicleSubsteps(1000.0f, 20, 20);
		}
   
            //reglage centerOfMass
            GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -0.9f, 0.2f);


    }

	void Update () {
        //son du moteur
        float Val_pitch = KMh / MaxSpeed + 1f;
        GetComponent<AudioSource>().pitch = Mathf.Clamp(Val_pitch, 1f, 3f); //stopper son a 3f
        KMh = rb.velocity.magnitude * 3.6f;
		rb.drag = Mathf.Clamp ((KMh / 250.0f) * 0.075f, 0.001f, 0.075f);

		direction = CrossPlatformInputManager.GetAxis("Horizontal");
		if (Mathf.Abs (direction) > 0.7f) {
			angle = Mathf.Lerp (angle, direction, Time.deltaTime * 4);


		} else {
			angle = Mathf.Lerp (angle, direction, Time.deltaTime * 2);
		}

		if (Mathf.Abs (CrossPlatformInputManager.GetAxis("Vertical")) < 0.1f) {
			motorTorque = 0.0f;
			brakeTorque = Mathf.Lerp (brakeTorque, rb.mass, Time.deltaTime * 2.0f);
		} else {
			motorTorque = Mathf.Lerp (motorTorque, CrossPlatformInputManager.GetAxis ("Vertical") * rb.mass * torqueForceWheel, Time.deltaTime);
			brakeTorque = 0.0f;
		}
		if (CrossPlatformInputManager.GetButton("Jump")) {
			handBrake = true;
           
		} else {
			handBrake = false;
		}
		if (handBrake) {
			brakeTorque = float.MaxValue;
			motorTorque = 0.0f;
		}

		if (rightFrontWheelCollider != null && leftFrontWheelCollider != null && rightRearWheelCollider != null && leftRearWheelCollider != null) {
			ApplyTorque (motorTorque);
			ApplyBrakes (brakeTorque);
		}

		

	}

	void OnGUI()
	{
		//GUI.Box(new Rect(60, 10, 250, 150), "Speed", boxStyle);
		GUI.Label(new Rect(((Screen.width/2)+15), ((Screen.height / 3) *2.5f), 600, 600), (Mathf.RoundToInt(KMh)) + "KM/h", customStyle);
	}

	public float returnAverageSpeed()
	{

		foreach (float kmh in SpeedList)
		{
			sum += (float)kmh;
		}
		average = sum / SpeedList.Count;

		return average;
	}

	public float returnAverageSpeedOZ()
	{
		foreach (float kmh in SpeedListOZ)
		{
			sumOZ += (float)kmh;
		}
		averageOZ = sumOZ / SpeedListOZ.Count;
		return averageOZ;
	}

	public void OnApplicationQuit()
	{

		//foreach (float kmh in SpeedList)
		//{
		//	sum += (float)kmh;
		//}
		//average = sum / SpeedList.Count;
		returnAverageSpeed();
		print(average + "av of speed");


		//foreach (float kmh in SpeedListOZ)
		//{
		//	sumOZ += (float)kmh;
		//}
		//averageOZ = sumOZ / SpeedListOZ.Count;
		returnAverageSpeedOZ();
		print(averageOZ + "avOZ of speed");

	}

	void FixedUpdate(){
		if (rightFrontWheelCollider != null && leftFrontWheelCollider != null && rightRearWheelCollider != null && leftRearWheelCollider != null) {
			DownForce ();
			StabilizeVehicle ();
			MeshUpdate ();
		}
		if (Mathf.Abs (direction) < 0.9f) {
			Vector3 vel = new Vector3 (rb.angularVelocity.x, 0.0f, rb.angularVelocity.z);
			rb.angularVelocity = Vector3.Lerp (rb.angularVelocity, vel, Time.deltaTime * 2.0f);
		}

		if (isObstacleZone == false)
		{
			SpeedList.Add(KMh);
		}

		if (isObstacleZone == true)
		{
			SpeedListOZ.Add(KMh);
		}	
		
	}
	
	void MeshUpdate(){
		rightFrontWheelCollider.steerAngle = angle * 30;
		leftFrontWheelCollider.steerAngle = angle * 30;
		WheelMeshUpdate (rightFrontWheelCollider, rightFrontWheelMesh);
		WheelMeshUpdate (leftFrontWheelCollider, leftFrontWheelMesh);
		WheelMeshUpdate (rightRearWheelCollider, rightRearWheelMesh);
		WheelMeshUpdate (leftRearWheelCollider, leftRearWheelMesh);
	}

	void WheelMeshUpdate(WheelCollider collider, Transform wheelMesh){
		Quaternion quat;
		Vector3 pos;
		collider.GetWorldPose (out pos, out quat);
		wheelMesh.position = pos;
		wheelMesh.rotation = quat;
	}

	void DownForce(){
		bool ground1 = rightFrontWheelCollider.isGrounded;
		bool ground2 = leftFrontWheelCollider.isGrounded;
		bool ground3 = rightRearWheelCollider.isGrounded;
		bool ground4 = leftRearWheelCollider.isGrounded;
		if ((ground1 && ground2) || (ground3 && ground4)) {
			float ajusteInclinacao = Mathf.Clamp(Mathf.Abs(Vector3.Dot (Vector3.up, transform.up)),0.3f,1.0f);
			rb.AddForce (-transform.up * (rb.mass*2.0f*ajusteInclinacao + (0.8f * ajusteInclinacao * Mathf.Abs(KMh*3.0f) * (rb.mass/125.0f))));
		}
	}

	void StabilizeVehicle(){
		float forceFrontLeft = 1.0f;
		float forceFrontRight = 1.0f;
		float forceReadLeft = 1.0f;
		float forceReadRight = 1.0f;
		WheelHit hit;
		bool isGround1 = leftRearWheelCollider.GetGroundHit(out hit);
		if (isGround1) {
			forceReadLeft = (-leftRearWheelCollider.transform.InverseTransformPoint (hit.point).y - leftRearWheelCollider.radius) / leftRearWheelCollider.suspensionDistance;
		}
		bool isGround2 = rightRearWheelCollider.GetGroundHit(out hit);
		if (isGround2) {
			forceReadRight = (-rightRearWheelCollider.transform.InverseTransformPoint (hit.point).y - rightRearWheelCollider.radius) / rightRearWheelCollider.suspensionDistance;
		}
		bool isGround3 = leftFrontWheelCollider.GetGroundHit(out hit);
		if (isGround3) {
			forceFrontLeft = (-leftFrontWheelCollider.transform.InverseTransformPoint (hit.point).y - leftFrontWheelCollider.radius) / leftFrontWheelCollider.suspensionDistance;
		}
		bool isGround4 = rightFrontWheelCollider.GetGroundHit(out hit);
		if (isGround4) {
			forceFrontRight = (-rightFrontWheelCollider.transform.InverseTransformPoint (hit.point).y - rightFrontWheelCollider.radius) / rightFrontWheelCollider.suspensionDistance;
		}

		float tiltAjustment = Mathf.Clamp(Mathf.Abs(Vector3.Dot (Vector3.up, transform.up)),0.3f,1.0f);
		float antiRollForce1 = (forceReadLeft - forceReadRight) * rb.mass * tiltAjustment;
		float antiRollForce2 = (forceFrontLeft - forceFrontRight) * rb.mass * tiltAjustment;
		if (isGround1) {
			rb.AddForceAtPosition (leftRearWheelCollider.transform.up * -antiRollForce1, leftRearWheelCollider.transform.position); 
		}
		if (isGround2) {
			rb.AddForceAtPosition (rightRearWheelCollider.transform.up * antiRollForce1, rightRearWheelCollider.transform.position); 
		}
		if (isGround3) {
			rb.AddForceAtPosition (leftFrontWheelCollider.transform.up * -antiRollForce2, leftFrontWheelCollider.transform.position); 
		}
		if (isGround4) {
			rb.AddForceAtPosition (rightFrontWheelCollider.transform.up * antiRollForce2, rightFrontWheelCollider.transform.position); 
		}
	}

	void ApplyTorque(float torqueForce){
		rightFrontWheelCollider.motorTorque = torqueForce;
		leftFrontWheelCollider.motorTorque = torqueForce;
		rightRearWheelCollider.motorTorque = torqueForce;
		leftRearWheelCollider.motorTorque = torqueForce;
	}

	void ApplyBrakes(float brakeForce){
		rightFrontWheelCollider.brakeTorque = brakeForce;
		leftFrontWheelCollider.brakeTorque = brakeForce;
		rightRearWheelCollider.brakeTorque = brakeForce;
		leftRearWheelCollider.brakeTorque = brakeForce;
	}
}
