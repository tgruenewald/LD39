using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour {

	public GameObject towerIcon;
	public GameObject towerPrefab;

	public Text buildingWarningText;
	public Text cancelText;

	private Vector3 mousePos;
	public float moveSpeed = 0.1f;
	public bool moveTower = false;
	public bool creating = false;
	public bool dangerousBuild = false;
	public bool creatingDisabled = false;
	public int towerCost = 100;
	public GameObject placingTowerIcon = null;
	// Use this for initialization

	void Start () {
		cancelText.text = "<-- Click to build a tower.\nTower Cost: " + towerCost + " Power.\nHot keys: A - Automatic fire, D - direct wind";
	}
	
	// Update is called once per frame
	void Update () {
		if (creating) {
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);
			placingTowerIcon.transform.position = new Vector3 (mousePos.x, mousePos.y, transform.position.z);// Vector2.Lerp(transform.position, mousePosition, moveSpeed);
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (creating)
			{
				//if building would be dangerous
				if(GetComponent<GameManager>().fortressPower < towerCost){
					buildingWarningText.text = "Not enough Power to build!";
				//	buildingWarningText.enabled = true;

				}
				else if(creatingDisabled)
				{
					buildingWarningText.enabled = true;
					buildingWarningText.text = "Invalid place to build tower.";
				}
				else if (dangerousBuild)
				{
					dangerousBuild = false;
					PlaceTower ();
				}
				else if ((GetComponent<GameManager>().fortressPower-towerCost) <= GetComponent<GameManager>().warningLevel)
				{
					buildingWarningText.text = "POWER WILL BE DANGEROUSLY LOW!\n To build anyway, click again.";
					buildingWarningText.enabled = true;
					dangerousBuild = true;
				}
				else{
					PlaceTower ();

				}
				//if building would end game
				//else
				//Debug.Log ("tower placed");
			}
			//moveTower = true;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			dangerousBuild = false;
			buildingWarningText.enabled = false;
			CancelTower ();
		}

	}


	public void TaskOnClick(){
		Debug.Log ("clicked on tower button");

		if (creating){
			Debug.Log ("Canceled. Place unit first.");
		}
			
		else {
			CreateTowerIcon (Input.mousePosition);
			cancelText.text = "To cancel, press space bar.\nTower Cost: " + towerCost + " Power";
		}
	}

	void PlaceTower ()
	{
		CreateTower (Input.mousePosition);
		buildingWarningText.enabled = false;
		cancelText.text = "<-- Click to build a tower.\nTower Cost: " + towerCost + " Power";
		creating = false;
	}

	public void CreateTower(Vector2 mousePosition)
	{
		RaycastHit hit = RayFromCamera(mousePosition, 1000.0f);
		Vector3 objectPos = Camera.main.ScreenToWorldPoint (mousePosition);
		GameObject.Instantiate(towerPrefab, placingTowerIcon.transform.position, Quaternion.identity);
		GetComponent<GameManager> ().fortressPower -= towerCost;
		Destroy (placingTowerIcon);
	}

	public void CancelTower(){
		creating = false;
		Destroy (placingTowerIcon);
		cancelText.text = "<-- Click to build a tower.\nTower Cost: " + towerCost + " Power";
	}

	public void CreateTowerIcon(Vector2 mousePosition)
	{
		RaycastHit hit = RayFromCamera(mousePosition, 1000.0f);
		Vector3 objectPos = Camera.main.ScreenToWorldPoint (mousePosition);
		placingTowerIcon = Instantiate(towerIcon, objectPos, Quaternion.identity) as GameObject;
		creating = true;

		if(GetComponent<GameManager>().fortressPower < towerCost){
			buildingWarningText.text = "Not enough Power to build!";
			buildingWarningText.enabled = true;

		} 
	}

	public RaycastHit RayFromCamera(Vector3 mousePosition, float rayLength)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		Physics.Raycast(ray, out hit, rayLength);
		return hit;
	}
}
