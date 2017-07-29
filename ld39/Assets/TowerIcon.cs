using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIcon : MonoBehaviour {

	private Vector3 mousePosition;
	public float moveSpeed = 0.1f;
	public bool moveTower = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (moveTower) {
			mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = new Vector3 (mousePosition.x, mousePosition.y, transform.position.z);// Vector2.Lerp(transform.position, mousePosition, moveSpeed);
		}

	}

	void OnMouseDown()
	{
		moveTower = true;
	}

}
