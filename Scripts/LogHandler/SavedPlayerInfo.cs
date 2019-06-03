using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedPlayerInfo : MonoBehaviour {

	public string savedName;
	private PlayerInfo playerInfo;

	public string savedDOB;

	public string savedGroup;

	public string savedLaterality;

	public string savedLevel;

    public string savedMeteo;

    public int savedSexe;

	void Start () {
		playerInfo = GameObject.FindObjectOfType<PlayerInfo>();	
	}

	public string returnPlayerName()
	{
        if (playerInfo.pseudoAd == "")
        {
            savedName = playerInfo.pseudo;
        }
        else
        {
            savedName = playerInfo.pseudoAd;
        }
		return savedName;
	}

	public string returnPlayerDOB()
	{
        if (playerInfo.dobAd == "")
        {
            savedDOB = playerInfo.dob;
        }
        else
        {
            savedDOB = playerInfo.dobAd;
        }
		return savedDOB;
	}

	public string returnPlayerGroup()
	{
        if (playerInfo.groupAd == "")
        {
            savedGroup = playerInfo.group;
        }
        else
        {
            savedGroup = playerInfo.groupAd;
        }
		return savedGroup;
	}

	public string returnPlayerLaterality()
	{
        if (playerInfo.lateralityAd =="")
        {
            savedLaterality = playerInfo.laterality;
        }
        else
        {
            savedLaterality = playerInfo.lateralityAd;
        }
		return savedLaterality;
	}

    public string returnPlayerSexe()
    {
        savedSexe= playerInfo.sexe;
        return savedGroup;
    }

    public string level()
	{

		return savedLevel;
	}

    public string jourNuit()
    {
        return savedMeteo;
    }

}
