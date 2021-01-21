using UnityEngine;

public class Game : MonoBehaviour
{
    void Start()
    {
        print("Start");
    }

    private void Awake()
    {
        print("Awake");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            print("You are pressing a key! Are you ready to make a game?");
        }
    }
}
