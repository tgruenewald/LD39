using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawnPoint : MonoBehaviour {

	public float timeToNextWind = 1f;
	public float timeBeforeStarting = 1f;
	public float timeToPauseWind = 1f;
	bool beginWind = false;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
	}

	IEnumerator dotimeBeforeStarting() {
		yield return new WaitForSeconds(timeBeforeStarting);
		beginWind = true;
		StartCoroutine (dotimeToPauseWind ());

	}
	IEnumerator dotimeToNextSpawn() {
		yield return new WaitForSeconds(Random.Range(0f,timeToNextWind));
		StartCoroutine (dotimeToNextSpawn ());
		if (beginWind) {
			make_wind ();			
		}
	}

	IEnumerator dotimeToPauseWind() {
		yield return new WaitForSeconds(timeToPauseWind);
		StartCoroutine (dotimeBeforeStarting ());
		beginWind = false;

	}
	void Awake() {
		StartCoroutine (dotimeToNextSpawn ());

		StartCoroutine (dotimeBeforeStarting ());
	}
	void make_wind() {

		if (true)
		{
			var grunt = (GameObject) Instantiate(Resources.Load("prefab/wind"), GetComponent<Transform>().position, GetComponent<Transform>().rotation) ;
			grunt.GetComponent<Wind> ().targetList = gameObject.GetComponentInParent<WindSpawnPointParent>().targetList;
			grunt.GetComponent<Wind> ().speed = gameObject.GetComponentInParent<WindSpawnPointParent>().windSpeed;
		}



	}
}
