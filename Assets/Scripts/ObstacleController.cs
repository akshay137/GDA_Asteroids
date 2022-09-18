using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	public float Speed = 1.5f;
	public float TurnSpeed = 30.0f;

	private Vector3 direction = new Vector3();

	// may be use a simpler solution
	public void SetDirectionAngle(float angle)
	{
		direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
	}

	private void FixedUpdate()
	{
		float delta = Time.fixedDeltaTime;
		float angle = transform.rotation.eulerAngles.z;
		transform.rotation = Quaternion.Euler(0, 0, angle + TurnSpeed * delta);

		transform.position += direction * Speed * delta;
	}
}
