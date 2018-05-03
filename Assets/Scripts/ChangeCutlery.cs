using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCutlery : MonoBehaviour {
    public GameObject[] cutlery, cutprefabs;
    private UIUtils utils;

    public int index = 0;
    // Use this for initialization
    void Start () {
        utils = FindObjectOfType<UIUtils>();
        

	}
	

    public void switchCutlery(){
        if(index<cutlery.Length)
        {
            cutlery[index].SetActive(true);
            utils.displayedObjectPrefab = cutprefabs[index];
            XAPIStatement statement = new XAPIStatement(Settings.username, "mailto:" + Settings.email, "placed", "https://w3id.org/xapi/dod-isd/verbs/placed", "http:∕∕adlnet.gov∕expapi∕activities∕DinnerTable", cutprefabs[index].name, "Placed object");
            Settings.instance.SEND(statement);
            index++;
        }
    }
}
