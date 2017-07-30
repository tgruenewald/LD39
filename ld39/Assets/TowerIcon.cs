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
	public float maxDistance = 5;

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
		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		Debug.Log ("touched2");
		if (Vector3.Distance(other.transform.position,this.transform.position) < maxDistance)
		{
			isTouching = true;
			Debug.Log ("touched");
		}
		else{
			isTouching = false;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.gameObject.tag == "tower_meredith") {
			Debug.Log ("collision with tower");
		}
	
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "tower_meredith") {
			Debug.Log ("leaving collision w/ tower");
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
