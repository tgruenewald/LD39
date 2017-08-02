using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
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

	private Vector3 mousePos;
	public GameObject windMarkerPrefab = null;
	public bool creatingMode = false;
	public bool redirectMode = false;
	GameObject[] windMarker;
	AudioSource[] audio = new AudioSource[2];
	void Start ()
	{
		power = startpower;
		audio[0]= gameObject.AddComponent<AudioSource> ();//gameObject.addComponent<AudioSource >();
		audio[1]= gameObject.AddComponent<AudioSource> ();


		// bullet.transform.position = transform.position;
	}

	void Awake ()
	{
		canvas = GameObject.Find ("Canvas").transform;

		CreateEnergyBar ();


	}

	IEnumerator waitForNextShoot ()
	{
		yield return new WaitForSeconds (1f);
		alreadyFired = false;

	}
	// Update is called once per frame
	void Update () {
		if (windMarkerPrefab != null && creatingMode) {
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint (mousePos);
			windMarkerPrefab.transform.position = new Vector3 (mousePos.x, mousePos.y, transform.position.z);// Vector2.Lerp(transform.position, mousePosition, moveSpeed);
		}

		if (mouseCounter == 0) {
			//Debug.Log ("Restoring outer collider");
			GetComponent<Collider2D> ().enabled = true;
		}

		if (power <= 0) {
			Debug.Log ("Reloading");
		} else {
			if (redirectMode) {
				if (!alreadyFired) {
					try {
						Debug.Log(" gameObject.GetInstanceID" + gameObject.GetInstanceID());

					alreadyFired = true;
					StartCoroutine (waitForNextShoot ());
					Debug.Log ("shooting wind");
					power--;
					Transform[] t = new Transform[1];
					t [0] = windMarker[0].transform;
					var grunt = (GameObject) Instantiate(Resources.Load("prefab/wind"), GetComponent<Transform>().position, GetComponent<Transform>().rotation) ;
						audio[0].PlayOneShot((AudioClip)Resources.Load("Music/wind"), 1f);			
					grunt.GetComponent<Wind> ().targetList = t; //gameObject.GetComponentInParent<WindSpawnPointParent>().targetList;
					grunt.GetComponent<Wind> ().speed = 6f;
					grunt.GetComponent<Wind> ().windId = gameObject.GetInstanceID();	;		
					}
					catch  {
						// if anything goes wrong go back to automatic
						redirectMode = false;
						windMarker = null;
					}
				}
			
			} else {
				// automatic fire mode
				if (targetList.Count > 0) {

					try {

						while (targetList.Peek () != null && !targetList.Peek ().GetComponent<Grunt> ().inRange) {
							targetList.Dequeue ();
						}
				 
						if (targetList.Count > 0 && !alreadyFired) {
							target = targetList.Dequeue ();
							alreadyFired = true;
							StartCoroutine (waitForNextShoot ());
							power-=2;
							GameObject bullet = (GameObject)Instantiate (Resources.Load ("prefab/bullet"), GetComponent<Transform> ().position, GetComponent<Transform> ().rotation);
							audio [0].pitch = 0.5f;
							audio[0].PlayOneShot((AudioClip)Resources.Load("Music/cannon"), 1f);

							//bullet.GetComponent<Rigidbody2D> ().velocity = transform.TransformDirection(target.transform.position); //ShootUtil.firingVector (transform, target, bulletSpeed);
							bullet.transform.position = Vector3.MoveTowards (bullet.transform.position, target.transform.position, bulletSpeed);
							Vector2 targetVelocity = target.GetComponent<Rigidbody2D> ().velocity;
							bullet.GetComponent<Bullet> ().origTargetVelocity = targetVelocity;
							bullet.GetComponent<Bullet> ().origTarget = target;
							bullet.GetComponent<Bullet> ().speed = bulletSpeed;



						}				
					} catch (MissingReferenceException e) {
						targetList.Dequeue ();
					}								
				}
			}


		}

		if (power >= maxpower) {
			powhold = power - maxpower;
			GameObject.Find ("Canvas").GetComponent<GameManager> ().fortressPower += powhold;
			power = maxpower;
			superchargeText.transform.position = new Vector3 (superTextPos.x, superTextPos.y, transform.position.z);// Vector2.Lerp(transform.position, mousePosition, moveSpeed);
		} else {
			superchargeText.transform.position = new Vector3 (3000, 3000, transform.position.z);// Vector2.Lerp(transform.position, mousePosition, moveSpeed);

		}

		newBar.value = power;
	}//Update

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Grunt") {
			//Debug.Log ("in range");
			targetList.Enqueue (coll.gameObject);
			coll.gameObject.GetComponent<Grunt> ().inRange = true;
		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Grunt") {
			//Debug.Log ("out of range");
			coll.gameObject.GetComponent<Grunt> ().inRange = false;
		}
	}

	void OnMouseEnter ()
	{
		if (tag != "selected_tower") {
			Debug.Log ("mouse entering collider 96");
			highlight ();
		}
	}

	void OnMouseExit ()
	{
		if (tag != "selected_tower") {
			unhighlight ();
		}

	}

	void OnMouseDown ()
	{
		audio [1].pitch = 3;
		audio[1].PlayOneShot((AudioClip)Resources.Load("Music/cannon"), 1f);
		Debug.Log ("mouse down on tower");
		if (tag == "selected_tower") {
			tag = "tower";
			unhighlight ();
		}
		else {
			tag = "selected_tower";
			Debug.Log ("becoming selected");
			// CreateWindMarker (Input.mousePosition);
			highlight ();			
		}


	}
	public void change_mode(int mode, GameObject[] windMarkers) {
		if (redirectMode) {
			// clean up
			Debug.Log ("changing to redirect destroy old wind marker");
			redirectMode = false;
			if (windMarker.Length > 0)
				DestroyObject (windMarker [0]);
		}		
		if (mode == GameManager.REDIRECT) {
			this.windMarker = windMarkers;
		}
		else {
			Debug.Log("resetting back to automatic");
			tag = "tower";
		}

		Debug.Log ("unhighlighting");
		// now deselect
		unhighlight ();

	}
	void highlight() {
		//temp.a = 0.5f;

		GetComponent<SpriteRenderer> ().material.SetColor("_Color", Color.yellow);//.color = new Color(255,255,96,0.5f);
		var gobj = transform.FindChild ("tower_range");
		if (gobj != null)
			gobj.GetComponent<SpriteRenderer> ().enabled = true;		
	}
	public void unhighlight() {
		GetComponent<SpriteRenderer> ().material.SetColor("_Color", Color.white);
		//temp.a = 0.5f;
		var gobj = transform.FindChild ("tower_range");
		if (gobj != null)
			gobj.GetComponent<SpriteRenderer> ().enabled = false;		
	}
	public GameObject[] CreateWindMarker (Vector2 mousePosition)
	{
		redirectMode = false;
		if (windMarker != null && windMarker.Length > 0)
			DestroyObject (windMarker [0]);
		creatingMode = true;
		RaycastHit hit = RayFromCamera (mousePosition, 1000.0f);
		Vector3 objectPos = Camera.main.ScreenToWorldPoint (mousePosition);
		windMarkerPrefab = (GameObject)GameObject.Instantiate (Resources.Load ("prefab/wind_redirect_marker"), transform.position, Quaternion.identity);
		windMarkerPrefab.GetComponent<WindMarkerPlacement> ().origWindMill = gameObject;
		windMarker = new GameObject[1];

		windMarker [0] = windMarkerPrefab; 
		return windMarker;

	}

	public RaycastHit RayFromCamera (Vector3 mousePosition, float rayLength)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (mousePosition);
		Physics.Raycast (ray, out hit, rayLength);
		return hit;
	}

	void CreateEnergyBar ()
	{
		barPosition = Camera.main.WorldToScreenPoint (transform.position);
		barPosition = new Vector3 (barPosition.x, barPosition.y + 30, transform.position.z);


		newBar = GameObject.Instantiate (towerEnergyBar, barPosition, Quaternion.identity);
		superchargeText = newBar.transform.Find ("Supercharge Text").gameObject; 
		superTextPos = new Vector3 (barPosition.x, barPosition.y + 20, transform.position.z);


		newBar.transform.SetParent (canvas);

		newBar.value = startpower;
		newBar.maxValue = maxpower;

	}

	void RedirectWind ()
	{
		var grunt = (GameObject)Instantiate (Resources.Load ("prefab/wind"), GetComponent<Transform> ().position, GetComponent<Transform> ().rotation);
		grunt.GetComponent<Wind> ().targetList = gameObject.GetComponentInParent<WindSpawnPointParent> ().targetList;
		grunt.GetComponent<Wind> ().speed = gameObject.GetComponentInParent<WindSpawnPointParent> ().windSpeed;
	}


}
