﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour { 
    public static string POSTAddUserURL = "https://competenceanalytics.com/data/xAPI/statements";
    public static string AUTHORIZATION_KEY = "MjgxOTUyZjAzZDRlNDU1NDBmYTY3ZTQ2M2ZmZGJhZGE1NDViMjg3NTo1MTVhN2I2OWNlYjdkZTAyYWU4Yzk2ZTA1ODk3M2YwMmQ5ZTI5N2I0";

    public static Settings instance = null;
    public string tableToLoad;
    public string gameScene = "DiningTable";
    public string mainMenuScene = "MainScene";

    private XAPIStatement statement;
    public static List<string> languageBase;
    void Awake()
    {

        statement = new XAPIStatement("Leo", "mailto:leom@itu.dk", "started", "http:∕∕adlnet.gov∕expapi∕verbs∕initialized", "http:∕∕adlnet.gov∕expapi∕activities∕DinnerTable", "Dinner Table", "Started Dinner Table");
        SEND(statement);
        if (instance == null)
            instance = this;

        else if (instance != this)

        Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    bool once = true;
    public void ChangeLanguage()
    {
        Text[] allTextObjects = GameObject.FindObjectsOfType<Text>();
        if (ExtensionMethod.currentLanguage != ExtensionMethod.Language.en && once)
        {
            languageBase = new List<string>();
            once = false;
            foreach (Text t in GameObject.FindObjectsOfType<Text>())
                languageBase.Add(t.text);
        }

        for (int i=0; i< allTextObjects.Length; i++)
        {
            allTextObjects[i].text = languageBase[i].Translate();
        }
    }

    public WWW SEND(XAPIStatement statement)
    {
        string json = JsonUtility.ToJson(statement);
        json = json.Replace("_", string.Empty);
        Debug.Log(json.ToString());
        WWW www;
        Dictionary<string, string> postHeader = new Dictionary<string, string>();
        postHeader.Add("Content-Type", "application/json");
        postHeader.Add("Authorization", "Basic " + AUTHORIZATION_KEY);
        postHeader.Add("X-Experience-API-Version", "1.0.1");
        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(json);
        
        www = new WWW(POSTAddUserURL, formData, postHeader);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    IEnumerator WaitForRequest(WWW data)
    {
        yield return data;

    }

}
