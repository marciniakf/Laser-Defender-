using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGscroller : MonoBehaviour {

	public float scrollspeed;
	public float tilesizeY;
	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = new Vector3(0, 0, 5);
		
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat(Time.time * scrollspeed,tilesizeY);
		transform.position = startPosition + Vector3.down * newPosition;
	}
}
