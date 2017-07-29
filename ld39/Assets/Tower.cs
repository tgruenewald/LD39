using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
	GameObject target = null;
	public GameObject bullet;
	public float bulletSpeed = 6f;
    private bool autoMode = true;
    private bool towerShooting = true;
    private bool towerSelected = false;

    bool forget = false;
	// Use this for initialization
	void Start () {
        bullet.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (towerShooting)
        {
           
            if (autoMode){
                FireAutoMode();
            }
            else {
                FireDirectMode();
            }
        }

		
	}
	void OnTriggerEnter2D(Collider2D coll){

        if (target == null && coll.gameObject.tag == "Grunt")
        {
            target = coll.gameObject;
            //DEBUG CODE
            //Grunt grunt = (Grunt)coll.gameObject.GetComponent<MonoBehaviour>();
            //grunt.speed = 0;
            //END DEBUG CODE
            Debug.Log("in range");
            //		if (coll.gameObject.tag == "Finish") {
            //			
            //		}
        }

    }

    void FireAutoMode()
    {
        //if there is a target
        if (target != null && !forget)
        {
            forget = true;
            // a = targetVelocity^2 - bulletSpeed^2
            // b = 2*Vector3.dot(targetVelocity, targetPosition - towerPosition)
            // altb = 2*(tvx*(tpx-cpx)+tvy*(tpy-cpy)
            // c = (Vector3.Distance(target.position, tower.position))^2
            // altc = sqr(tpx-cpx) + sqr(tpy-cpy)
            // d = -b/2*a
            // e = sqrt(b^2 - 4 * a * c)/ (2  *a )
            // t1 = d-e
            // t2 = d+e
            // t = (t1 > t2 && t2 > 0 ) ? t2 : t1
            // velocity =( target.position + target.velocity*t - tower.position) / t

            Vector2 targetVelocityTmp = target.GetComponent<Rigidbody2D>().velocity;
            Vector3 targetVelocity = new Vector3(targetVelocityTmp.x, targetVelocityTmp.y, 0);
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, 0);
            float a = Vector3.Dot(targetVelocity, targetVelocity) - Mathf.Pow(bulletSpeed, 2);
            float b = 2f * Vector3.Dot(targetVelocity, targetPosition - transform.position);
            float c = Mathf.Pow(Vector3.Distance(targetPosition, transform.position), 2);
            float d = -b / (2f * a);
            float e = Mathf.Sqrt(Mathf.Pow(b, 2) - 4f * a * c) / (2f * a);
            float t1 = d - e;
            float t2 = d + e;
            float t = (t1 > t2 && t2 > 0f) ? t2 : t1;
            Vector3 bulletVelocity = (targetPosition + new Vector3(targetVelocity.x, targetVelocity.y) * t - transform.position) / t;
            Debug.Log("firing");
            Debug.Log(targetVelocity);
            Debug.Log(targetPosition);
            Debug.Log(a);
            Debug.Log(b);
            Debug.Log(c);
            Debug.Log(d);
            Debug.Log(e);
            Debug.Log(t1);
            Debug.Log(t2);
            Debug.Log(t);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletVelocity.x, bulletVelocity.y);



        }// if there is a target
    }//FireAutoMode

    void FireDirectMode()
    {
        //math involved?
    }

}
