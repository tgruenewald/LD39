using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour {

	public GameObject towerIcon;
	public GameObject towerPrefab;
	private Vector3 mousePos;
	public float moveSpeed = 0.1f;
	public bool moveTower = false;
	public bool creating = false;

	public GameObject placingTowerIcon = null;
	// Use this for initialization

	void Start () {
		
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
				PlaceTower ();
				Debug.Log ("tower placed");
			}
			//moveTower = true;
		}

	}


	public void TaskOnClick(){
		Debug.Log ("clicked on tower button");

		if (creating){
			Debug.Log ("Canceled. Place unit first.");
		}
			
		else {
			CreateTowerIcon (Input.mousePosition);

		}
	}

	void PlaceTower ()
	{
		CreateTower (Input.mousePosition);
		creating = false;
	}

	public void CreateTower(Vector2 mousePosition)
	{
		RaycastHit hit = RayFromCamera(mousePosition, 1000.0f);
		Vector3 objectPos = Camera.main.ScreenToWorldPoint (mousePosition);
		GameObject.Instantiate(towerPrefab, placingTowerIcon.transform.position, Quaternion.identity);
		Destroy (placingTowerIcon);
	}

	public void CreateTowerIcon(Vector2 mousePosition)
	{
		RaycastHit hit = RayFromCamera(mousePosition, 1000.0f);
		Vector3 objectPos = Camera.main.ScreenToWorldPoint (mousePosition);
		placingTowerIcon = Instantiate(towerIcon, objectPos, Quaternion.identity) as GameObject;
		creating = true;
	}

	public RaycastHit RayFromCamera(Vector3 mousePosition, float rayLength)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		Physics.Raycast(ray, out hit, rayLength);
		return hit;
	}
}
