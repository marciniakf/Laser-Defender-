using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public GameObject Enemy_1Prefab;

	public float width = 10f;
	public float hight = 5f;
	public float speed = 5;
	public float spawndelay = 0.5f;


	private float xmin;
	private float xmax;
	private float ymin;
	private float ymax;

	private bool movingRight = false;



	// Use this for initialization
	void Start()
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		Vector3 upmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
		Vector3 downmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		xmin = leftmost.x;
		xmax = rightmost.x;
		ymax = upmost.y;
		ymin = downmost.y;
		//restrict enemy in gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
		transform.position = new Vector3(newX, newY, transform.position.z);
		SpawnUntilFull();
	}
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width, hight, 0));
	}
	void SpawnEnemies()
	{
		foreach (Transform child in transform)
		{
			GameObject enemies = Instantiate(Enemy_1Prefab, child.transform.position, Quaternion.identity) as GameObject;
			enemies.transform.parent = child;
		}
	}
	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();
		if (freePosition)
		{
			GameObject enemies = Instantiate(Enemy_1Prefab, freePosition.transform.position, Quaternion.identity) as GameObject;
			enemies.transform.parent = freePosition;
		}
		if (NextFreePosition()){
			Invoke("SpawnUntilFull", spawndelay);
		}
	}




	// Update is called once per frame
	void Update()
	{
		if (movingRight)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);

		if (leftEdgeOfFormation <= xmin)
		{
			movingRight = true;
		}
		else if (rightEdgeOfFormation >= xmax)
		{
			movingRight = false;
		}
		if (AllMembersDead())
		{
			Debug.Log("Empty Formaton");
			SpawnUntilFull();
		}
	}
	Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount == 0)
			{
				return childPositionGameObject;
			}
		}
		return null;
	}


		bool AllMembersDead(){
			foreach (Transform childPositionGameObject in transform)
			{
				if (childPositionGameObject.childCount > 0)
				{
					return false;
				}
			}
			return true;
		}


		

}








