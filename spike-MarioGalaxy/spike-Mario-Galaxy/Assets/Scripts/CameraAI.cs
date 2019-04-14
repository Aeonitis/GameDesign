using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAI : MonoBehaviour
{

    public string tag = "Player";
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        // Getting player position
        Transform player = GameObject.FindGameObjectWithTag(tag).transform;
        // Getting camera offset, representing Vector between player and camera, a distance
        cameraOffset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag(tag).transform;
        
        transform.position = player.position + cameraOffset;
    }
}
