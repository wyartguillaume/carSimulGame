using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PlayerInfo : MonoBehaviour
{

	public GameObject instance;
    //patient
    public string pseudo = "", pseudoAd="";
    public TMP_InputField pseudoInput, pseudoInputAd;

    public string dob = "", dobAd="";
    public TMP_InputField dateDeNaissance, dateDeNaissanceAd;

    public string group = "", groupAd= "";
    public TMP_Dropdown dropDownGroup, dropDownGroupAd;

    public string laterality = "", lateralityAd="";
    public TMP_Dropdown dropDownLaterality, dropDownLateralityAd;

    public Toggle homme, femme, hommeAd, femmeAd;
    public int sexe;

   

	void Awake()
	{
		//inputFieldDob = GetComponent<InputField>();
		DontDestroyOnLoad(instance);
	}

	void Update()
	{
		SetUserName();
		SetDOB();
		SetGroup();
		SetLaterality();
        SetSexe();
        SetUsernameAd();
        SetGroupAd();
        SetDOBAd();
        SetLateralityAd();
		
	}

    public void SetSexe()
    {
        if (homme.isOn || hommeAd.isOn)
        {
            sexe = 0;
        }
        if (femme.isOn || femmeAd.isOn)
        {
            sexe = 1;
        }
    }

 
    public string SetUserName()
	{
		pseudo = pseudoInput.text;
		return pseudo;
	}

    public string SetUsernameAd()
    {
        pseudoAd = pseudoInputAd.text;
        return pseudoAd;
    }

	public string SetDOB()
	{
		dob = dateDeNaissance.text;
        dob = dateDeNaissanceAd.text;
		return dob;
	}

    public string SetDOBAd()
    {
        dobAd = dateDeNaissanceAd.text;
        return dob;
    }


    public string SetGroup()
	{
		group = dropDownGroup.options[dropDownGroup.value].text;
        return group;
	}

    public string SetGroupAd()
    {
        groupAd = dropDownGroupAd.options[dropDownGroupAd.value].text;
        return groupAd;
    }

    public string SetLaterality()
	{
		laterality = dropDownLaterality.options[dropDownLaterality.value].text;
        return laterality;
	}

    public string SetLateralityAd()
    {
        lateralityAd = dropDownLateralityAd.options[dropDownLateralityAd.value].text;
        return lateralityAd;
    }



}
