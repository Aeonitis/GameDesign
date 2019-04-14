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
		GameObject instance = Instantiate(sphere, transform.position, Quaternion.identity);
		Rigidbody instanceBody = instance.GetComponent<Rigidbody>();
		instanceBody.AddForce(Random.onUnitSphere * 1000.0f);
		instanceBody.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
		Invoke("Spawn", spawnDelay);
	}
	
}
