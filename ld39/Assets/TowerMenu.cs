using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerMenu : MonoBehaviour {

	public GameObject iconMenuPos;
	public Vector3 iconMenuPosition;
	public GameObject autoButton;
	public GameObject redirectButton;
	public GameObject towerMenu;

	// Use this for initialization
	void Start () {
		
		iconMenuPosition = iconMenuPos.transform.position;
		iconMenuPosition = Camera.main.WorldToScreenPoint (iconMenuPosition);
	}

	void Awake(){
		

		towerMenu = GameObject.FindGameObjectWithTag ("tower_menu");
		autoButton = GameObject.FindGameObjectWithTag ("AutoButton");
		redirectButton = GameObject.FindGameObjectWithTag ("RedirectButton");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter(){
		autoButton.gameObject.SetActive (true);
		redirectButton.gameObject.SetActive (true);
		GetComponentInParent<Tower> ().mouseCounter++;
		towerMenu.transform.position = new Vector3 (iconMenuPosition.x, iconMenuPosition.y, transform.position.z);// Vector2.Lerp(transform.position, mousePosition, moveSpeed);

	}

	void OnMouseExit(){
		GetComponentInParent<Tower> ().mouseCounter--;
		Debug.Log ("mouse exiting tower collision zone");
		towerMenu.transform.position = new Vector3 (3000,3000, transform.position.z);// Vector2.Lerp(transform.position, mousePosition, moveSpeed);

	}
}
