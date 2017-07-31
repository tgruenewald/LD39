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
	public string warningString;
	public Slider energyBar;
	public const int AUTOMATIC = 0;
	public const int REDIRECT = 1;


	public bool beginLevel = false;
	public bool levelComplete = false;
	public bool gameOver = false;

	void Start(){
		StartCoroutine (BlinkText ());
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
		}
		foreach (GameObject selectedTower in selectedTowers) {
			selectedTower.GetComponent<Tower> ().change_mode (mode, windMarkers);
		}
	}

	void Awake()
	{
		beginLevel = true;
	}

	void Update(){

		if (Input.GetKeyDown(KeyCode.W))
		{
			SceneManager.LoadScene ("scene2");

		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			SceneManager.LoadScene ("title");

		}

		if (!levelComplete)
		{
			if (Input.GetKeyDown (KeyCode.A)) {
				Debug.Log ("You pressed A");
				change_selected_towers (AUTOMATIC);
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				Debug.Log ("You pressed D");
				change_selected_towers (REDIRECT);
			}
			if (fortressPower <= warningLevel)
			{
				warningText.enabled = true;
				warningString = "WARNING: RUNNING OUT OF POWER";
			}
			else {
				warningText.enabled = false;
			}

			if (fortressPower <= 0)
			{
				GameOver ();
			}

			powerAmountText.text = "Main Power: " + fortressPower;
			UpdateEnergyBar ();

			CheckIfLevelComplete ();
		}


	}//Update

	public IEnumerator BlinkText(){
		while (true)
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
		energyBar.value = fortressPower;
		/*if(energyBar.value < 100){
			energyBar.color = Color.Lerp (Color.red, Color.white, Mathf.PingPong (Time.time, 1f));
		}
		else{
			energyBar.image.color = Color.Lerp (Color.red, Color.blue, energyBar.value);

		}*/
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
			warningString = "YOU SHOT THE BREEZE! YOU WIN!";
		}
	}

	public void GameOver(){
		StopCoroutine ("NaturalDepletion");
		warningString = "GAME OVER: YOU RAN OUT OF POWER";
		fortressPower = 0;
		gameOver = true;
	}
}
