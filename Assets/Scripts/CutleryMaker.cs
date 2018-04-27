using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
public class CutleryMaker : MonoBehaviour {

    public GameObject cutlery;
	public float createHeight;
    public bool isBallCreating = true;
    public CutlerySwitcher switcher;
    private ChangeCutlery cut;
    void CreateCutlery(Vector3 atPosition)
	{
        GameObject culteryGO = Instantiate (cutlery, atPosition, Quaternion.identity);
        cut = culteryGO.GetComponent<ChangeCutlery>();
        if(cut)
            cut.index++;
        switcher.UpdateText();
        //isBallCreating = false;
        Destroy(this);

	}

	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0 && isBallCreating)
		{
			var touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
				ARPoint point = new ARPoint {
					x = screenPosition.x,
					y = screenPosition.y
				};
						
				List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, 
					ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
				if (hitResults.Count > 0) {
					foreach (var hitResult in hitResults) {
						Vector3 position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                        CreateCutlery (new Vector3 (position.x, position.y, position.z));
						break;
					}
				}

			}
		}

	}

}
