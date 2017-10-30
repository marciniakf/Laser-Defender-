using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour {
	public GameObject Explosion;



	public void ExplosionAsteroid()
	{
		Instantiate(Explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
		Debug.Log("rozpierdol");
	}

}
