using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour {

	[SerializeField]
    private int startCountDown = 10;
    SaveSessionLevel1 sessionLevel1;
    SaveSessionLevel2 sessionLevel2;
    SaveSessionLevel3 sessionLevel3;
    NextScene endLevel;

    IEnumerator Decompte()
    {
        while (startCountDown > 0)
        {
            yield return new WaitForSeconds(1f);
            startCountDown--;
        }
        sessionLevel1.CreateSession();
        sessionLevel2.CreateSession();
        sessionLevel3.CreateSession();
        endLevel.EndLevel();
    }

    private void Start()
    {
        sessionLevel1 = GameObject.FindObjectOfType<SaveSessionLevel1>();
        sessionLevel2 = GameObject.FindObjectOfType<SaveSessionLevel2>();
        sessionLevel3 = GameObject.FindObjectOfType<SaveSessionLevel3>();
        endLevel = GameObject.FindObjectOfType<NextScene>();
    }
    public void DemarrerDecompte()
    {
        StartCoroutine(Decompte());
    }
}
