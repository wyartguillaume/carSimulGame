using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class counterButton : MonoBehaviour {

    public int buttonAcceleration;
    public int buttonDesacceleration;
    private void Update()
    {
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            buttonDesacceleration++;
            Debug.Log(buttonDesacceleration);
        }
        if (CrossPlatformInputManager.GetButton("Vertical"))
        {
            buttonAcceleration++;
        }
    }
}
