﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour {

	public float damage = 100f;

	public float GetDmg(){
		return damage;
	}

	public void Hit(){
		Destroy(gameObject);

	}
}
