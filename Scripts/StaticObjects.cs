using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticObjects : MonoBehaviour
{
    //UI Debug Text Elements
    GameObject text,text2,text3;
    public static Text DebugText, DebugText2, DebugText3;

    void Start()
    {
        text = GameObject.Find("DebugText");
        DebugText = text.GetComponent<Text>();
        DebugText.text = "DebugText is running.";
        text2 = GameObject.Find("DebugText2");
        DebugText2 = text2.GetComponent<Text>();
        DebugText2.text = "DebugText is running.";
        text3 = GameObject.Find("DebugText3");
        DebugText3 = text3.GetComponent<Text>();
        DebugText3.text = "DebugText is running.";


    }

    // Update is called once per frame
    void Update()
    {
        //if (ballObject != null) { Debug.Log(ballObject.name); }

    }
}
