using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculNbrTropProche : MonoBehaviour {

    public int nbrFoisProche;
    public Collider other;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            nbrFoisProche++;
        }
    }

    public int ReturnNbrFoisTouche()
    {
        if (nbrFoisProche > 0)
        {
            return nbrFoisProche;
        }
        else return 0;
    }
}
