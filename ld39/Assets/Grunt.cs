using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour {
	public Transform[] targetList;// =new Transform[];
	public Transform target;
	public float speed = 3f;
	public int nextTargetIndex = 0;
	// Use this for initialization
	void Start () {
		target = targetList[nextTargetIndex];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		float step = speed * Time.deltaTime;
		float distance = Vector3.Distance (transform.position, target.transform.position);
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);
		if ( distance < 0.2f) {
			target = targetList[++nextTargetIndex];
		}

	}
}
