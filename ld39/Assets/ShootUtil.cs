using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUtil {

	public static Vector2 firingVector(Transform startingPosition, GameObject target, float bulletSpeed) {
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
		try {
			

		Vector2 targetVelocityTmp = target.GetComponent<Rigidbody2D>().velocity;
		Vector3 targetVelocity = new Vector3 (targetVelocityTmp.x, targetVelocityTmp.y, 0);
		Vector3 targetPosition = new Vector3 (target.transform.position.x, target.transform.position.y, 0);
		float a = Vector3.Dot (targetVelocity, targetVelocity) - Mathf.Pow (bulletSpeed, 2);
		float b = 2f * Vector3.Dot (targetVelocity, targetPosition - startingPosition.position);
		float c = Mathf.Pow (Vector3.Distance (targetPosition, startingPosition.position),2);
		float d = -b / (2f * a);
		float e = Mathf.Sqrt (Mathf.Pow (b,2) - 4f * a * c) / (2f * a);
			Debug.Log("e is " + e);
		float t1 = d - e;
		float t2 = d + e;
		float t = (t1 > t2 && t2 > 0f) ? t2 : t1;
		Vector3 bulletVelocity = (targetPosition + new Vector3 (targetVelocity.x, targetVelocity.y) * t - startingPosition.position)/t;

		//			Debug.Log (targetVelocity);
		//			Debug.Log (targetPosition);
		//			Debug.Log (a);
		//			Debug.Log (b);
		//			Debug.Log (c);
		//			Debug.Log (d);
		//			Debug.Log (e);
		//			Debug.Log (t1);
		//			Debug.Log (t2);
		//			Debug.Log (t);
			if (float.IsNaN(bulletVelocity.x) || float.IsNaN(bulletVelocity.y))
			{
				Debug.Log("NAN DETECTED!!!");
				return new Vector2();
			}
		return new Vector2 (bulletVelocity.x, bulletVelocity.y);
		//	return  Vector3.Normalize (target.transform.position - startingPosition.position) * speed;
		}
		catch (MissingReferenceException e) {
			// just ignore
		}
		return new Vector2();
	}
}
