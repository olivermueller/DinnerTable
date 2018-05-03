using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadSceneButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{

    public string tableToLoad;
    public string tableTitle;
    bool isDragging = false;


    public void BackToMain()
    {
        XAPIStatement statement = new XAPIStatement(Settings.username, "mailto:" + Settings.email, "selected", "https:∕∕w3id.org∕xapi∕dod-isd∕verbs∕chose", "http:∕∕adlnet.gov∕expapi∕activities∕DinnerTable", "Main Screen", "Went to main screen");
        Settings.instance.SEND(statement);
        SceneManager.LoadScene(Settings.instance.mainMenuScene);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(CheckDrag());
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(SetDrag());
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    IEnumerator SetDrag()
    {
        yield return new WaitForSeconds(0.2f);
        isDragging = false;
    }


    IEnumerator CheckDrag()
    {
        yield return new WaitForSeconds(0.2f);
        if (isDragging == false)
        {
            Settings.instance.tableToLoad = tableToLoad;
            Settings.instance.currentScenario = tableTitle;
            XAPIStatement statement = new XAPIStatement(Settings.username, "mailto:" + Settings.email, "selected", "https:∕∕w3id.org∕xapi∕dod-isd∕verbs∕chose", "http:∕∕adlnet.gov∕expapi∕activities∕DinnerTable", "theme:" + tableTitle, "Started Dinner Table");
            Settings.instance.SEND(statement);
            SceneManager.LoadScene(Settings.instance.gameScene);
            
        }
    }

   

}
