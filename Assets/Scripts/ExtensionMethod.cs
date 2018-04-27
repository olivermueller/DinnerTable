using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public static class ExtensionMethod
{

    public enum Language
    {
        en,
        da,
        de
    }

    public static Language currentLanguage;



    public static string Translate(this string input)
    {
        input = input.ToLower().Replace(" ", "");
        input = input.Replace(",", "");
        input = input.Replace("-", "");
        input = input.Replace("!", "");
        string output = "";
        

        TextAsset text = Resources.Load("Languages") as TextAsset;

        var doc = new XmlDocument();
        doc.Load(new StringReader(text.text));

        var baseNode = doc.DocumentElement;

        int nNodes = baseNode.ChildNodes.Count;

        foreach (XmlElement node in baseNode.ChildNodes)
        {
            if (node.Name == input || input.Contains(node.Name))
            {
                output = node.SelectSingleNode(currentLanguage.ToString()).InnerText;
            }
        }
        return output;
    }
}
