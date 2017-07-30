using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortress_power : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll){
		if (coll == true && coll.gameObject.tag == "Grunt") {
			Destroy (coll.gameObject, 2.5f);
			GameObject.Find ("Canvas").GetComponent<GameManager> ().fortressPower -= 100;
		}
	}
}