﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

    public GameObject music;
	// Use this for initialization
	void Start () {
        Invoke("myMethod", 5f);
	}

    // Update is called once per frame
    void myMethod() {
        SceneManager.LoadScene("Controls Page");
        DontDestroyOnLoad(music);
    }
            
}
