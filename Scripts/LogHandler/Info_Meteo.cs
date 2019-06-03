using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info_Meteo : MonoBehaviour {

    public string meteo;
    public SavedPlayerInfo savMeteo;

    private void Awake()
    {
        savMeteo = GameObject.FindObjectOfType<SavedPlayerInfo>();
    }

    public void returnJour()
    {
        meteo = "jour";
        savMeteo.savedMeteo = meteo;
    }

    public void returnNuit()
    {
        meteo = "nuit";
        savMeteo.savedMeteo = meteo;
    }

    
}
