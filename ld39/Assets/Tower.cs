using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour {
	Queue<GameObject> targetList = new Queue<GameObject> ();
	GameObject target = null;
	public float bulletSpeed = 6f;
	private bool alreadyFired = false;

	CanvasObject.GetComponent<TowerMenu>();

	// Use this for initialization
	void Start () {
		// bullet.transform.position = transform.position;
	}
	IEnumerator waitForNextShoot() {
		yield return new WaitForSeconds(1f);
		alreadyFired = false;
		Debug.Log ("ready to fire");

	}
	// Update is called once per frame
	void Update () {
		if (targetList.Count > 0 && !alreadyFired) {
			target = targetList.Dequeue ();
			alreadyFired = true;
			StartCoroutine (waitForNextShoot ());
			GameObject bullet = (GameObject)Instantiate(Resources.Load("prefab/bullet"), GetComponent<Transform>().position, GetComponent<Transform>().rotation) ;

			Debug.Log ("firing");

			bullet.GetComponent<Rigidbody2D> ().velocity = ShootUtil.firingVector (transform, target, bulletSpeed);

		}
	}
	void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Grunt") {
            Debug.Log("in range");
		    targetList.Enqueue (coll.gameObject);
        }
	}

	void OnMouseEnter(){
			towerMenu.SetActive(true);
	}

}
