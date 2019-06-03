using System;
using UnityEngine;

[RequireComponent(typeof (CarController_Unet))]
public class CarUserControl_Unet : MonoBehaviour
{
	private CarController_Unet m_Car; // the car controller we want to use

    private void Awake()
    {
		m_Car = GetComponent<CarController_Unet>();  // get the car controller
    }


    private void FixedUpdate()
    {
        // pass the input to the car!
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		float handbrake = Input.GetAxis("Jump");
		if (Input.GetKeyDown (KeyCode.R)) {
			m_Car.ResetCar ();
		}
           m_Car.Move(h, v, v, handbrake);
       }
}
