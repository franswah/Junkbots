using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Player2D : MonoBehaviour {

	public float speed = 2;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		float hori = Input.GetAxis ("Horizontal");

		rb.velocity = new Vector3 (hori, 0, 0) * speed;
	}
}
