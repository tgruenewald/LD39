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

			return  Vector3.Normalize (target.transform.position - startingPosition.position) * bulletSpeed;
		}
		catch (MissingReferenceException e) {
			// just ignore
		}
		return new Vector2();
	}
}
