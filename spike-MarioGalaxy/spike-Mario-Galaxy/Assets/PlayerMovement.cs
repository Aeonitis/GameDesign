using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerMovement assuming spheres
public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 20.0f;
    public float gravity = 9.81f;
    public Transform gravitySource;
    public Vector3 velocity = Vector3.zero;
    /// <param name="gravityVector">Vector pointing inwards. Negative form means pointing outwards.</param>
    private Vector3 gravityVector;
    /// <param name="gravityVelocity">Player in the air will be accumulating gravity. We want to avoid a linear drop, too boring!</param>
    // private float gravityVelocity = 0.0f;
    private bool grounded = false;
    public float jumpPower = 10.0f;
    /// <param name="jumpOffset">Move player up before the jump.</param>
    public float jumpOffset = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    void Update() {
        
        transform.position += transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;

        //Rotate on Z-Axis, Applying a rotation of eulerAngles.z degrees around the z axis
        transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime);

        if(Input.GetButton("Jump") && grounded) {
            grounded = false;
            
            //Jump logic
            Vector3 jumpDirection = (transform.position - gravitySource.position).normalized;
            velocity += jumpDirection*jumpPower;
            transform.position += jumpDirection*jumpOffset;

            //Failed experiment :(
            // gravityVelocity = -jumpPower;
            // transform.position += -gravityVector.normalized;
        }
    }

    // FixedUpdate due to Physics/RigidBody in use, avoids the jittering you'd get with Update()
    // Note: Triggers (OnTriggerEnter, OnTriggerStay, etc...) only occur during FixedUpdate
    void FixedUpdate()
    {
        // Actual vector between the two positions, normalized to one unit long (to be reused in other code) e.g. multiplied by gravity
        // gravityVector = (gravitySource.position - transform.position).normalized * (gravity + gravityVelocity) * Time.deltaTime;
        
        // A 1.0 normalized Direction, multiply by gravity n so we get actual vectordown then multiply delta time to be time-based
        gravityVector = (gravitySource.position - transform.position).normalized*gravity*Time.deltaTime;

        velocity += gravityVector;
        // gravityVelocity += (gravity + gravityVelocity) * Time.deltaTime;

        // Rotate to given forward direction, and maintain the 'up' direction to handle rotation relative to gravitySource
        transform.rotation = Quaternion.LookRotation(-gravityVector, transform.up);
        // Move to gravity, applying velocity to player
        transform.position +=  velocity*Time.deltaTime;
        // transform.position +=  gravityVector*Time.deltaTime;

        // TODO: Constraints on rotation, set them programatically
        // GetComponent<Rigidbody>().AddForce(gravityVector);

    }

    void OnTriggerEnter(Collider collider) {

        StayOnSphere(collider);
    }
 

    void OnTriggerStay(Collider collider) {

        StayOnSphere(collider);
    }

    // To kick you out of the inner body if you sink inwards...
    // Pre-requisite: Collider 'Is Trigger' property is set to true
    void StayOnSphere(Collider collider) {

        grounded = true;
        velocity = Vector3.zero;
        // Reset accumulated gravity velocity, alternative: can assign to OnCollisionStay() too
        // gravityVelocity = 0.0f;

        // Set position to as far as the colliding shape's axis radius length (diameter*0.5 for radius) of the vector direction
        transform.position = gravitySource.position - gravityVector.normalized*collider.transform.localScale.y*0.5f;

        // Alternative way: Cast to gain access to sphere's radius param
        // SphereCollider sphereCollider = (SphereCollider) collider;
        // transform.position = gravitySource.position - gravityVector.normalized*sphereCollider.radius;
    }

    void OnCollisionStay(Collision collision) {
        // if(collision.collider == gravitySource.GetComponent<Collider>()) {
        //     transform.position = gravitySource.position - gravityVector.normalized*(gravitySource.localScale.x/2.0f);
        // }
    }
}
