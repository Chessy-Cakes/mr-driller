using UnityEngine;
using System.Collections;

//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
public abstract class MovingObject : MonoBehaviour
{
	public LayerMask blockingLayer;
	protected Rigidbody2D rb;

	//Protected, virtual functions can be overridden by inheriting classes.
	protected virtual void Start ()
	{
		//Get a component reference to this object's Rigidbody2D
		rb = GetComponent <Rigidbody2D> ();
	}
		

	//The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
	//AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
	protected virtual void AttemptMove <T> (int xDir, int yDir) where T : Component
	{
		bool isMoving = false;
		Vector2 velocity = rb.velocity;
		velocity.x = xDir;
		rb.velocity = velocity;

		if (xDir != 0 && rb.velocity.y == 0) { // on horizontal movement
			OnCantMove (xDir, yDir);
		}
	
		if (rb.velocity.y == 0 && yDir != 0) { // on vertical movement
			OnCantMove (xDir, yDir);
		}
	}


	//The abstract modifier indicates that the thing being modified has a missing or incomplete implementation.
	//OnCantMove will be overriden by functions in the inheriting classes.
	protected abstract void OnCantMove(int xDir, int yDir);
}