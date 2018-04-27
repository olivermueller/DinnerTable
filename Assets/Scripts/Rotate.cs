using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    Rigidbody rb;
    public bool hasTwoAxisRotation;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    float momentum = 0, speed = 0.3f;
    Vector2 touchDeltaPosition;
    void Update()
    {

        if (hasTwoAxisRotation)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Get movement of the finger since last frame
                touchDeltaPosition = -Input.GetTouch(0).deltaPosition;
                //gameObject.GetComponent<Rigidbody>().AddTorque(Vector3.right * 5 * touchDeltaPosition.y);
                //gameObject.GetComponent<Rigidbody>().AddTorque(Vector3.up * 5 * touchDeltaPosition.x);
                //gameObject.transform.RotateAround(gameObject.transform.position, Vector3.right, -touchDeltaPosition.y * speed);
                //gameObject.transform.RotateAround(gameObject.transform.position, Vector3.up, -touchDeltaPosition.x * speed);
                //(new Vector3(touchDeltaPosition.y * speed, 0, touchDeltaPosition.x *  speed));
                rb.AddTorque(Camera.main.transform.right * -touchDeltaPosition.x);
                rb.AddTorque(Camera.main.transform.up * touchDeltaPosition.y);
            }
        }
        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Get movement of the finger since last frame
                touchDeltaPosition = -Input.GetTouch(0).deltaPosition;
                momentum = 1;
                //rb.AddTorque(Camera.main.transform.right * -touchDeltaPosition.x);
                //rb.AddTorque(Camera.main.transform.right * touchDeltaPosition.y);
            }
            if (momentum > 0)
            {


                gameObject.transform.Rotate(new Vector3(0, 0, touchDeltaPosition.x * momentum * speed));
                momentum -= Time.deltaTime;
            }
            else
            {
                momentum = 0;
            }
        }



    }
}
