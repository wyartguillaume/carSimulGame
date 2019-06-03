using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJeux : MonoBehaviour
{
    [SerializeField]
    GameObject levelSelectionMenuJour, levelSelectionMenuNuit, mainMenu, menuChoixJourNuit;


   /* public void GoLevelSelection()
    {
        levelSelectionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }*/

    public void GoMainMenu()
    {

        mainMenu.SetActive(true);
        menuChoixJourNuit.SetActive(false);
    }

    public void GoMenuChoix()
    {
        SceneManager.LoadScene(0);
    }

    public void GoLevelJour()
    {
        levelSelectionMenuJour.SetActive(true);
        menuChoixJourNuit.SetActive(false);
    }

    public void GoLevelNuit()
    {
        levelSelectionMenuNuit.SetActive(true);
        menuChoixJourNuit.SetActive(false);
    }

    public void GoMenuChoixLevelNuitJour()
    {
        levelSelectionMenuNuit.SetActive(false);
        levelSelectionMenuJour.SetActive(false);
        mainMenu.SetActive(false);
        menuChoixJourNuit.SetActive(true);
    }


}
