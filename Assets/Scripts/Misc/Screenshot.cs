using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    //Saves a screenshot when a button is pressed

    [SerializeField] KeyCode screenShotButton;

    private void Awake()
    {
        screenShotButton = KeyCode.F12;
    }

    void Update()
    {
        if (Input.GetKeyDown(screenShotButton))
        {
            ScreenCapture.CaptureScreenshot("D:/stuff/Unity Screens/screenshot " + 
                System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png");
            Debug.Log("A screenshot was taken!");
        }
    }
}
