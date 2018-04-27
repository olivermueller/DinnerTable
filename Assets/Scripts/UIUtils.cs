using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUtils : MonoBehaviour {

    public GameObject displayedObject;
    public GameObject displayedObjectPrefab;
    public GameObject closedButton, previewButton, backgroundPlane, instructionPanel, doneButton;
    public float ANIMATION_SPEED = 1;
    bool animateBackground = false;
    private ChangeCutlery cutlery;
    private GameObject[] planes;
    public void Update()
    {
        backgroundPlane.transform.position = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 25));
        if (animateBackground)
        {
            if (backgroundPlane.transform.localScale.x < 25) backgroundPlane.transform.localScale += new Vector3(ANIMATION_SPEED, ANIMATION_SPEED, ANIMATION_SPEED);
        }
        else
        {
            if (backgroundPlane.transform.localScale.x > 1) backgroundPlane.transform.localScale -= new Vector3(ANIMATION_SPEED, ANIMATION_SPEED, ANIMATION_SPEED);
        }
        if(!cutlery)
            cutlery = FindObjectOfType<ChangeCutlery>();
        else
        {
            if (cutlery.index > 1)
            {
                previewButton.GetComponent<Button>().interactable = true;
            }
            else
                previewButton.GetComponent<Button>().interactable = false;
        }
    }

    public void ShowObject()
    {
        //displayedObject = o;
        previewButton.SetActive(false);
        doneButton.SetActive(false);
        instructionPanel.SetActive(false);

        cutlery.gameObject.SetActive(false);
        planes = GameObject.FindGameObjectsWithTag("Plane");
        foreach (var plane in planes)
        {
            plane.SetActive(false);
        }
        closedButton.SetActive(true);
        Invoke("CreateObject", 0.3f);
    }

    public void CreateObject()
    {
        displayedObject = Instantiate(displayedObjectPrefab, (transform.position + transform.forward / 2.0f) - new Vector3(0, 0.1f, 0), displayedObjectPrefab.transform.rotation);
        displayedObject.transform.parent = transform;
    }

    public void ActivateUI()
    {
        previewButton.SetActive(true);
        doneButton.SetActive(true);
        instructionPanel.SetActive(true);
        cutlery.gameObject.SetActive(true);
        foreach (var plane in planes)
        {
            plane.SetActive(true);
        }
    }

    public void DestroyObject()
    {
        Destroy(displayedObject);
        closedButton.SetActive(false);
    }

    public void RemoveObject()
    {
        Invoke("DestroyObject", 0.3f);
        Invoke("ActivateUI", 0.5f);
    }

    public void AnimateBackground(bool animate)
    {
        animateBackground = animate;        
    }
}
