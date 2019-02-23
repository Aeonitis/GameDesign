using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
	public float rotateSpeed;
	public Vector3 rotationDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = transform.rotation * Quaternion.AngleAxis(180 * rotateSpeed * Time.deltaTime, rotationDirection);
	}
}
