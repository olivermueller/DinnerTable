using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGUIScreen : MonoBehaviour {

	
	void Start ()
    {
        GameObject.FindObjectOfType<Canvas>().enabled = false;
	}
	
	
	void OnGUI ()
    {
        GUI.Label(new Rect(Screen.width/2-100, Screen.height/2-30, 200, 20), "Name");
        Settings.username = GUI.TextField(new Rect(Screen.width/2-100, Screen.height/2, 200, 20), Settings.username, 25);

    }
}
