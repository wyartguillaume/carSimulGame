using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class DataBaseManager : MonoBehaviour
{

    MySqlConnection con;
    //ConnexionPatient
    public TMP_InputField pseudoLog;

    //InscriptionPatient
    public TMP_InputField prenom, nom, pseudoIns;
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
    private int enfant = 0;
    private int psyId = 1;
    private string dateCreation;
    private int index;
    private string nomPsy, prenomPsy;


    [SerializeField]
    GameObject InscriptionMenuPatient, ConnectionMenuPatient, InscriptionMenuPsy, ConnexionMenuPsy, mainMenuPatient, mainMenuPsycho;
  



    public void Start()
    {
        //nbrEnfants.text = enfant.ToString();
    }

    private string CreationDate()
    {
        now = DateTime.Today;
        dateCreation = now.ToString("yyyy-MM-dd");
        return dateCreation;
    }

    public void ConnectBDD()
    {
        string constr = "Server=mysql-maestro700.alwaysdata.net;DATABASE=maestro700_bdd;USER ID=179260;Password=Maestro4005;Pooling=true;Charset=utf8;";

        try
        {
            con = new MySqlConnection(constr);
            con.Open();
            Debug.Log(con.State.ToString());
        }
        catch (IOException Ex)
        {
            Debug.Log(Ex.ToString());
        }

    }

    private void OnApplicationQuit()
    {
        Debug.Log("Connexion Base de donnée fermée");
        if (con != null && con.State.ToString() != "Closed")
        {
            con.Close();
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


    public void InscriptionPlayer()
    {
        ConnectBDD();
        bool Exist = false;

        //Verification existence email
        MySqlCommand commandsql = new MySqlCommand("SELECT pseudo FROM patient WHERE pseudo='" + pseudoIns.text + "'", con);
        MySqlDataReader MyReader = commandsql.ExecuteReader();

        while (MyReader.Read())
        {
            if (MyReader["pseudo"].ToString() != "")
            {
                Debug.Log("email existe déja");
                Exist = true;
            }
        }
        MyReader.Close();

        if (!Exist)
        {
            string command = "INSERT INTO patient VALUES (default,"+psyId+",'" + pseudoIns.text +
                                                    "','" + dateDeNaissance.text +
                                                    "','" + sexe +
                                                    "','" + lateral +
                                                    "','" + groupeChoix +
                                                    "','" + nom.text +
                                                    "','" + prenom.text +
                                                    "','" + mail.text +
                                                    "','" + profession.text +
                                                    "','" + etatCivil +                                            
                                                    "','" + nbrEnfants.text +
                                                    "','" + nbrVisite + 
                                                    "','" + CreationDate() +
                                                    "',false)";
            MySqlCommand cmd = new MySqlCommand(command, con);

            try
            {
                cmd.ExecuteReader();
                Debug.Log("Ajout patient");
                SceneManager.LoadScene(1);
                nbrVisite++;

            }
            catch (IOException ex)
            {
                Debug.Log(ex.ToString());
            }
            cmd.Dispose();
            con.Close();
        }

    }

    public void PlayerConnection()
    {
        ConnectBDD();
        string pseudo = null;

        try
        {

            MySqlCommand commandeSql = new MySqlCommand("SELECT * FROM patient WHERE pseudo = '" + pseudoLog.text + "'", con);
            MySqlDataReader MyReader = commandeSql.ExecuteReader();
            while (MyReader.Read())
            {
                pseudo = MyReader["pseudo"].ToString();

                if (pseudo == pseudoLog.text)
                {
                    Debug.Log("Welcome");
                    SceneManager.LoadScene(1);
                    nbrVisite++;
                }
                else
                {
                    Debug.Log("Pseudo inexistant");
                }
            }
            MyReader.Close();
        }
        catch (IOException EX) { Debug.Log(EX.ToString()); }
        con.Close();

        
    }


    

    

    
    

   
    

}
