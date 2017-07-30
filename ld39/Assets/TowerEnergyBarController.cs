using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerEnergyBarController : MonoBehaviour {

	public Slider towerEnergyBar;
	public int placeholderEnergy = 100;	
	public Vector3 iconMenuPosition;

	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		towerEnergyBar = GetComponent<Slider> ();
		transform.position = new Vector3 (iconMenuPosition.x, iconMenuPosition.y, transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		towerEnergyBar.value = placeholderEnergy;
	}
}
