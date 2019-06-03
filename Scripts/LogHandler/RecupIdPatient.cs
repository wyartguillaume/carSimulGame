using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RecupIdPatient : MonoBehaviour {
    public SavedPlayerInfo savedPlayerInfo;
    public string namePatient;
    public string patientId;
    Patient myObject = new Patient();
    public int idPat;

    private void Start()
    {
        StartCoroutine(findPatientId());
    }

    private void Awake()
    {
        savedPlayerInfo = GameObject.FindObjectOfType<SavedPlayerInfo>();
    }



    IEnumerator findPatientId()
    {

        WWWForm form = new WWWForm();
        form.AddField("nomPatient", savedPlayerInfo.returnPlayerName());
        using (UnityWebRequest www = UnityWebRequest.Post("https://cognitivedrive.be/gameIdPat", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string jsonString = www.downloadHandler.text;
                myObject  = JsonUtility.FromJson<Patient>(jsonString);
                Debug.Log(myObject.id);
                idPat = myObject.id;
            }
        }
    }

    public int recup()
    {
        return idPat;
    }
}


[System.Serializable]
public class Patient
{
    public int id;
}


