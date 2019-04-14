using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAI : MonoBehaviour
{

    public string tag = "Player";

    public float cameraSpeed = 1.0f;
    public float cameraRotationSpeed = 30.0f;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        // Getting player position
        Transform player = GameObject.FindGameObjectWithTag(tag).transform;
        // Getting camera offset, representing Vector between player and camera, a distance. Same magnitude, opposite direction
        cameraOffset = player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag(tag).transform;
        Vector3 goalPosition;
        Quaternion goalRotation;
        
        // transform.position = player.position + cameraOffset;
        // Current player 'up' direction is arranged the opposite 'wrong' way, due to Z-axis being up instead of Y-axis >_<
        // transform.rotation = Quaternion.LookRotation(player.up, player.forward);

        // Set to player position (won't be a visible change as it happens in frame before rendering)
        transform.position = player.position;

        // Change orientation of camera to face player
        transform.rotation = Quaternion.LookRotation(player.up, player.forward);

        // Move camera back to original position relative to player (cameraOffset) & rotation
        transform.position -= transform.rotation*cameraOffset;

    }
}
