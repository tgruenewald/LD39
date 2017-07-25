using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// https://www.youtube.com/watch?v=QkisHNmcK7Y
public class ScrollingBackground : MonoBehaviour {

	public float backgroundSize;
	public float parallaxSpeed;
	public bool scrolling, parallax;

	private Transform cameraTransform;
	private Transform[] layers;
	private float viewZone = 3;
	private int leftIndex;
	private int rightIndex;
	private float lastCamaraX;



	private void Start() {
		cameraTransform = Camera.main.transform;
		lastCamaraX = cameraTransform.position.x;
		layers = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			layers [i] = transform.GetChild (i);
		}
		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}

	private void Update() {
		if (parallax) {
			float deltaX = cameraTransform.position.x - lastCamaraX;
			transform.position += Vector3.right * (deltaX * parallaxSpeed);			
		}

		lastCamaraX = cameraTransform.position.x;
		if (scrolling) {
			if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone)) {
				ScrollLeft ();
			}
			if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone)) {
				ScrollRight ();
			}			
		}

	}

	private void ScrollLeft() {
		int lastRight = rightIndex;
		layers [rightIndex].position = Vector3.right * (layers [leftIndex].position.x - backgroundSize);
		leftIndex = rightIndex;
		rightIndex--;

		if (rightIndex < 0)
			rightIndex = layers.Length - 1;
	}
	private void ScrollRight() {
		int lastRight = leftIndex;
		layers [leftIndex].position = Vector3.right * (layers [rightIndex].position.x + backgroundSize);
		rightIndex = leftIndex;
		leftIndex++;

		if (leftIndex == layers.Length)
			leftIndex = 0;
	}

}
