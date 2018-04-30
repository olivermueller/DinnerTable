using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
public class CutlerySwitcher : MonoBehaviour {

	//public GameObject ballMake;
    private ChangeCutlery cutlery;
    //public Button next, reset;
    public Text nextText, resetText;
    bool isBallCreating = true;
    public GameObject cutleryPrefab;
    public bool useDebugMode = true;
	
	
	// Update is called once per frame
	void Update () {
        

        if(!cutlery)
        {
            if (GameObject.FindGameObjectsWithTag("Plane").Length > 0)
            {
                nextText.text = "Touch screen to place table cloth";
            }
            else
                nextText.text = "Scan area".Translate();

            if (useDebugMode && Input.GetMouseButton(0) && isBallCreating)
            {
                CreateCutlery(new Vector3(0, -1, 0));
            }
            else if (Input.touchCount > 0 && isBallCreating)
            {
                var touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began)
                {
                    var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
                    //CreateCutlery(Vector3.zero);
                    ARPoint point = new ARPoint
                    {
                        x = screenPosition.x,
                        y = screenPosition.y
                    };

                    List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point,
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
                    Debug.Log(hitResults.Count);
                    if (hitResults.Count > 0)
                    {
                        
                        foreach (var hitResult in hitResults)
                        {
                            Vector3 position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                            CreateCutlery(new Vector3(position.x, position.y, position.z));
                            Switch();
                            break;
                        }
                    }

                }
            }

            //var cut = FindObjectOfType<ChangeCutlery>();
            //if (cut)
            //    cutlery = cut;
        }
	}
    void CreateCutlery(Vector3 atPosition)
    {
        GameObject culteryGO = Instantiate(cutleryPrefab, atPosition, Quaternion.identity);
        cutlery = culteryGO.GetComponent<ChangeCutlery>();
        if (cutlery)
            cutlery.switchCutlery();
        UpdateText();
        isBallCreating = false;

    }
    /*
	void OnGUI()
	{
        GUI.skin.label.fontSize = 120;
        string modeString = "";
        if (cutlery)
            modeString = cutlery.cutlery[cutlery.index-1].name + " placed";
        else
        {
            modeString = "Press screen to begin";
        }
        if (GUI.Button(new Rect(Screen.width -300.0f, 0.0f, 300.0f, 200.0f), modeString))
		{
            Switch();
		}
        if(cutlery)
        {
            if (GUI.Button(new Rect(Screen.width - 300.0f, 200.0f, 300.0f, 200.0f), "Reset"))
            {
                Reset();
            }
        }

	}
	*/
    bool passed = false;
    public void Switch(){
        if (cutlery)
        {
            if (cutlery.index == cutlery.cutlery.Length)
            {
                nextText.text = "Well done!".Translate();
                if (passed == false)
                {
                    XAPIStatement statement = new XAPIStatement(Settings.username, "mailto:" + Settings.email, "passed", "http:∕∕adlnet.gov∕expapi∕verbs∕passed", "http:∕∕adlnet.gov∕expapi∕activities∕DinnerTable", Settings.instance.currentScenario, "Completed " + Settings.instance.currentScenario);
                    Settings.instance.SEND(statement);
                    passed = true;
                }
            }
            else{
                cutlery.switchCutlery();
                UpdateText();
            }
        }
    }
    public void UpdateText(){
        nextText.text = "Place " + cutlery.cutlery[cutlery.index - 1].name;
    }
    public void Reset(){
        if(cutlery){
            Destroy(cutlery.gameObject);
            //ballMake.GetComponent<CutleryMaker>().isBallCreating = true;
            isBallCreating = true;

            if(GameObject.FindGameObjectsWithTag("Plane").Length > 0)
            {
                nextText.text = "Touch screen to place table cloth";
            }
            else
                nextText.text = "Scan area";
    
        }
    }
}
