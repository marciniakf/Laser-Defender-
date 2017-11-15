using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgDetection : MonoBehaviour {

	public float health = 150;
	public float EnemyLaserSpeed = -5f;
	public GameObject redLaser;
	public float shootsPerSeconds = 0.5f;
	public int enemyscore = 150;
	public AudioClip enemylasersound;
	public AudioClip enemydestroyed;

	private ScoreKeeper scoreKeeper;


	void Start()
	{
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	void Update(){

		float propability = shootsPerSeconds * Time.deltaTime;
		if (Random.value < propability){
			EnemyFire();

		}
	}

	void EnemyFire(){
		Vector3 startPosition = transform.position + new Vector3(0, -0.3f, 0);
		GameObject EnemyLaser = Instantiate(redLaser, startPosition, Quaternion.identity) as GameObject;
		EnemyLaser.GetComponent<Rigidbody>().velocity = new Vector3(0, EnemyLaserSpeed);
		AudioSource.PlayClipAtPoint(enemylasersound, startPosition);

	}

	private void OnTriggerEnter(Collider collision){

		//if (collision.gameObject.tag == "Laser") na tagach

		ProjectilePlayer missle = collision.gameObject.GetComponent<ProjectilePlayer>();
		if (missle){
			health -= missle.GetDmg();
			missle.Hit();
			if (health <= 0){
				Destroy(gameObject);
				scoreKeeper.ScorePoints(enemyscore);
				AudioSource.PlayClipAtPoint(enemydestroyed, transform.position);
					
			}
		}

			//Debug.Log("enemy hit");
			//Destroy(collision.gameObject);
		}

	}
	

