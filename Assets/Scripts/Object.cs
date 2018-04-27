using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Object
{
    public string id;
    public Definition definition;

    public Object(string id, string definitionName, string definitionDescription)
    {
        this.id = id;
        this.definition = new Definition(definitionName, definitionDescription);
    }

}
