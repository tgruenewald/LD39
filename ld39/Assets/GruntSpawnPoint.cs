using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntSpawnPoint : MonoBehaviour {

	[System.Serializable]
	public class Wave
	{
		public GameObject enemyPrefab;
		public int maxEnemies;
		public int spawnInterval;
		public int enemySpeed;
	}

	public Wave[] waves;
	public int timeBetweenWaves = 5;
	public int currentWave = 0;
		float lastSpawnTime;
	public int enemiesSpawned = 0;

	public float timeToNextSpawn = 1f;//THIS VARIABLE SHOULD BE CHANGED IN THE INSPECTOR FOR EACH WAVE, NOT HERE
	//public float gruntSpeed = 3f;

	public int timeBeforeFirstWave;
	public bool beginWaves = false;
	public bool spawningComplete = false;

	public bool prepareMusic = true;
	public bool attackMusic = false;

	public Transform[] targetList;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;


	}

	void Awake(){
		//StartCoroutine (StartWaves ());
	}


	// Update is called once per frame
	void Update () {

		if (GameObject.Find ("Canvas").GetComponent<GameManager>().beginLevel)
		{
			StartCoroutine (StartWaves ());
		}


		if (beginWaves)
		{
			if (currentWave < waves.Length){
				
				float timeInterval = Time.time - lastSpawnTime;
				float spawnInterval = waves [currentWave].spawnInterval;

				if ((currentWave == 0 && enemiesSpawned == 0)||((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || enemiesSpawned > 0 && timeInterval > spawnInterval) && enemiesSpawned < waves[currentWave].maxEnemies){
					SetMusic ("attack");
					lastSpawnTime = Time.time;
					StartCoroutine (spawnTime (waves[currentWave].enemyPrefab, waves[currentWave].enemySpeed));
					enemiesSpawned++;
				}

				if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Grunt") == null)
				{
					SetMusic ("prepare");
					currentWave++;
					enemiesSpawned  = 0;
					lastSpawnTime = Time.time;
				}
			}
			else if (GameObject.FindGameObjectWithTag("Grunt") == null)
			{
				SetMusic ("prepare");
				Debug.Log ("WAVES COMPLETE");
				spawningComplete = true;
			}
		}

	}//Update

	IEnumerator spawnTime(GameObject gruntPrefab, int gruntSpeed) {
		yield return new WaitForSeconds(timeToNextSpawn);
		//StartCoroutine (spawnTime ());
		var grunt = (GameObject) Instantiate(gruntPrefab, GetComponent<Transform>().position, GetComponent<Transform>().rotation) ;
		grunt.GetComponent<Grunt> ().targetList = targetList;
		grunt.GetComponent<Grunt> ().speed = gruntSpeed;

	}

	IEnumerator StartWaves(){
		yield return new WaitForSeconds (timeBeforeFirstWave);
		beginWaves = true;
	}

	void SetMusic(string musicType)
	{
		Debug.Log ("setting music: " + musicType);
		if (musicType == "prepare")
		{
			if (attackMusic)
			{
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().PlayAttackMusic ();
				attackMusic = false;
				prepareMusic = true;
			}

		}
			
		if (musicType == "attack")
		{
			if (prepareMusic)
			{
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().PlayPrepareMusic ();
				prepareMusic = false;
				attackMusic = true;
			}
		}
			
		
	}
}
