using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	public float Speed = 1.5f;
	public float TurnSpeed = 30.0f;

	private Vector2 direction = new Vector2();

	public Rigidbody2D rigidBody;

	// may be use a simpler solution
	public void SetDirectionAngle(float angle)
	{
		Vector3 temp = Quaternion.Euler(0, 0, angle) * Vector3.up;
		direction = temp;
	}

	private void FixedUpdate()
	{
		float delta = Time.fixedDeltaTime;

		rigidBody.rotation += TurnSpeed * delta;
		rigidBody.position += Speed * delta * direction;
	}
}
