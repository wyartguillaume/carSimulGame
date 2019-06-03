using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;


public class InscrptionPsycho : MonoBehaviour {

    public TMP_InputField prenomPsycho, nomPsycho, mailPsy, mdp;
    [SerializeField]
    GameObject InscriptionMenuPsy, mainMenuPatient;
    public TMP_Dropdown psychologue;
    private int index;
    private string nomPsy, prenomPsy;
    TMP_Dropdown.OptionData m_NewData, m_NewData2;
    List<TMP_Dropdown.OptionData> m_Messages = new List<TMP_Dropdown.OptionData>();

    public void CreateUser()
    {

        StartCoroutine(InscriptionPsycho());
    }

    IEnumerator InscriptionPsycho()
    {
        WWWForm form = new WWWForm();
        form.AddField("nomPost", nomPsycho.text);
        form.AddField("prenomPost", prenomPsycho.text);
        form.AddField("mdpPost", mdp.text);
        form.AddField("emailPost", mailPsy.text);
        
 
        using (UnityWebRequest www = UnityWebRequest.Post("https://cognitivedrive.be/gameInsPsy", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                InscriptionMenuPsy.SetActive(false);
                mainMenuPatient.SetActive(true);
            }
        }
    }

    

 }



