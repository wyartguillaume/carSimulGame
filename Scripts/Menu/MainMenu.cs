using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour {
    [SerializeField]
    GameObject inscriptionSimple, inscription;
    [SerializeField]
    GameObject mainMenuPsy, mainPatient;
    [SerializeField]
    GameObject inscriptionMenuPat, inscriptionMenuPsy, connexionMenuPsy;
    [SerializeField]
    GameObject connexionMenuPat,infosupp, infPrincipale;
    [SerializeField]
    GameObject ajoutButton;
    [SerializeField]
    GameObject infosPrincipale;
    private float speed = 1000f;
    [SerializeField]
    GameObject infoSupp;



    public void GoInscriptionPatient()
    {
        infPrincipale.SetActive(true);
        mainPatient.SetActive(false);
        inscriptionMenuPat.SetActive(true);
        infosupp.SetActive(false);
        ajoutButton.SetActive(true);
        inscriptionSimple.SetActive(true);
        inscription.SetActive(false);
       // infPrincipale.transform.position = Vector3.MoveTowards(infPrincipale.transform.position, target2.transform.position, 1000f);

    }




    public void GoConnexionPatient()
    {
        mainPatient.SetActive(false);
        connexionMenuPat.SetActive(true);
    }




    public void GoMenuPatient()
    {
        mainPatient.SetActive(true);
        inscriptionMenuPat.SetActive(false);
        connexionMenuPat.SetActive(false);
    }


    //psy

    public void GoInscriptionPsy()
    {
        mainMenuPsy.SetActive(false);
        inscriptionMenuPsy.SetActive(true);
    }

    public void GoConnexionPsy()
    {
        mainMenuPsy.SetActive(false);
        connexionMenuPsy.SetActive(true);
    }

    public void GoMenuPsy()
    {
        mainMenuPsy.SetActive(true);
        mainPatient.SetActive(false);
        connexionMenuPsy.SetActive(false);
        inscriptionMenuPsy.SetActive(false);
    }

    //revenir au menu choix psy ou patient
    /*public void GoMenuChoix()
    {
        SceneManager.LoadScene(0);
    }*/

    public void AjoutInfos()
    {
       // infosPrincipale.transform.position = Vector3.MoveTowards(infosPrincipale.transform.position, target.transform.position, speed);
        infoSupp.SetActive(true);
        ajoutButton.SetActive(false);
        infPrincipale.SetActive(false);
        inscriptionSimple.SetActive(false);
        inscription.SetActive(true);

    }

    public void Quitter()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif  
    }




}
