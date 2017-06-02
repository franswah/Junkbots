﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Bomb : MonoBehaviour {

	public LayerMask collisionMask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((collisionMask & (1 << other.gameObject.layer)) > 0)
			Destroy(gameObject);
	}
}
