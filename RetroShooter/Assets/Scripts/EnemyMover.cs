﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

	public float speed;
	// Use this for initialization
	void Start()
	{
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, -speed);
		}
	}

}
