using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoueQuiiteLaroute : MonoBehaviour {

    public int collisionVoitureGauche;
    public int collisionVoitureDroite;
    private float chronoLeftStart = 0f;
    private bool timerDepartLeft = false;
    private float timesLeft;
    private float timeTotLeft;
    private float chronoRightStart = 0f;
    private bool timerDepartRight = false;
    private float timesRight;
    private float timeTotRight;
    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "WheelCollider":
                collisionVoitureGauche++;
                chronoLeftStart = Time.time;
                timerDepartLeft = true;
                break;
            // Debug.Log("En dehors de la route :" +collisionVoitureGauche +" Temps: ");

            case "WheelColliderRight":
                collisionVoitureDroite++;
                chronoRightStart = Time.time;
                timerDepartRight = true;
                // Debug.Log("En dehors de la route :" + collisionVoitureGauche + " Temps: ");
                // Debug.Log("En dehors de la route droite:" + collisionVoitureDroite);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "WheelCollider":

                timerDepartLeft = false;
                timeTotLeft += timesLeft;
                Debug.Log("temps Gauche: " + string.Format("{0:00}:{1:00}:{2:000}", Mathf.Floor(timeTotLeft / 60), timeTotLeft % 60, (timeTotLeft * 1000) % 1000));
                break;

            case "WheelColliderRight":

                timerDepartRight = false;
                timeTotRight += timesRight;
                Debug.Log("temps Droite: " + string.Format("{0:00}:{1:00}:{2:000}", Mathf.Floor(timeTotRight / 60), timeTotRight % 60, (timeTotRight * 1000) % 1000));
                break;

        }
    }

    public int RencontreRouteGauche()
    {
        return collisionVoitureGauche;
    }

    public int RencontreRouteDroite()
    {
        return collisionVoitureDroite;
    }

    public string TimerRight()
    {
        return string.Format("{0:00}:{1:00}:{2:000}", Mathf.Floor(timeTotRight / 60), timeTotRight % 60, (timeTotRight * 1000) % 1000);
    }

    public string TimerLeft()
    {
       return string.Format("{0:00}:{1:00}:{2:000}", Mathf.Floor(timeTotLeft / 60), timeTotLeft % 60, (timeTotLeft * 1000) % 1000);
    }

    private void Update()
    {
        if(timerDepartLeft == true)
        {
            timesLeft = Time.time - chronoLeftStart;
        }

        if(timerDepartRight == true)
        {
            timesRight = Time.time - chronoRightStart;
        }

    }


}
