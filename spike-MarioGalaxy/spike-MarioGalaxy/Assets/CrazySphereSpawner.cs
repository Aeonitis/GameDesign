using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazySphereSpawner : MonoBehaviour {

	public float spawnDelay = 0.1f;
public GameObject sphere;

	// Use this for initialization
	void Start () {
		Invoke("Spawn", spawnDelay);
	}

	void Spawn() {
		Instantiate(sphere, transform.position, Quaternion.identity);
	}
	
}
