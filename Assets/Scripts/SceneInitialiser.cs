using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitialiser : MonoBehaviour {

	void Start ()
    {
        

        if (SceneManager.GetActiveScene().name.CompareTo(Settings.instance.gameScene) == 0)
        {
            GameObject.FindGameObjectWithTag("ModeChooser").GetComponent<CutlerySwitcher>().cutleryPrefab = (GameObject)Resources.Load(Settings.instance.tableToLoad, typeof(GameObject));
        }
    }
	
}
