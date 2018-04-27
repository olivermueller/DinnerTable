using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

public class NameGUIScreen : MonoBehaviour {

    RegexUtilities emailChecker;
    void Start ()
    {
        emailChecker = new RegexUtilities();
        
        
    }
    private TouchScreenKeyboard keyboard;
    
	void OnGUI ()
    {
        if (Settings.showEmailGUI)
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
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
                if (emailChecker.IsValidEmail(Settings.email))
                {
                    XAPIStatement statement = new XAPIStatement(Settings.username, "mailto:" + Settings.email, "started", "http:∕∕adlnet.gov∕expapi∕verbs∕initialized", "http:∕∕adlnet.gov∕expapi∕activities∕DinnerTable", "Dinner Table", "Started Dinner Table");
                    Settings.instance.SEND(statement);
                    Settings.showEmailGUI = false;
                    FindObjectOfType<Canvas>().enabled = true;
                    keyboard.active = false;
                }
                else
                {
                    Settings.email = "";
                }
            }
        }
    }
    public class RegexUtilities
    {
        bool invalid = false;

        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper);
            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}
