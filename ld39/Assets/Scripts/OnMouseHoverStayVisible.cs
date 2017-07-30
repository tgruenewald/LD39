using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseHoverStayVisible : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver()
	{
		Debug.Log ("moused over");
		this.gameObject.SetActive (true);
	}

	void OnMouseExit()
	{
		this.gameObject.SetActive (false);
	}
}
