using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipController : MonoBehaviour
{
	public readonly string ForwardAxis = "Vertical";
	public readonly string FireAction = "Fire1";

	public GameObject BulletPrefab;

	public Transform Gun;

	public float Speed = 5.0f;

	public float FireRate = 0.5f;

	public float LookAtSafeDistance = 1.0f;

	private float lastFiredTS = 0.0f;

	public Rigidbody2D rigidBody;

	private void Update()
	{
		float movement = Mathf.Round(Input.GetAxis(ForwardAxis));
		Move(movement * Time.deltaTime);

		LookAtCursor();

		if (Input.GetButton(FireAction))
			Fire();
	}

	void Move(float amount)
	{
		Vector2 forward = new Vector2(transform.up.x, transform.up.y);
		rigidBody.position += amount * Speed * forward;
	}

	void LookAtCursor()
	{
		Vector3 cursor = AsteroidsGameManager.instance.ScreenToWorld(Input.mousePosition);
		Vector3 direction = transform.position - cursor;
		direction.z = 0;
		if (direction.magnitude < LookAtSafeDistance) return;

		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90.0f;
		rigidBody.rotation = angle;
	}

	void Fire()
	{
		float currentTime = Time.time;
		if (currentTime < (lastFiredTS + FireRate)) return; // can't fire yet
		lastFiredTS = currentTime;

		Instantiate(BulletPrefab, Gun.position, Gun.rotation);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!collider.CompareTag("obstacle")) return;

		AsteroidsGameManager.instance.GameOver();
	}
}
