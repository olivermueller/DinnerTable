using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{
    public ExtensionMethod.Language buttonLanguage;
    public bool isUsed = false;
    public GameObject subMenu;

    bool changeSize=false;
    Vector2 from, to; 
    public void ChangeLanguage()
    {
        if (isUsed)
        {
            GameObject newLang = subMenu.transform.parent.GetChild(0).gameObject;
            Vector2 currentSize = newLang.transform.GetComponent<RectTransform>().sizeDelta;
            //subMenu.SetActive(!subMenu.activeInHierarchy);
            if (!subMenu.activeInHierarchy)
            {
                subMenu.SetActive(true);
                for (int i = 0; i < subMenu.transform.childCount; i++)
                {
                    subMenu.transform.GetChild(i).GetComponent<Animator>().SetTrigger("animateUp");
                }
                newLang.transform.GetComponent<RectTransform>().sizeDelta = currentSize / 2;
            }
            else
            {
                newLang.transform.GetComponent<RectTransform>().sizeDelta = currentSize * 2;
                for (int i = 0; i < subMenu.transform.childCount; i++)
                {
                    subMenu.transform.GetChild(i).GetComponent<Animator>().SetTrigger("animateDown");
                }
                StartCoroutine(WaitForAnimation());
                
            }
        }
        else
        {
            isUsed = false;
            GameObject newLang = subMenu.transform.parent.GetChild(0).gameObject;
            XAPIStatement statement = new XAPIStatement(Settings.username, "mailto:" + Settings.email, "selected", "https:∕∕w3id.org∕xapi∕dod-isd∕verbs∕chose", "http:∕∕adlnet.gov∕expapi∕activities∕DinnerTable", "Language:" + buttonLanguage.ToString(), "Started Dinner Table");
            Settings.instance.SEND(statement);


            LanguageButton langButtonScript = newLang.GetComponent<LanguageButton>();
            Sprite spriteObj = newLang.GetComponent<Image>().sprite;
            langButtonScript.isUsed = true;


            ExtensionMethod.Language oldLang = langButtonScript.buttonLanguage;
            Sprite oldSprite = spriteObj;

            langButtonScript.buttonLanguage = this.buttonLanguage;
            newLang.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;

            this.buttonLanguage = oldLang;
            this.GetComponent<Image>().sprite = oldSprite;


            ExtensionMethod.currentLanguage = newLang.GetComponent<LanguageButton>().buttonLanguage;
            Settings.instance.ChangeLanguage();
            Vector2 currentSize = newLang.transform.GetComponent<RectTransform>().sizeDelta;
            newLang.transform.GetComponent<RectTransform>().sizeDelta = currentSize * 2;
            for (int i = 0; i < subMenu.transform.childCount; i++)
            {
                subMenu.transform.GetChild(i).GetComponent<Animator>().SetTrigger("animateDown");
            }
            StartCoroutine(WaitForAnimation());


        }
    }

    IEnumerator WaitForAnimation()
    {
        //yield return new WaitForSeconds(0.5f);
        GetComponent<Button>().interactable = false;
        yield return new WaitUntil(()=>subMenu.transform.GetChild(subMenu.transform.childCount -1).GetComponent<RectTransform>().sizeDelta.x<=1);

        GetComponent<Button>().interactable = true;
        for (int i = 0; i < subMenu.transform.childCount; i++)
        {
            subMenu.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(55, 55);
        }

        subMenu.SetActive(false);
    }



   

}
