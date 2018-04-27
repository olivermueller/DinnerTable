using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadSceneButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{

    public string tableToLoad;

    bool isDragging = false;


    public void BackToMain()
    {
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

            XAPIStatement statement = new XAPIStatement("Peter Sommerauer", "mailto:peter@sommerauer.li", "downloaded", "http://id.tincanapi.com/verb/downloaded", "http://activitystrea.ms/schema/1.0/application", "application", "downloaded Quiz-App");
            Settings.instance.SEND(statement);

            SceneManager.LoadScene(Settings.instance.gameScene);
            
        }
    }

   

}
