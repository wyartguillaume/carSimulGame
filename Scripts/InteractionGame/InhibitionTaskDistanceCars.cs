using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InhibitionTaskDistanceCars : MonoBehaviour {

    public GameObject uiTooClose;
    public Transform otherCar;
    private int nbTimeTooClose;

	// Use this for initialization
	void Start () {

        uiTooClose.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        CalculateDistance();
    }

    void CalculateDistance()
    {
        float distance = Vector3.Distance(otherCar.position, transform.position);

        if (distance < 10)
        {
            uiTooClose.SetActive(true);
			//print(distance);

            if (uiTooClose)
            {
                nbTimeTooClose++;   
            }
		}

        else
        {
            uiTooClose.SetActive(false);
        }   
    }

    void OnApplicationQuit()
    {
        Debug.Log("Nombre de fois trop pres de la voiture: " + nbTimeTooClose);
    }
}
