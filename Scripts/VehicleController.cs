using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour {

    public WheelCollider front_left, front_right, back_left, back_right;
    public float Torque = 10000;
    public float Speed;
    public float MaxSpeed = 200f;
    public int Brake = 10000;
    public float CoefAccelaration = 10f;
    public float WheelAngleMax = 10f;
    public bool freinage = false;

    private void Start()
    {
        //reglage centerOfMass
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -0.9f, 0.2f);
    }

    private void Update()
    {
        //son du moteur
        float Val_pitch = Speed / MaxSpeed + 1f;
        GetComponent<AudioSource>().pitch = Mathf.Clamp(Val_pitch, 1f, 3f); //stopper son a 3f

        Speed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        Debug.Log((int)Speed);
        //Acceleration
        if (Input.GetKey(KeyCode.UpArrow) && Speed < MaxSpeed && !freinage)
        {
            front_left.brakeTorque = 0;
            front_right.brakeTorque = 0;
            back_left.brakeTorque = 0;
            back_right.brakeTorque = 0;
            back_left.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAccelaration * Time.deltaTime;
            back_right.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAccelaration * Time.deltaTime;
        }

        if (!Input.GetKey(KeyCode.UpArrow) || Speed > MaxSpeed && !freinage)
        {
            back_left.motorTorque = 0;
            back_right.motorTorque = 0;
            back_left.brakeTorque = Brake * CoefAccelaration * Time.deltaTime;
            back_right.brakeTorque = Brake * CoefAccelaration * Time.deltaTime;
        }

        //Direction du véhicule
        front_left.steerAngle = Input.GetAxis("Horizontal") * WheelAngleMax;
        front_right.steerAngle = Input.GetAxis("Horizontal") * WheelAngleMax;

        //Freinage

        if (Input.GetKey(KeyCode.Space))
        {
            freinage = true;
            back_left.brakeTorque = Mathf.Infinity;
            back_right.brakeTorque = Mathf.Infinity;
            front_left.brakeTorque = Mathf.Infinity;
            front_right.brakeTorque = Mathf.Infinity;
            back_left.motorTorque = 0;
            back_right.motorTorque = 0;

        }
        else
        {
            freinage = false;
        }

        //Marche Arriere
        if (Input.GetKey(KeyCode.DownArrow))
        {
            front_left.brakeTorque = 0;
            front_right.brakeTorque = 0;
            back_left.brakeTorque = 0;
            back_right.brakeTorque = 0;
            back_left.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAccelaration * Time.deltaTime;
            back_right.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAccelaration * Time.deltaTime;
        }
    }
}
