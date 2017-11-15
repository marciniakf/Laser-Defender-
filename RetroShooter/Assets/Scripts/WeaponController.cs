using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public GameObject shot;
	public Transform shotspawn;
	public float fireRate;
	public float delay;
	public float EnemyLaserSpeed;

	private AudioSource audiosource;

	void Start () {
		audiosource = GetComponent<AudioSource>();
		InvokeRepeating ("Fire", delay,fireRate);
	}

	void Fire (){
			
		GameObject EnemyLaser = Instantiate(shot, shotspawn.position, shotspawn.rotation);
		EnemyLaser.GetComponent<Rigidbody>().velocity = new Vector3(0, -EnemyLaserSpeed);
		audiosource.Play();

		
	}
}
