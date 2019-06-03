using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnexionMenu : MonoBehaviour {

    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject connexionMenu;

    public void RetourMenu()
    {
        connexionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
