using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIcon : MonoBehaviour {

	private Vector3 mousePosition;
	public TowerButton TowerButton;
	public GameObject towerPrefab;
	public float moveSpeed = 0.1f;
	public bool moveTower = false;
	public bool isTouching = false;
	public float maxDistance = 1;

	public int collisionCount = 0;

	//public Transform towerIconTransform;
	void Awake() {
		Renderer rend = GetComponent<Renderer> ();
		TowerButton = GameObject.Find("Canvas").GetComponent<TowerButton>();
	}
	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		if (collisionCount == 0)
		{
			GameObject.Find ("Canvas").GetComponent<TowerButton> ().creatingDisabled = false;
			isTouching = true;

		}
		else
		{
			GameObject.Find ("Canvas").GetComponent<TowerButton> ().creatingDisabled = true;
			isTouching = false;

		}
			
	}

	/*void OnTriggerStay2D(Collider2D other)
	{
		if (Vector3.Distance(other.transform.position,this.transform.position) < maxDistance)
		{
			Debug.Log ("touched");
			if (other.gameObject.tag == "tower_meredith" || other.gameObject.tag == "border") {
				isTouching = true;
				GameObject.Find ("Canvas").GetComponent<TowerButton> ().creatingDisabled = true;

			}

		}
		else{
			if (other.gameObject.tag == "tower_meredith" || other.gameObject.tag == "border") {
				isTouching = false;

			}

		}
	}*/

	void OnTriggerEnter2D(Collider2D coll){

		//gameObject.GetComponent<SpriteRenderer> ().material.color = new Color32(255,255,255,150);
		Debug.Log(coll.gameObject.tag);

		if (coll.gameObject.tag == "tower" || coll.gameObject.tag == "border") {
			collisionCount++;


		}
	
	}

	void OnTriggerExit2D(Collider2D coll){

		//gameObject.GetComponent<SpriteRenderer> ().material.color = Color.green;
		if (coll.gameObject.tag == "tower"|| coll.gameObject.tag == "border") {
			collisionCount--;
		}
		Debug.Log ("exit collision "+ coll.gameObject.tag);

	}


	public RaycastHit RayFromCamera(Vector3 mousePosition, float rayLength)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		Physics.Raycast(ray, out hit, rayLength);
		return hit;
	}
}
