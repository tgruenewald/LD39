using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIcon : MonoBehaviour {

	private Vector3 mousePosition;
	public TowerButton TowerButton;
	public GameObject towerPrefab;
	public float moveSpeed = 0.1f;
	public bool moveTower = false;


	//public Transform towerIconTransform;
	void Awake() {
		TowerButton = GameObject.Find("Canvas").GetComponent<TowerButton>();
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}




	public RaycastHit RayFromCamera(Vector3 mousePosition, float rayLength)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		Physics.Raycast(ray, out hit, rayLength);
		return hit;
	}
}
