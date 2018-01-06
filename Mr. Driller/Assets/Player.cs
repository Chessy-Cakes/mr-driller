using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Rigidbody2D rb;
	float movement = 0f;

	public float speed = 10f;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        if (Input.GetKey("right")) {
            movement = speed;
        } else if (Input.GetKey("left")) {
            movement = -speed;
        } else {
            movement = 0;
        }
	}

	void FixedUpdate() {
		Vector2 velocity = rb.velocity;
		velocity.x = movement;
		rb.velocity = velocity;
	}
}
