using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour {
	public Transform[] targetList;// =new Transform[];
	public Transform target;
	public float speed = 0.1f;
	public int nextTargetIndex = 0;
	// Use this for initialization
	void Start () {
		target = targetList[nextTargetIndex];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
//		float step = speed * Time.deltaTime;
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.velocity = Vector3.Normalize (target.transform.position - transform.position) * speed;
		//transform.position = Vector3.MoveTowards (transform.position, target.position, step);
		float distance = Vector3.Distance (transform.position, target.transform.position);
		if ( distance < 0.2f) {
			target = targetList[++nextTargetIndex];
		}

	}
}
