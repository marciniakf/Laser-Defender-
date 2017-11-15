using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public GameObject Laser;
	public float speed = 15.0f;
	public float tilt = 10f;
	public float paddingLR = 1f;
	public float paddingUD = 1f;
	public float LaserSpeed;
	public float FiringRate = 0.3f;
	public float health = 400f;
	public AudioClip firesound;
	public AudioClip playerdestroyed;
	public GameObject ExplosionPlayer;

	private readonly GameController gamecontroller;

	float xmin;
	float xmax;
	float ymin;
	float ymax;

	// Use this for initialization
	void Start()
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		Vector3 upmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
		Vector3 downmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		xmin = leftmost.x + paddingLR;
		xmax = rightmost.x - paddingLR;
		ymax = upmost.y - paddingUD;
		ymin = downmost.y + paddingUD;
	}

	void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0, 0.6f, 0);
		GameObject beam = Instantiate(Laser, startPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody>().velocity = new Vector3(0, LaserSpeed);
		AudioSource.PlayClipAtPoint(firesound, startPosition);

	}


	// Update is called once per frame
	void FixedUpdate()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", 0.000001f, FiringRate);
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			//transform.position += new Vector3(-speed * Time.deltaTime,0,0);	
			transform.position += Vector3.left * speed * Time.deltaTime;
			transform.rotation = Quaternion.Euler(-90, transform.rotation.x * speed * -tilt, 0);
		}
		else if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			transform.rotation = Quaternion.Euler(-90, 0, 0);
			//Debug.Log("Left UP");
		}

		else if (Input.GetKey(KeyCode.RightArrow))
		{
			//transform.position += new Vector3(+speed * Time.deltaTime,0,0);
			transform.position += Vector3.right * speed * Time.deltaTime;
			transform.rotation = Quaternion.Euler(-90, transform.rotation.x * speed * tilt, 0);
		}

		else if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			transform.rotation = Quaternion.Euler(-90, 0, 0);
		}

		else if (Input.GetKey(KeyCode.UpArrow))
		{
			//transform.position += new Vector3(0,+speed * Time.deltaTime,0);
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			//transform.position += new Vector3(0,-speed * Time.deltaTime,0);
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		//restrict player in gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
		transform.position = new Vector3(newX, newY, transform.position.z);
	}
	void OnTriggerEnter(Collider collision)
	{
		ProjectileEnemy missle = collision.gameObject.GetComponent<ProjectileEnemy>();
		if (missle)
		{
			Debug.Log("trafiony pociskiem");
			health -= missle.GetDmg();
			missle.Hit();
			if (health <= 0)
			{
				Destroy(gameObject);
				Instantiate(ExplosionPlayer, transform.position, Quaternion.identity);
				AudioSource.PlayClipAtPoint(playerdestroyed, transform.position);
				GameController obj = GameObject.Find("Game Controller").GetComponent<GameController>();
				obj.GameOver();
			}
		}

		//LevelManager obj = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		//obj.LoadLevel("Win Screen");

		AsteroidCollision Asteroid = collision.gameObject.GetComponent<AsteroidCollision>();
		if (Asteroid)
		{
			Debug.Log("Asteroida/statek");

			Asteroid.ExplosionAsteroid();
			Destroy(gameObject);
			GameController obj = GameObject.Find("Game Controller").GetComponent<GameController>();
			obj.GameOver();
			Debug.Log("Game Over");
			
		}

		if (collision.gameObject.tag == ("Enemy"))
		{
			Debug.Log("przeciwnik/statek");
			Instantiate(ExplosionPlayer, transform.position, Quaternion.identity);
			Destroy(gameObject);
			GameController obj = GameObject.Find("Game Controller").GetComponent<GameController>();
			obj.GameOver();
			Debug.Log("Game Over");

		}

	}
}
			


	

