using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public float Speed = 10.0f;

	public float Lifetime = 1.5f;

	private void Start()
	{
		Destroy(this.gameObject, Lifetime); // set lifetime
	}

	private void FixedUpdate()
	{
		float delta = Time.fixedDeltaTime;

		transform.position += Speed * delta * transform.up;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("obstacle")) return;

		AsteroidsGameManager.instance.OnObstacleDestroyed(other.gameObject);
		Destroy(other.gameObject);
		Destroy(this.gameObject);
	}
}
