using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject Explosion;
	public int scorevalue = 100;
	public ScoreKeeper scorekeeper;


	void Start()
	{

		scorekeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();

	}
	public void OnTriggerEnter(Collider col)
	{
		ProjectilePlayer missle = col.gameObject.GetComponent<ProjectilePlayer>();

		if (missle)
		{
			missle.Hit();
			Debug.Log("statek/pocisk");
			Instantiate(Explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
			scorekeeper.ScorePoints(scorevalue);

		}

		PlayerController player = col.gameObject.GetComponent<PlayerController>();
		if (player)
		{
			Debug.Log("statek/player");
			Instantiate(Explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

	}
}
