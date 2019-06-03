using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChoix : MonoBehaviour {


    public void GoPsy()
    {
        SceneManager.LoadScene(1);
    }

    public void GoPatient()
    {
        SceneManager.LoadScene(2);
    }

    public void Quitter()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif  
    }

}
