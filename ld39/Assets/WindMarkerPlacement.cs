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

	public IEnumerator WaitForWindToHitTutorial(){
		yield return new WaitForSeconds (4f);

		if (GameObject.Find("tutorial_powerTower").GetComponent<Tower>().power > 0) {
			// yay
			Debug.Log ("hit target");
			GameState.DisplayNextHelp (6);
		}
		else {
			Debug.Log ("missed target");
			GameState.DisplayNextHelp (5);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted && Input.GetMouseButtonDown(0)) {
			hasStarted = true;
			AudioSource audio = gameObject.AddComponent<AudioSource> ();
			audio.pitch = 3f;
			audio.PlayOneShot((AudioClip)Resources.Load("Music/cannon"), 1f);
			if (GameState.isTutorial) {
				StartCoroutine (WaitForWindToHitTutorial ());
			}
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
