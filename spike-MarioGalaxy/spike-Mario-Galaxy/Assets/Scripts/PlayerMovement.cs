using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 #region bugs
    // - Planet-switching interrupted when holding jump button
 #endregion

/**
 * PlayerMovement assuming spheres as gravity sources
 */
public class PlayerMovement : MonoBehaviour
{
    #region vardefinitions
    /// <param speed="gravity">Player's movement speed scalar.</param>
    public float speed = 1.0f;
    /// <param name="rotationSpeed">Player's rotation speed scalar.</param>
    public float rotationSpeed = 20.0f;
    /// <param name="gravity">Gravity scalar set on player.</param>
    public float gravity = 9.81f;
    /// <param name="gravitySource">Object which causes gravitational pull.</param>
    public Transform gravitySource;
    /// <param name="velocity">Player velocity vector applied for passive gravity and active jumps.</param>
    public Vector3 velocity = Vector3.zero;
    /// <param name="gravityVector">Vector pointing inwards. Negative form means pointing outwards.</param>
    private Vector3 gravityVector;
    /// <param name="grounded">Player state boolean when on ground.</param>
    private bool grounded = false;
    public float jumpPower = 10.0f;
    /// <param name="jumpOffset">Move player up before the jump.</param>
    public float jumpOffset = 1.0f;
    /// <param name="gravityRotationRate">Rotation in degrees.</param>
    public float gravityRotationRate = 30.0f;

    public float cameraShakeMagnitude = 0.4f;
    public float cameraShakeDuration = 0.05f;
    private bool justJumped = false;
    private bool shakeCamera = false;
    // private Vector3 cameraInitialPosition;
    // private Vector3 cameraShakeOffset;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    
    }

    void LateUpdate() {
        // CameraShake logic
        if(shakeCamera) {
            Camera.main.transform.position += Random.insideUnitSphere*cameraShakeMagnitude;
        }
    }

    void Update() {
        justJumped = false;

        transform.position += transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // Rotate on Z-Axis, Applying a rotation of eulerAngles.z degrees around the z axis
        transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime);

        if(Input.GetButton("Jump") && grounded) {
            grounded = false;
            
            //Jump logic
            Vector3 jumpDirection = (transform.position - gravitySource.position).normalized;
            velocity += jumpDirection*jumpPower;
            // Offsets the character when you hit jump so it's not on the ground
            // transform.position += jumpDirection*jumpOffset;

        }

        /** 
            Amount of gravity to apply FOR THE deltaTIME BEING ;)
            A 1.0 normalized Vector/Direction, multiply by gravity scalar 'n' so we get the actual/relative Vector.down
        */
        gravityVector = (gravitySource.position - transform.position).normalized*gravity*Time.deltaTime;

        // Velocity is needed, incrementing gravity
        velocity += gravityVector;

        // Rotate to given quaternion, maintaining the 'up' direction to handle rotation relative to this transform's current rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-gravityVector, transform.up), gravityRotationRate*Time.deltaTime);
        // Move to gravity, applying velocity to player
        transform.position +=  velocity*Time.deltaTime;
    }

    void StopCameraShake() {
        shakeCamera = false;
    }

    // FixedUpdate (once per frame) due to Physics/RigidBody in use, avoids the jittering you'd get with Update()
    // Note: Triggers (OnTriggerEnter, OnTriggerStay, etc...) only occur during FixedUpdate
    void FixedUpdate() {

    }

    void OnTriggerEnter(Collider collider) {

        if(collider.tag == "Planetoid") {
            shakeCamera = true;
            Invoke("StopCameraShake", cameraShakeDuration);

            StayOnSphere(collider);
        }

        // Set gravitysource as the collider transform of 'GravitationalField' tagged object
        if(collider.tag == "GravitationalField") {
            gravitySource = collider.transform;    
        }
    }
 

    void OnTriggerStay(Collider collider) {

        if(collider.tag == "Planetoid") {
            StayOnSphere(collider);
        }
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
