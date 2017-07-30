using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour {
	Queue<GameObject> targetList = new Queue<GameObject> ();
	GameObject target = null;
	public float bulletSpeed = 6f;
	private bool alreadyFired = false;

	public int power;
	public int startpower;
	public int maxpower;
	int powhold;

	// Use this for initialization

	public Transform canvas;
	public Slider towerEnergyBar;
	Slider newBar;
	Vector3 barPosition;
	Vector3 superTextPos;
	public GameObject superchargeText;
	public int mouseCounter = 0;


	void Start () {
		power = startpower;

		// bullet.transform.position = transform.position;
	}

	void Awake()
	{
		canvas = GameObject.Find("Canvas").transform;

		CreateEnergyBar ();


	}

	IEnumerator waitForNextShoot() {
		yield return new WaitForSeconds(1f);
		alreadyFired = false;

	}
	// Update is called once per frame
	void Update () {
		if (mouseCounter == 0) {
			Debug.Log ("Restoring outer collider");
			GetComponent<Collider2D> ().enabled = true;
		}
		if (power <= 0) {
			Debug.Log ("Reloading");
		}
		else{
			
				if (targetList.Count > 0) {

			try {

					while (!targetList.Peek ().GetComponent<Grunt>().inRange) {
						targetList.Dequeue ();
					}
				 
					if (targetList.Count > 0 && !alreadyFired) {
						target = targetList.Dequeue ();
						alreadyFired = true;
						StartCoroutine (waitForNextShoot ());
							power--;
						GameObject bullet = (GameObject)Instantiate(Resources.Load("prefab/bullet"), GetComponent<Transform>().position, GetComponent<Transform>().rotation) ;


						bullet.GetComponent<Rigidbody2D> ().velocity = ShootUtil.firingVector (transform, target, bulletSpeed);
							Vector2 targetVelocity = target.GetComponent<Rigidbody2D>().velocity;
							bullet.GetComponent<Bullet>().origTargetVelocity = targetVelocity;
							bullet.GetComponent<Bullet>().origTarget = target;
							bullet.GetComponent<Bullet>().speed = bulletSpeed;



						}				
					}				
				catch (MissingReferenceException e) {
					targetList.Dequeue ();
				}								
			}


		}

		if(power >= maxpower) {
			powhold = power - maxpower;
			GameObject.Find ("Canvas").GetComponent<GameManager> ().fortressPower += powhold;
			power = maxpower;
			superchargeText.transform.position = new Vector3 (superTextPos.x, superTextPos.y, transform.position.z);// Vector2.Lerp(transform.position, mousePosition, moveSpeed);
		}
		else{
			superchargeText.transform.position = new Vector3 (3000,3000, transform.position.z);// Vector2.Lerp(transform.position, mousePosition, moveSpeed);

		}

	}
	void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Grunt") {
            Debug.Log("in range");
		    targetList.Enqueue (coll.gameObject);
			coll.gameObject.GetComponent<Grunt> ().inRange = true;
        }
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Grunt") {
			Debug.Log("out of range");
			coll.gameObject.GetComponent<Grunt> ().inRange = false;
		}
	}

	void OnMouseEnter(){

		Debug.Log ("mouse entering collider");
		mouseCounter++;
		GetComponent<Collider2D> ().enabled = false;
	}
	void OnMouseExit(){
		mouseCounter--;
	}

	void CreateEnergyBar(){
		barPosition = Camera.main.WorldToScreenPoint (transform.position);
		barPosition = new Vector3 (barPosition.x, barPosition.y + 30, transform.position.z);


		newBar = GameObject.Instantiate(towerEnergyBar, barPosition, Quaternion.identity);
		superchargeText = newBar.transform.Find ("Supercharge Text").gameObject; 
		superTextPos = new Vector3 (barPosition.x, barPosition.y + 20, transform.position.z);


		newBar.transform.SetParent (canvas);

		newBar.value = startpower;
		newBar.maxValue = startpower * 2;

	}


}
