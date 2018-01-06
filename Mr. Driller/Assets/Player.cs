using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	// Used to store location of screen touch origin for mobile controls.
	private Vector2 touchOrigin = -Vector2.one;

	Rigidbody2D rb;
	float movement = 0f;
	public float speed = 10f;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{

		// Check if we are running either in the Unity editor or in a standalone build.
		// #if UNITY_STANDALONE || UNITY_WEBPLAYER

		// Standard keyboard input 
		if (Input.GetKey ("right")) {
			movement = speed;
		} else if (Input.GetKey ("left")) {
			movement = -speed;
		} else {
			movement = 0;
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
					movement = x > 0 ? speed : -speed;
				} else {
					// TODO: Add functionality to vertical to include sliding up 1 block vs. drilling
					// If y is greater than zero, set horizontal to 1, otherwise set it to -1
					// vertical = y > 0 ? 1 : -1;
				}
			} else {
				movement = 0;
			}

			#endif //End of mobile platform dependendent compilation section started above with #elif
		}
	}

	void FixedUpdate ()
	{
		Vector2 velocity = rb.velocity;
		velocity.x = movement;
		rb.velocity = velocity;
	}
}
