﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class responsible for generating terrain/levels.
 * 
 */
public class TerrainGenerator : MonoBehaviour {

	public float levelWidth; //How long the level should be

	public GameObject[] frames;


	/*
	 * Call the generate function at start
	 */
	void Start(){
		GenerateLevel ();
	}


	/*
	 * Call once to generate an entire level
	 */
	public void GenerateLevel(){
		float curWidth = 0;

		while (curWidth < levelWidth) {		//	Spawn a new frame until we reach level width
			GameObject spawned = (GameObject)Instantiate (GetRandomFrame (), new Vector3 (transform.position.x + curWidth, transform.position.y, transform.position.z), Quaternion.identity);
			if (spawned.GetComponent<PlatformFrame> () != null) {
				spawned.GetComponent<PlatformFrame> ().spawnPattern = Random.Range(0,2);
			}
			spawned.GetComponent<IFrame> ().FillFrame ();
			curWidth += spawned.GetComponent<TerrainFrame> ().width;
		}
	}


	/*
	 * Get a random frame prefab
	 */
	public GameObject GetRandomFrame(){
		return frames [Random.Range (0, frames.Length)];
	}
}
