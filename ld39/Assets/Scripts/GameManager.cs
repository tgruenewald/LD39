using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public int fortressPower = 1000;
	public int depletionRate = 25;
	public int numberOfTowers = 1;

	public Text warningText;
	public Text powerAmountText;
	public string warningString;

	void Start(){
		StartCoroutine (BlinkText ());


		StartCoroutine (NaturalDepletion());
	}
	void Update(){

		if (fortressPower <= 100)
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

	public void GameOver(){
		StopCoroutine ("NaturalDepletion");
		warningString = "YOU RAN OUT OF POWER";
		fortressPower = 0;
	}
}
