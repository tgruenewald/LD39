using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMarkerPlacement : MonoBehaviour {
	private Vector3 mousePos;
	public GameObject origWindMill = null;
	bool hasStarted = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted && Input.GetMouseButtonDown(0)) {
			hasStarted = true;

			GetComponent<SpriteRenderer> ().enabled = false;
			Debug.Log("WIND MARKER: mouse down");
			GameObject[] selectedTowers;
			selectedTowers = GameObject.FindGameObjectsWithTag("selected_tower");

			foreach (GameObject selectedTower in selectedTowers) {
				Debug.Log ("send wind");
				selectedTower.GetComponent<Tower> ().creatingMode = false;
				selectedTower.GetComponent<Tower> ().redirectMode = true;
				selectedTower.GetComponent<Tower> ().unhighlight ();
				selectedTower.tag = "tower";
			}
		}
	
	}


}
