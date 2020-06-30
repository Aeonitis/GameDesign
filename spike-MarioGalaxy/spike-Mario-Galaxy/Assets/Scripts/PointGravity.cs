using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGravity : MonoBehaviour {
	public float force = 9.98f;
	public float radius = 100.0f;
	
	void FixedUpdate () {
		// Grab everything within radius, get colliders from them and apply our force to them
		foreach (Collider collider in Physics.OverlapSphere(transform.position, radius))
		{
			Rigidbody rigidBodyOfColliderAround = GetComponent<Collider>().GetComponent<Rigidbody>();
			if(rigidBodyOfColliderAround) {
				rigidBodyOfColliderAround.AddExplosionForce(-force, transform.position, radius);
			}
		}
		
	}
}
