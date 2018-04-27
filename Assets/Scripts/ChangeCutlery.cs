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
	
	// Update is called once per frame
	void Update () {
		
	}
    public void switchCutlery(){
        if(index<cutlery.Length)
        {
            cutlery[index].SetActive(true);
            utils.displayedObjectPrefab = cutprefabs[index];
            index++;
        }
    }
}
