using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawnPoint : MonoBehaviour {

	public float timeToNextSpawn = 1f;
	public float windSpeed = 3f;
	public Transform[] targetList;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
	}
	IEnumerator spawnTime() {
		yield return new WaitForSeconds(timeToNextSpawn);
		StartCoroutine (spawnTime ());
		var grunt = (GameObject) Instantiate(Resources.Load("prefab/wind"), GetComponent<Transform>().position, GetComponent<Transform>().rotation) ;
		grunt.GetComponent<Wind> ().targetList = targetList;
		grunt.GetComponent<Wind> ().speed = windSpeed;

	}
	void Awake() {
		StartCoroutine (spawnTime ());
	}
}
