using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource prepareMusic;
	public AudioSource attackMusic;
	// Use this for initialization
	void Start () {
		prepareMusic.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayPrepareMusic()
	{
		prepareMusic.Stop ();
		attackMusic.Play ();

	}

	public void PlayAttackMusic()
	{

		attackMusic.Stop ();
		prepareMusic.Play ();

	}
}
