using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Player;   //Reference the Player game object.

    private Vector3 Offset;     //Store the offset distance between player and camera.

	// Use this for initialization
	void Start ()
    {
        Offset = transform.position - Player.transform.position;    //Calculate the offset value by findign the distance between the player's position and the camera's.
	}
	
	
	void LateUpdate ()  // LateUpdate is called after Update each frame.
    {
        transform.position = Player.transform.position + Offset;
	}
}
