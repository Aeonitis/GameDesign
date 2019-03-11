using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerMovement assuming spheres
public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 20.0f;
    public float gravity = 9.98f;
    public Transform gravitySource;
    /// <param name="gravityVector">Vector pointing inwards. Negative form means pointing outwards.</param>
    private Vector3 gravityVector;                      

    // Start is called before the first frame update
    void Start()
    {
    
    }

    void Update() {
        
        transform.position += transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;

        //Rotate on Z-Axis, Applying a rotation of eulerAngles.z degrees around the z axis
        transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime);
    }

    // FixedUpdate due to Physics/RigidBody in use
    void FixedUpdate()
    {
        // Actual vector between the two positions, normalized to one unit long (to be reused in other code) e.g. multiplied by gravity
        gravityVector = (gravitySource.position - transform.position).normalized * gravity;

        // TODO: Alternative may be to make Z vector look up to handle rotation relative to gravitySource
        transform.rotation = Quaternion.LookRotation(-gravityVector);

        transform.position +=  gravityVector*Time.deltaTime;

        // TODO: Constraints on rotation, set them programatically
        // GetComponent<Rigidbody>().AddForce(gravityVector);

    }

    // To kick you out of the inner body if you sink inwards...
    // Pre-requisite: Sphere Collider 'Is Trigger' is set to true
    void OnTriggerEnter(Collider collider) {

        // Cast to gain access to sphere's radius param
        // SphereCollider sphereCollider = (SphereCollider) collider;
        // transform.position = gravitySource.position - gravityVector.normalized*sphereCollider.radius;
        
        // Set position to as far as the colliding shape's axis radius length (diameter*0.5 for radius) of the vector direction
        transform.position = gravitySource.position - gravityVector.normalized*collider.transform.localScale.y*0.5f;
    }

    void OnTriggerStay(Collider collider) {

        // Cast to gain access to sphere's radius param
        // SphereCollider sphereCollider = (SphereCollider) collider;

        // Set position to as far as the colliding shape's axis radius length (diameter*0.5 for radius) of the vector direction
        transform.position = gravitySource.position - gravityVector.normalized*collider.transform.localScale.y*0.5f;
        // transform.position = gravitySource.position - gravityVector.normalized*sphereCollider.radius;
    }

    void OnCollisionStay(Collision collision) {
        // if(collision.collider == gravitySource.GetComponent<Collider>()) {
        //     transform.position = gravitySource.position - gravityVector.normalized*(gravitySource.localScale.x/2.0f);
        // }
    }
}
