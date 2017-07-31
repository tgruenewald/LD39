using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMarkerPlacement : MonoBehaviour {
	private Vector3 mousePos;
	public GameObject origWindMill = null;
	int count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			count++;
			Debug.Log("WIND MARKER: mouse down");

		}
		if (count >= 2) {
			origWindMill.GetComponent<Tower> ().creatingMode = false;
			origWindMill.GetComponent<Tower> ().redirectMode = true;
		}
	}


}
