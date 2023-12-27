using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class logActivity : MonoBehaviour
{
    string filename = "";

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }
    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/LogFile.csv";
    }


    public void Log(string logString, string stackTrace, LogType type)
    {
        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine("[" + System.DateTime.Now + "]" + logString);
        tw.Close();
    }


 
}
