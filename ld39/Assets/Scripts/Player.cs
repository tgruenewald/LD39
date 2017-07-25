using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour {

	public float maxSpeed = 3.0f;
	public float jumpForce = 20f;
	public bool grounded = true;
    public LayerMask whatIsGround;
	public AudioClip walkSound;
	private CircleCollider2D groundCheck;
	private Animator animator;
	private bool facingRight = true;
	private float move;
	private bool jump;

	// Use this for initialization

    public void SpawnAt(GameObject myPlayer)
    {
		Camera.main.GetComponent<Camera2DFollow>().target = myPlayer.transform;
		myPlayer.GetComponent<BoxCollider2D> ().enabled = true;

    }
	void Awake(){
		DontDestroyOnLoad (this.gameObject);

	}	
	void Start () {
		groundCheck = GetComponent<CircleCollider2D>();
//		animator = GetComponent<Animator>();
	}

	
	// Update is called once per frame
	void Update () {
		grounded = groundCheck.IsTouchingLayers (whatIsGround);

		move = Input.GetAxis ("Horizontal");
		if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
//		if (move != 0) {
//			animator.SetBool("isMoving", true);
//		}
//		else {
//			animator.SetBool("isMoving", false);
//		}

		jump = Input.GetButtonDown ("Jump") || Input.GetButtonDown ("Vertical");
		
		//Debug.Log ("move = " + move);
	}
	void FixedUpdate () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);	

		if (grounded && jump) {
			PlayWalk(walkSound);
			GetComponent<Rigidbody2D> ().AddForce(new Vector2(0f, jumpForce));
		}
	}
    void Flip()
    {
        //Debug.Log("switching...");
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Finish" ) {
			SpawnPoint.SwitchToLevel (this.gameObject);
			Debug.Log("Loading scene2");
			SceneManager.LoadScene("scene1");
		}
		if (coll.gameObject.tag == "exit" ) {
			SpawnPoint.SwitchToLevel (this.gameObject);
			Debug.Log("Loading scene2");
			SceneManager.LoadScene("scene2");
		}		
	}

	// void OnTriggerExit2D(Collider2D coll){
	// 	if (coll.gameObject.layer == LayerMask.NameToLayer ("Water"))
	// 		isTouchingWater = false;
	// }

	public void PlayWalk(AudioClip sound){
		AudioSource[] audios = gameObject.GetComponents<AudioSource>();
		if(audios.Length == 0)
			return;

		AudioSource slot = audios[1];
		if(slot.isPlaying)
			slot.Stop();
		
		audios[1].PlayOneShot(sound, 1f);
	}
	public void PlayAudio(AudioClip sound){
		AudioSource[] audios = gameObject.GetComponents<AudioSource>();
		if(audios.Length == 0)
			return;

		AudioSource slot = audios[0];
		if(slot.isPlaying)
			slot.Stop();
		
		audios[0].clip = sound;
		audios[0].Play();
	}

	public void StopAudio(AudioClip sound){
		AudioSource[] audios = gameObject.GetComponents<AudioSource>();
		if(audios.Length == 0)
			return;

		foreach(var slot in audios){
			if(slot.clip == sound)
				slot.Stop();
		}
	}

	public void StopAllAudio(){
		AudioSource[] audios = gameObject.GetComponents<AudioSource>();
		if(audios.Length == 0)
			return;

		foreach(var slot in audios){
			slot.Stop();
		}
	}	
}
