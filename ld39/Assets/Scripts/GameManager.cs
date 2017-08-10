using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public int fortressPower = 1000;
	public int depletionRate = 15;
	public int numberOfTowers = 1;
	public int warningLevel = 250;
	public Text warningText;
	public Text powerAmountText;
	public Text currentWaveText;

	public string warningString;
	public Slider energyBar;
	public const int AUTOMATIC = 0;
	public const int REDIRECT = 1;
	public int timeBeforeWaves = 15;

	public bool beginLevel = false;
	public bool levelComplete = false;
	public bool gameOver = false;
	public int timeToNextLevel = 5;

	public int currentLevel;

	void Start(){
		StartCoroutine (NaturalDepletion());

		energyBar.value = 1000;
	}
	void change_selected_towers(int mode) {
		GameObject[] selectedTowers;
		GameObject[] windMarkers = null;
	    selectedTowers = GameObject.FindGameObjectsWithTag("selected_tower");
		if (mode == REDIRECT && selectedTowers.Length > 0) {
			// create just one wind marker
			windMarkers = selectedTowers [0].GetComponent<Tower> ().CreateWindMarker (Input.mousePosition);
			selectedTowers [0].GetComponent<Tower> ().redirectSprite.enabled = true;
		}
		if (mode == AUTOMATIC) {
			selectedTowers [0].GetComponent<Tower> ().redirectSprite.enabled = false;
		}
		foreach (GameObject selectedTower in selectedTowers) {
			selectedTower.GetComponent<Tower> ().change_mode (mode, windMarkers);
		}
	}

	void Awake()
	{
		
		currentLevel = int.Parse(SceneManager.GetActiveScene ().name.Substring (5, 1));
		if (currentLevel > 0) {
			StartCoroutine (WarnBeforeWaves ());
		}
	}

	void Update(){

		if (Input.GetKeyDown(KeyCode.W))
		{
			SceneManager.LoadScene ("level" + currentLevel);

		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			SceneManager.LoadScene ("title");

		}
		if (fortressPower <= 0 || gameOver)
		{
			GameOver ();
		}



		if (!levelComplete && !gameOver)
		{
			if (Input.GetKeyDown (KeyCode.A)) {
				Debug.Log ("You pressed A");
				change_selected_towers (AUTOMATIC);
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				Debug.Log ("You pressed D");
				GameState.DisplayNextHelp (4);
				change_selected_towers (REDIRECT);
			}
			if (fortressPower <= warningLevel && !gameOver && !levelComplete)
			{
				warningText.enabled = true;
				warningString = "WARNING: RUNNING OUT OF POWER";
			}
			else if (beginLevel){
				warningText.enabled = false;
			}




			CheckIfLevelComplete ();
		}
		UpdateEnergyBar ();


	}//Update

	public IEnumerator BlinkText(){
		while (!levelComplete && !gameOver)
		{
			warningText.text = "";
			yield return new WaitForSeconds (.5f);
			warningText.text = warningString;
			yield return new WaitForSeconds (.5f);
		}
	}

	public IEnumerator NaturalDepletion(){
		while (true)
		{
			fortressPower -= 1;
			yield return new WaitForSeconds (1f/(depletionRate * numberOfTowers));
		}
	}

	public void UpdateEnergyBar(){
		powerAmountText.text = "Main Power: " + fortressPower;

		energyBar.value = fortressPower;
		/*if(energyBar.value < 100){
			energyBar.color = Color.Lerp (Color.red, Color.white, Mathf.PingPong (Time.time, 1f));
		}
		else{
			energyBar.image.color = Color.Lerp (Color.red, Color.blue, energyBar.value);

		}*/
	}
	public IEnumerator WarnBeforeWaves()
	{
		Debug.Log ("warning");
		while (timeBeforeWaves > 0)
		{
			if (timeBeforeWaves == 1)
			{
				warningText.text = "Waves will begin in " + timeBeforeWaves + " second";
			}
			else{
				warningText.text = "Waves will begin in " + timeBeforeWaves + " seconds";
			}
			warningText.enabled = true;
			timeBeforeWaves--;
			yield return new WaitForSeconds(1f);
		}
		beginLevel = true;
		warningString = "";
		StartCoroutine (BlinkText ());


	}
	public void CheckIfLevelComplete()
	{
		bool allGruntsDead = true;
		GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag ("gruntSpawnPoint");
		foreach (GameObject spawnPoint in spawnPoints)
		{

			if (spawnPoint.GetComponent<GruntSpawnPoint>().spawningComplete == false)
				allGruntsDead = false;
		}
		if (allGruntsDead)
		{
			levelComplete = true;
			warningText.enabled = true;
			if (currentLevel == 5)
			{
				warningString = "ALL FIVE LEVELS CLEARED!\n YOU WIND THE GAME!";
				GameObject.Find ("Canvas").GetComponent<TowerButton> ().buildingWarningText.enabled = false;
				warningText.text = warningString;
				warningText.enabled = true;

			}
			else{
				
				warningString = "WELL DONE! YOU SHOT THE BREEZE!";
				warningText.text = warningString;
				warningText.enabled = true;

				StartCoroutine (LoadNextLevel ());
			}

		}
	}

	IEnumerator LoadNextLevel()
	{
		while (timeToNextLevel > 0)
		{
			yield return new WaitForSeconds(1f);
			GameObject.Find ("Canvas").GetComponent<TowerButton> ().buildingWarningText.text = "Loading next level in " + timeToNextLevel;
			GameObject.Find ("Canvas").GetComponent<TowerButton> ().buildingWarningText.enabled = true;
			timeToNextLevel--;
		}
		currentLevel++;
		SceneManager.LoadScene ("level" + currentLevel);


	}

	public void GameOver(){
		gameOver = true;
		StopCoroutine ("NaturalDepletion");
		Debug.Log ("Game Over");
		warningString = "GAME OVER: YOU RAN OUT OF POWER";
		warningText.text = warningString;
		warningText.enabled = true;
		fortressPower = 0;
	}
}
