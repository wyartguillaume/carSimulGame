using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MauvaisChemin : MonoBehaviour
{

    [SerializeField]
    GameObject textMauvaisChemin;

  

    private void Update()
    {
        if (this.transform.rotation.eulerAngles.y < 200f && this.transform.rotation.eulerAngles.y > 100f)
        {
            textMauvaisChemin.SetActive(true);
        }
        else
        {
            textMauvaisChemin.SetActive(false);
        }

    }
   
}
