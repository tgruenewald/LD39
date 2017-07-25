using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class GameState
{
    private static GameObject droplet = null;
    public static GameObject introMusic = null;



	public static void SetPlayerDroplet(GameObject droplet){
		GameState.droplet = droplet;
	}

	public static Player GetPlayerDroplet(){
		if(droplet == null){
			droplet = GameObject.FindGameObjectWithTag("Player");
			if(droplet == null) {
                Debug.Log("Getting player droplet but it is null");
                return null;
            }
				
		}

		return droplet.GetComponent<Player>();
	}
}