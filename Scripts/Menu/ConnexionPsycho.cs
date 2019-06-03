using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class ConnexionPsycho : MonoBehaviour
{
    public TMP_Text connexionReussie, connexionEchec;
    public TMP_InputField email, mdp;
    public GameObject ConnexionMenuPsy, mainMenuPatient;


    private void Start()
    {

    }
    public void Connexion()
    {

        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        WWWForm form = new WWWForm();
        form.AddField("mdpPost", mdp.text);
        form.AddField("email", email.text);
        using (UnityWebRequest www = UnityWebRequest.Post("https://cognitivedrive.be/gameConnexionPsycho", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                connexionEchec.text = "Adresse email ou mot de passe incorrect";

            }
            else
            {
                connexionReussie.text = "Bienvenue " +email.text;
                ConnexionMenuPsy.SetActive(false);
                mainMenuPatient.SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class PsychoCo
{
    public string email;
    public string mdp;
}

[System.Serializable]
public class ResultPsychoCo
{
    public PsychoCo[] psychosCo;
}