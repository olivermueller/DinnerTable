using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInScene : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
