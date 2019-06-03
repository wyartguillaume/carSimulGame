using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inscription : MonoBehaviour {
    [SerializeField]
    GameObject MainMenu;
    [SerializeField]
    GameObject InscriptionMenu;
    [SerializeField]
    GameObject infosPrincipale;
    private float speed = 1000f;
    [SerializeField]
    GameObject target, infoSupp;
    [SerializeField]
    GameObject ajout;



    public void RetourMenu()
    {
        InscriptionMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void AjoutInfos()
    {
        infosPrincipale.transform.position = Vector3.MoveTowards(infosPrincipale.transform.position, target.transform.position, speed);
        infoSupp.SetActive(true);
        ajout.SetActive(false);

    }


}
