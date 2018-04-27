using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateProfile : MonoBehaviour {
    [System.Serializable]
    public struct pageInfo
    {
        public string tableName;
        public string titleText;
        public string infoText;
        public Sprite image;
    }

    public pageInfo[] pagesInfo;

    public RectTransform window;
	public bool startWelcomeScreen;

    
    

	List<RectTransform> pageLayout;
    public GameObject swipePagePrefab;
	private float wide;

	private float mousePositionStartX;
	private float mousePositionEndX;
	private float dragAmount ;
	private float screenPosition;
	private float lastScreenPosition;
	private float lerpTimer;
	private float lerpPage;

	public int pageCount = 1;
	public string side = "";

	public int swipeThrustHold = 30;
	public int spaceBetweenProfileImages = 30;
	private bool canSwipe;

	public GameObject cartoonWindow;

	public Texture2D userPic;

	#region mono functions

	void Start() {

        

        pageLayout = new List<RectTransform>();
		wide = cartoonWindow.GetComponent<RectTransform>().rect.width;

		for(int i = 0; i < pagesInfo.Length; i++){


            GameObject currentPage = GameObject.Instantiate(swipePagePrefab);
            currentPage.transform.SetParent(cartoonWindow.transform);
            Text[] t = currentPage.GetComponentsInChildren<Text>();

            //title first in the hierarchy, then the description
            t[0].text = pagesInfo[i].titleText.Translate();
            t[1].text = pagesInfo[i].infoText.Translate();
            currentPage.GetComponentInChildren<Button>().gameObject.GetComponent<LoadSceneButton>().tableToLoad = pagesInfo[i].tableName;

            currentPage.GetComponentsInChildren<Image>()[1].sprite = pagesInfo[i].image;

            RectTransform buttonRectTrans = currentPage.GetComponent<RectTransform>();
            buttonRectTrans.anchoredPosition = new Vector2(((wide + spaceBetweenProfileImages) * i), 0);
            pageLayout.Add(buttonRectTrans);

		}

		side = "right";
        
        startWelcomeScreen = true;



	}

	void Update() {

		if(!startWelcomeScreen)
			return;

		lerpTimer=lerpTimer+Time.deltaTime;
		if(lerpTimer<.333){
			screenPosition = Mathf.Lerp(lastScreenPosition ,lerpPage*-1 , lerpTimer*3);
			lastScreenPosition=screenPosition;
		}

		if(Input.GetMouseButtonDown(0) && Input.mousePosition.y > (Screen.height*0.2f) && Input.mousePosition.y < (Screen.height*0.91f) ) {
			canSwipe = true;
			mousePositionStartX = Input.mousePosition.x;
		}


		if(Input.GetMouseButton(0)) {
			if(canSwipe){
				mousePositionEndX = Input.mousePosition.x;
				dragAmount=mousePositionEndX-mousePositionStartX;
				screenPosition=lastScreenPosition+dragAmount;
			}
		}

		if(Mathf.Abs(dragAmount) > swipeThrustHold && canSwipe){
			canSwipe = false;
			lastScreenPosition=screenPosition;
			if(pageCount < pageLayout.Count )
				OnSwipeComplete () ;
			else if(pageCount == pageLayout.Count && dragAmount < 0)
				lerpTimer=0;
			else if(pageCount == pageLayout.Count && dragAmount > 0)
				OnSwipeComplete () ;
		}

		if(Input.GetMouseButtonUp(0)) {

			if(Mathf.Abs(dragAmount) < swipeThrustHold) {
                OnSwipeComplete();

            }
		}

		for(int i = 0; i < pageLayout.Count; i++){

			pageLayout[i].anchoredPosition = new Vector2(screenPosition+((wide+spaceBetweenProfileImages)*i),0);

			if(side == "right") {
				if(i == pageCount-1) {
					pageLayout[i].localScale = Vector3.Lerp(pageLayout[i].localScale,new Vector3(1.2f,1.2f,1.2f),Time.deltaTime*5);
					Color temp = pageLayout[i].GetComponent<Image>().color;
					pageLayout[i].GetComponent<Image>().color = new Color(temp.r,temp.g,temp.b,1);
				} else {
					pageLayout[i].localScale = Vector3.Lerp(pageLayout[i].localScale,new Vector3(0.7f,0.7f,0.7f),Time.deltaTime*5);
					Color temp = pageLayout[i].GetComponent<Image>().color;
					pageLayout[i].GetComponent<Image>().color = new Color(temp.r,temp.g,temp.b,0.5f);
				}
			} else {
				if(i == pageCount) {
					pageLayout[i].localScale = Vector3.Lerp(pageLayout[i].localScale,new Vector3(1.2f,1.2f,1.2f),Time.deltaTime*5);
					Color temp = pageLayout[i].GetComponent<Image>().color;
					pageLayout[i].GetComponent<Image>().color = new Color(temp.r,temp.g,temp.b,1);
				} else {
					pageLayout[i].localScale = Vector3.Lerp(pageLayout[i].localScale,new Vector3(0.7f,0.7f,0.7f),Time.deltaTime*5);
					Color temp = pageLayout[i].GetComponent<Image>().color;
					pageLayout[i].GetComponent<Image>().color = new Color(temp.r,temp.g,temp.b,0.5f);
				}
			}
		}


	}

	#endregion



	private void OnSwipeComplete () {

		lastScreenPosition=screenPosition;

		if(dragAmount > 0){

			if(Mathf.Abs(dragAmount) > (swipeThrustHold)){

				if(pageCount == 0){
					lerpTimer=0;
					lerpPage=0;
				}else {
					if(side == "right")
						pageCount--;
					side = "left";
					pageCount-=1;
					lerpTimer=0;
					if(pageCount < 0)
						pageCount = 0;
					lerpPage = (wide+spaceBetweenProfileImages)*pageCount;
					//introimage[pagecount] is the current picture
				}

			} else {
				lerpTimer=0;
			}

		} else if(dragAmount < 0) {

			if(Mathf.Abs(dragAmount) > (swipeThrustHold)){

				if(pageCount == pageLayout.Count)
                {
					lerpTimer=0;
					lerpPage=(wide+spaceBetweenProfileImages)*pageLayout.Count - 1;
				}else {
					if(side == "left")
						pageCount++;
					side = "right";
					lerpTimer=0;
					lerpPage = (wide+spaceBetweenProfileImages)*pageCount;
					pageCount++;
					//introimage[pagecount] is the current picture
				}

			} else {

				lerpTimer=0;
			}
		}
	}
}
