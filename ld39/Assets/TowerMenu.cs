using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerMenu : MonoBehaviour {

	public GameObject iconMenuPos;
	public Vector3 iconMenuPosition;
	public GameObject autoButton;
	public GameObject redirectButton;

	// Use this for initialization
	void Start () {
		iconMenuPosition = iconMenuPos.transform.position;
		iconMenuPosition = Camera.main.WorldToScreenPoint (iconMenuPosition);

		//autoButton = GameObject.FindGameObjectWithTag ("AutoButton");
		//redirectButton = GameObject.FindGameObjectWithTag ("RedirectButton");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter(){
		Debug.Log ("mouse entered tower collision zone");

		autoButton.gameObject.SetActive (true);
		redirectButton.gameObject.SetActive (true);
		GameObject towerMenu = GameObject.FindGameObjectWithTag("tower menu");
		towerMenu.transform.position = new Vector3 (iconMenuPosition.x, iconMenuPosition.y, transform.position.z);// Vector2.Lerp(transform.position, mousePosition, moveSpeed);

	}

	void OnMouseExit(){
		autoButton.gameObject.SetActive (false);
		redirectButton.gameObject.SetActive (false);
	}
}
