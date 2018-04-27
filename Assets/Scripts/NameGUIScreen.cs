using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGUIScreen : MonoBehaviour {

	
	void Start ()
    {
        
        FindObjectOfType<Canvas>().enabled = false;
	}

    bool showGUI = true;
	void OnGUI ()
    {
        if (showGUI)
        {
            GUI.skin.label.fontSize = GUI.skin.button.fontSize = GUI.skin.textField.fontSize = 40;
            Rect nameInputRect = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 90, 400, 100);


            Settings.username = GUI.TextField(nameInputRect, Settings.username, 25);
            if (Settings.username.Length == 0)
                GUI.Label(nameInputRect, "Name");

            Rect emailTextRect = new Rect(nameInputRect.x, nameInputRect.y + 100, 400, 100);

            Settings.email = GUI.TextField(emailTextRect, Settings.email, 25);
            if (Settings.email.Length == 0)
                GUI.Label(emailTextRect, "Email");
            Rect buttonRect = new Rect(emailTextRect.x, emailTextRect.y + 100, 400, 100);
            if (GUI.Button(buttonRect, "SUBMIT"))
            {
                XAPIStatement statement = new XAPIStatement(Settings.username, "mailto:" + Settings.email, "started", "http:∕∕adlnet.gov∕expapi∕verbs∕initialized", "http:∕∕adlnet.gov∕expapi∕activities∕DinnerTable", "Dinner Table", "Started Dinner Table");
                Settings.instance.SEND(statement);
                showGUI = false;
                FindObjectOfType<Canvas>().enabled = true;
            }
        }
    }
}
