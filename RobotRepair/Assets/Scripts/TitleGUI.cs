using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleGUI : MonoBehaviour
{
    public GUISkin customSkin;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void OnGUI()
    {
        //button width:
       int buttonW= 100;
        //button height
        int buttonH = 50;

        //half of the screen width
        float halfScreenW = Screen.width / 2;
        //half of the button width
        float halfButtonW = buttonW/ 2;

        GUI.skin = customSkin;
        if (GUI.Button(new Rect(halfScreenW-halfButtonW, 310, buttonW, buttonH),"Play game" ))
        {
            //print("You clicked me!");
            Application.LoadLevel("game");
        }
    }
}
