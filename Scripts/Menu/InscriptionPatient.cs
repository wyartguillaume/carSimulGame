using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class InscriptionPatient : MonoBehaviour
{

    public TMP_InputField prenom, nom, pseudo;
    public TMP_InputField mail;
    public TMP_InputField dateDeNaissance, nbrEnfants;
    public Toggle homme;
    public Toggle femme;
    public TMP_InputField profession;
    public TMP_Dropdown status, lateralite, groupe, psychologue;
    private string etatCivil = "";
    private string lateral;
    private string groupeChoix;
    private int sexe;
    private int nbrVisite;
    private DateTime now;
    private int enfant;
    private int psyId;
    private string dateCreation;
    private int index;
    private string nomPsy, prenomPsy;





    public void CreateUser()
    {

        StartCoroutine(InscrptionPatient());
    }

    IEnumerator InscrptionPatient()
    {

        WWWForm form = new WWWForm();
        form.AddField("dateDeNaissancePost", dateDeNaissance.text);
        form.AddField("pseudoPost", pseudo.text);
        form.AddField("sexePost", sexe);
        form.AddField("lateralitePost", lateral);
        form.AddField("groupPost", groupeChoix);
        form.AddField("nomPost", nom.text);
        form.AddField("prenomPost", prenom.text);
        form.AddField("emailPost", mail.text);
        form.AddField("professionPost", profession.text);
        form.AddField("etatCivilPost", etatCivil);
        form.AddField("nbrEnfantPost", nbrEnfants.text);
        using (UnityWebRequest www = UnityWebRequest.Post("https://cognitivedrive.be/gameInsPat/" + psychologue.value, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log("Ajout user");
                SceneManager.LoadScene(1);
            }
        }
    }

    public void Selection()
    {
        switch (status.value)
        {
            case 0:
                etatCivil = "Célibataire";
                break;
            case 1:
                etatCivil = "Marié";
                break;
            case 2:
                etatCivil = "Veuf";
                break;
        }

        switch (lateralite.value)
        {
            case 0:
                lateral = "Main droite";
                break;
            case 1:
                lateral = "Main gauche";
                break;
            case 2:
                lateral = "ambidextre";
                break;

            case 3:
                lateral = "Je ne sais pas";
                break;
        }

        switch (groupe.value)
        {
            case 0:
                groupeChoix = "Depressif";
                break;
            case 1:
                groupeChoix = "Alcoolique";
                break;
            case 2:
                groupeChoix = "Je ne sais pas";
                break;
        }
    }

    public void Toggle()
    {
        if (homme.isOn)
        {
            sexe = 0;
        }
        if (femme.isOn)
        {
            sexe = 1;
        }
    }
}
