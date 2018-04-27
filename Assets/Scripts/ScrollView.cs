using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour {


    [System.Serializable]
    public struct pageInfo
    {
        public string tableName;
        public string titleText;
        public string infoText;
        public Sprite image;
    }

    public pageInfo[] pagesInfo;

    public int scrollSpeed;

    Vector2 initialSize;
    int currentlyFocused=0;
    int totalElements;
    private void Start()
    {


        for (int i = 0; i < pagesInfo.Length; i++)
        {
            Text[] t = transform.GetChild(i).GetComponentsInChildren<Text>();

            //title first in the hierarchy, then the description
            t[0].text = pagesInfo[i].titleText.Translate();
            t[1].text = pagesInfo[i].infoText.Translate();
            transform.GetChild(i).GetComponentInChildren<Button>().gameObject.GetComponent<LoadSceneButton>().tableToLoad = pagesInfo[i].tableName;

            transform.GetChild(i).GetComponentsInChildren<Image>()[1].sprite = pagesInfo[i].image;

        }

        totalElements = transform.childCount;
        initialSize = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
    }

    bool hasSwiped = false, isChangingSize = false;
    Vector2 prevPos = Vector2.zero;
    Vector2 deltaPos;
    float inertia = 5;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    float dist = 0;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (prevPos == Vector2.zero) prevPos = Input.mousePosition;
            Vector2 currentPos = Input.mousePosition;
            dist = Mathf.Abs(currentPos.x - prevPos.x);
            deltaPos = (currentPos - prevPos).normalized;
            if (deltaPos.x != 0)
            {
                hasSwiped = true;
                inertia = 5;
            }
            prevPos = currentPos;
        }
        else
        {
            prevPos = Vector2.zero;
        }
        if (hasSwiped)
        {
            if(currentlyFocused == totalElements - 1 && deltaPos.x >0)
                MoveAllElements(deltaPos.x, dist);
            else if (currentlyFocused == 0 && deltaPos.x < 0)
                MoveAllElements(deltaPos.x, dist);
            else if(currentlyFocused >0 && currentlyFocused < totalElements -1)
                MoveAllElements(deltaPos.x, dist);

        }
        if (isChangingSize)
        {
            ChangeSize(transform.GetChild(currentlyFocused).GetComponent<RectTransform>());
        }
    }

    void MoveAllElements(float direction, float mag)
    {

        if (direction < 0) direction = -1;
        if (direction > 0) direction = 1;
        for (int i = 0; i < totalElements; i++)
        {
            mag = Mathf.Clamp(mag, 0.0f, scrollSpeed);
            RectTransform rectTrans = transform.GetChild(i).GetComponent<RectTransform>();
            Vector2 pos = rectTrans.anchoredPosition;
            rectTrans.anchoredPosition += new Vector2(direction * mag, 0);

            if (rectTrans.anchoredPosition.x < 60 && rectTrans.anchoredPosition.x > -60)
            {
                isChangingSize = true;
                transform.GetChild(i).GetComponentInChildren<Button>().interactable = true;
                currentlyFocused = i;
                if (rectTrans.anchoredPosition.x< 5 && rectTrans.anchoredPosition.x >-5)
                {
                    inertia = -1;
                    return;
                }
            }
            else
            {
                rectTrans.sizeDelta = initialSize;
                transform.GetChild(i).GetComponentInChildren<Button>().interactable = false;
            }


        }
        inertia -= Time.deltaTime;
        if (inertia < 0)
        {
            inertia = 5;
            hasSwiped = false;
        }
    }

    void ChangeSize(RectTransform trans)
    {
        if(trans.sizeDelta.x < initialSize.x + initialSize.x*0.2f)
            trans.sizeDelta += new Vector2(Time.deltaTime*Mathf.Clamp(dist, 0, 10)*500, Time.deltaTime*Mathf.Clamp(dist, 0, 10)*500);

    }


}
