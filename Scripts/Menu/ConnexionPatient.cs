using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ConnexionPatient : MonoBehaviour
{

    public TMP_InputField pseudo;
    
    public void Connexion()
    {

        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = new UnityWebRequest("https://cognitivedrive.be/gameConnexionPatient");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            string jsonString = www.downloadHandler.text;
            ResultPatientCo patient = JsonUtility.FromJson<ResultPatientCo>("{\"patientCo\":" + jsonString + "}");
            for (int i = 0; i < patient.patientCo.Length; i++)
            {
                if (pseudo.text == patient.patientCo[i].pseudo)
                {
                    Debug.Log("Welcome");
                    SceneManager.LoadScene(1);
                }
            }
        }
    }
}

[System.Serializable]
public class PatientCo
{
    public string pseudo;
    public string dateDeNaissance;
    public string lateralite;
    public string groupe;
}

[System.Serializable]
public class ResultPatientCo
{
    public PatientCo[] patientCo;
}