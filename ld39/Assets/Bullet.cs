using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public GameObject origTarget;
	public Vector2 origTargetVelocity;
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		try {
			if (origTarget != null)	{
				if (!origTarget.GetComponent<Rigidbody2D>().velocity.Equals(origTargetVelocity)) {
					GetComponent<Rigidbody2D>().velocity = ShootUtil.firingVector(transform, origTarget, speed);
					origTargetVelocity = origTarget.GetComponent<Rigidbody2D>().velocity;
				}
			}		
		}
		catch(MissingReferenceException e) {}


	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Grunt") {
			GameObject.FindWithTag ("Grunt").GetComponent<Grunt> ().health--;
			Destroy (this.gameObject);	
		}
	}
}
