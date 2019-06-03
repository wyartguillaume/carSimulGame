using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

	public int sceneNumber;


    public void EndLevel()
    {
        StartCoroutine(EndLevelWait());
    }

    IEnumerator EndLevelWait()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(sceneNumber);
    }

    public void OnclickNextLevel1()
	{
		SceneManager.LoadScene("Scenes/Level1/Level1");
	}

    public void OnclickNextLevel13()
    {
        SceneManager.LoadScene("Scenes/Level1/Level1_3min");
    }

    public void OnclickNextLevel16()
    {
        SceneManager.LoadScene("Scenes/Level1/Level1_6min");
    }

    public void OnclickNextLevel2()
    {
        SceneManager.LoadScene("Scenes/Level2/Level2");
    }


    public void OnclickNextSceneLevel23()
    {
        SceneManager.LoadScene("Scenes/Level2/Level2_3min");
    }

    public void OnclickNextSceneLevel26()
    {
        SceneManager.LoadScene("Scenes/Level2/Level2_6min");
    }

    public void OnclickNextSceneLevel3()
    {
        SceneManager.LoadScene("Scenes/Level3/Level3");
    }

    public void OnclickNextSceneLevel1Nuit()
    {
        SceneManager.LoadScene("Scenes/Level1/Level1Nuit");
    }

    public void OnclickNextLevel13Nuit()
    {
        SceneManager.LoadScene("Scenes/Level1/Level1_3minNuit");
    }

    public void OnclickNextLevel16Nuit()
    {
        SceneManager.LoadScene("Scenes/Level1/Level1_6minNuit");
    }

    public void OnclickNextLevel2Nuit()
    {
        SceneManager.LoadScene("Scenes/Level2/Level2Nuit");
    }

    public void OnclickNextLevel23Nuit()
    {
        SceneManager.LoadScene("Scenes/Level2/Level2_3minNuit");
    }

    public void OnclickNextLevel26Nuit()
    {
        SceneManager.LoadScene("Scenes/Level2/Level2_6minNuit");
    }

    public void OnclickNextLevel3Nuit()
    {
        SceneManager.LoadScene("Scenes/Level3/Level3Nuit");
    }

    public void OnclickNextLevel33()
    {
        SceneManager.LoadScene("Scenes/Level3/Level3_3min");
    }

    public void OnclickNextLevel36()
    {
        SceneManager.LoadScene("Scenes/Level3/Level3_6min");
    }

    public void OnclickNextLevel33Nuit()
    {
        SceneManager.LoadScene("Scenes/Level3/Level3_3minNuit");
    }

    public void OnclickNextLevel36Nuit()
    {
        SceneManager.LoadScene("Scenes/Level3/Level3_6minNuit");
    }

    public void nextscene()
    {
        SceneManager.LoadScene(sceneNumber);
    }

	

}
