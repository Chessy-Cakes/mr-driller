using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{

	// Used to store location of screen touch origin for mobile controls.
	private Vector2 touchOrigin = -Vector2.one;
	Rigidbody2D rb;
	private int horizontal = 0;
	private int vertical = 0;

	public int speed = 1;

	protected override void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		base.Start ();
	}

	protected void Update ()
	{

		horizontal = 0;     //Used to store the horizontal move direction.
		vertical = 0;       //Used to store the vertical move direction.

		// Check if we are running either in the Unity editor or in a standalone build.
		// #if UNITY_STANDALONE || UNITY_WEBPLAYER

		// Standard keyboard input 
		if (Input.GetKey ("right")) {
			horizontal = speed;
		} else if (Input.GetKey ("left")) {
			horizontal = -speed;
		} else {
			horizontal = 0;
		}

		if (Input.GetKey ("up")) {
			vertical = 0; //speed
		} else if (Input.GetKey ("down")) {
			vertical = -speed;
		} else {
			vertical = 0;
		}

		// Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
		#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

		// Check if Input has registered more than zero touches
		if (Input.touchCount > 0) {
			// Store the first touch detected.
			Touch myTouch = Input.touches [0];

			// Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began) {
				// If so, set touchOrigin to the position of that touch
				touchOrigin = myTouch.position;
			}
			// If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
			else if ((myTouch.phase == TouchPhase.Stationary || myTouch.phase == TouchPhase.Moved) && touchOrigin.x >= 0) {
				// Set touchEnd to equal the position of this touch
				Vector2 touchEnd = myTouch.position;

				// Calculate the difference between the beginning and end of the touch on the x axis.
				float x = touchEnd.x - touchOrigin.x;

				// Calculate the difference between the beginning and end of the touch on the y axis.
				float y = touchEnd.y - touchOrigin.y;

				// Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
				// touchOrigin.x = -1;

				// Check if the difference along the x axis is greater than the difference along the y axis.
				if (Mathf.Abs (x) > Mathf.Abs (y)) {
					// TODO: Add functionaity to add AttemptMove to include drill functionality
					// If x is greater than zero, set horizontal to speed, otherwise set it to -speed
					horizontal = x > 0 ? speed : -speed;
				} else {
					// TODO: Add functionality to vertical to include sliding up 1 block vs. drilling
					// If y is greater than zero, set horizontal to 1, otherwise set it to -1
					vertical = y > 0 ? 0 : -speed;
				}
			}
		}
		#endif
			
		// Prevent movement if character is falling
		// TODO: Adjust this portion to work if player is climbing up one set of rocks (eg. up-right)
		if (rb.velocity.y != 0) {
			horizontal = 0;
		}

		AttemptMove<Component> (horizontal, vertical);
	}

	protected void DestroyBlock(Vector2 vect) {
		// If player is not falling (velocity.y == 0)
		// Destroy the block immediately under the player

        // TODO: Add logic for "X" blocks which need to be drilled multiple times
		Debug.DrawRay (rb.position, vect, Color.magenta);
		RaycastHit2D hit = Physics2D.Raycast (rb.position + vect, vect, 0f);
		if (hit.collider != null) {
			print (hit.collider.gameObject.name);
			if (hit.collider.gameObject.name == "GreenBlock(Clone)") {
				Object.Destroy (hit.collider.gameObject);
			}
		}
	}

	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		base.AttemptMove <T> (xDir, yDir);
	}

	protected override void OnCantMove (int xDir, int yDir)
	{
		if (xDir < 0) {
			DestroyBlock (Vector2.left);
		} else if (xDir > 0) {
			DestroyBlock (Vector2.right);
		}

		if (yDir < 0) {
			DestroyBlock (Vector2.down);
		} else if (yDir > 0) {
			DestroyBlock (Vector2.up);
		}

	}
		
}
