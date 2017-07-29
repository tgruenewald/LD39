using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntSpawnPoint : MonoBehaviour {
	public float timeToNextSpawn = 1f;
	public float gruntSpeed = 3f;
	public Transform[] targetList;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
	}
	IEnumerator spawnTime() {
		yield return new WaitForSeconds(timeToNextSpawn);
		StartCoroutine (spawnTime ());
		var grunt = (GameObject) Instantiate(Resources.Load("prefab/grunt"), GetComponent<Transform>().position, GetComponent<Transform>().rotation) ;
		grunt.GetComponent<Grunt> ().targetList = targetList;
		grunt.GetComponent<Grunt> ().speed = gruntSpeed;

	}
	void Awake() {
		StartCoroutine (spawnTime ());
	}
	// Update is called once per frame
	void Update () {
		
	}
}
