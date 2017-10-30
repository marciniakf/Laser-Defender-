using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shreder : MonoBehaviour
{
	private ScoreKeeper scorekeeper;
	void Start()
	{

		scorekeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();

	}
	void OnTriggerEnter(Collider col)
	{
		Destroy(col.gameObject);
		DestroyByContact asteroid = col.gameObject.GetComponent<DestroyByContact>();
		if (asteroid)
		{
			scorekeeper.ScorePoints(-50);
		}
		EnemyMover enemy = col.GetComponent<EnemyMover>();
		if (enemy)
		{
			scorekeeper.ScorePoints(-100);
		}
	}



}

