using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class CreateDropdownPat : MonoBehaviour
{
    public string nomPsy, prenomPsy;
    public TMP_Dropdown psychologue, psychologueAd;
    private int index;
    TMP_Dropdown.OptionData m_NewData, m_NewData2;
    List<TMP_Dropdown.OptionData> m_Messages = new List<TMP_Dropdown.OptionData>();
    void Start()
    {

        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = new UnityWebRequest("https://cognitivedrive.be/gameCreateDropdown");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            psychologue.ClearOptions();
            psychologueAd.ClearOptions();
            string jsonString = www.downloadHandler.text;
            ResultPsycho psycho = JsonUtility.FromJson<ResultPsycho>("{\"psychos\":" + jsonString + "}");
            m_NewData2 = new TMP_Dropdown.OptionData();
            m_NewData2.text = "Les psychologues";
            m_Messages.Add(m_NewData2);
            for (int i = 0; i < psycho.psychos.Length; i++)
            {
                m_NewData = new TMP_Dropdown.OptionData();

                m_NewData.text = psycho.psychos[i].nom + " " + psycho.psychos[i].prenom;
                m_Messages.Add(m_NewData);

                //Take each entry in the message List
            }

            foreach (TMP_Dropdown.OptionData message in m_Messages)
            {
                //Add each entry to the Dropdown
                psychologue.options.Add(message);
                psychologueAd.options.Add(message);
                //Make the index equal to the total number of entries
                index = m_Messages.Count - 1;
            }
            
        }
    }

}


[System.Serializable]
public class Psycho
{
    public string nom;
    public string prenom;
    public int id;
}

[System.Serializable]
public class ResultPsycho
{
    public Psycho[] psychos;
}



