using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class GameState
{
	
	public static bool isTutorial = false;
	public static int lastNum = 0;
	public static bool EndTutorial() {
		if (isTutorial && lastNum >= 6) {
			GameObject.Find ("text_" + lastNum).GetComponent<SpriteRenderer> ().enabled = false;
			isTutorial = false;
			return true;

		}
		return false;
	}
	public static void DisplayNextHelp(int tutorial_num) {
		if (GameState.isTutorial && !EndTutorial()) {
			
			if (tutorial_num > 1) {
				GameObject.Find ("text_" + (lastNum)).GetComponent<SpriteRenderer> ().enabled = false;
			}

			lastNum = tutorial_num;
			GameObject.Find ("text_" + tutorial_num).GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

//	public static bool towerColliderOn = true;

}