﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clickStart() {
		SceneManager.LoadScene ("level1");
	}

	public void clickCredits() {
		SceneManager.LoadScene ("credits");
	}
}
