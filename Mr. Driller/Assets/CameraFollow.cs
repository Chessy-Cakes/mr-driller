using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothSpeed = 0.5f;

	private Vector3 currentVelocity;

	void LateUpdate () {
		if (target.position.y > transform.position.y) {
			Vector3 newPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
			transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref currentVelocity, smoothSpeed);
		}
	}
}
