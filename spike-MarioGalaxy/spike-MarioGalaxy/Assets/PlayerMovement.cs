using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float gravity = 9.98f;
    public Transform gravitySource;
    private Vector3 gravityVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // FixedUpdate due to RigidBody in use
    void Update()
    {
        // Actual vector between the two positions, normalized to one unit long (to be reused in other code) e.g. multiplied by gravity
        gravityVector = (gravitySource.position - transform.position).normalized * gravity;

        // transform.position +=  gravityVector*Time.deltaTime;

        // TODO: Alternative may be to make Z vector look up to handle rotation relative to gravitySource
        transform.rotation = Quaternion.LookRotation(-gravityVector);
        
        // TODO: Constraints on rotation, set them programatically
        GetComponent<Rigidbody>().AddForce(gravityVector * Time.deltaTime);

    }

    void OnCollisionStay(Collision collision) {
        if(collision.collider == gravitySource.GetComponent<Collider>()) {
            transform.position = gravitySource.position - gravityVector.normalized*(gravitySource.localScale.x/2.0f);
        }
    }
}
