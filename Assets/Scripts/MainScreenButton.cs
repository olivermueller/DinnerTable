using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenButton : MonoBehaviour
{
    RegexUtilities emailChecker;
    private void Start()
    {
        emailChecker = new RegexUtilities();
    }
    public void SendInfo()
    {
        Settings.username = GameObject.FindGameObjectWithTag("UsernameText").GetComponent<Text>().text;
        Settings.email = GameObject.FindGameObjectWithTag("EmailText").GetComponent<Text>().text;
        Debug.Log(Settings.email);
        Debug.Log(Settings.username);
        if (emailChecker.IsValidEmail(Settings.email))
        {
            

            XAPIStatement statement = new XAPIStatement(Settings.username, "mailto:" + Settings.email, "started", "http:∕∕adlnet.gov∕expapi∕verbs∕initialized", "http:∕∕adlnet.gov∕expapi∕activities∕DinnerTable", "Dinner Table", "Started Dinner Table");
            GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>().enabled = true;
            transform.parent.GetComponent<Canvas>().enabled = false;
            Settings.instance.SEND(statement);

            PlayerPrefs.SetString("Name", Settings.username);
            PlayerPrefs.SetString("Email", Settings.email);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Email").transform.GetChild(1).GetComponent<Text>().text = "";
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
