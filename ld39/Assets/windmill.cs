using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windmill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.tag == "wind") {
			// put check here to not collect your own wind
			if (coll.gameObject.GetComponent<Wind> ().windId != gameObject.transform.parent.gameObject.GetInstanceID()) {
				GetComponentInParent<Tower> ().power++;
				DestroyObject (coll.gameObject);
			}

		}
			
	}
}
