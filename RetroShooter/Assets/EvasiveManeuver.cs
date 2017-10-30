﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}
public class EvasiveManeuver : MonoBehaviour {

	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;




	public float dodge;
	public float smoothing;
	public float tilt;
	public Boundary boundary;

	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody rb;


	void Start () {

		rb = GetComponent<Rigidbody>();
		StartCoroutine(Evade());
		currentSpeed = rb.velocity.y;
	}

	IEnumerator Evade(){
		
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

		while (true){
			targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(maneuverTime.x,maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range(maneuverWait.x,maneuverWait.y));
			
		}
	}



	void FixedUpdate()
	{

		float newManeuver = Mathf.MoveTowards(rb.velocity.x,targetManeuver,Time.deltaTime*smoothing);
		rb.velocity = new Vector3(newManeuver, currentSpeed, 0);
		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
			0.0f);
		rb.rotation = Quaternion.Euler(90f, 180f, rb.velocity.x * -tilt);
		}
}
