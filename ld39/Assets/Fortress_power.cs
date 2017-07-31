using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortress_power : MonoBehaviour {
	AudioSource audio;
	IEnumerator flash() {
		GetComponent<SpriteRenderer> ().material.SetColor("_Color", Color.red);

		yield return new WaitForSeconds(.1f);
		GetComponent<SpriteRenderer> ().material.SetColor("_Color", Color.white);

	}
	void Start() {
		audio = gameObject.AddComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll == true && coll.gameObject.tag == "Grunt") {
			audio.PlayOneShot((AudioClip)Resources.Load("Music/grunt-castle"), .5f);
			Destroy (coll.gameObject, 2.5f);
			StartCoroutine (flash ());
			GameObject.Find ("Canvas").GetComponent<GameManager> ().fortressPower -= 100;
		}
	}
}