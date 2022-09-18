using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipController : MonoBehaviour
{
	public readonly string ForwardAxis = "Vertical";
	public readonly string FireAction = "Fire1";

	public Object BulletPrefab;

	public Transform Gun;

	public float Speed = 5.0f;

	public float FireRate = 0.5f;

	public float LookAtSafeDistance = 1.0f;

	private float lastFiredTS = 0.0f;

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
		transform.position += amount * Speed * transform.up;
	}

	void LookAtCursor()
	{
		Vector3 cursor = AsteroidsGameManager.instance.ScreenToWorld(Input.mousePosition);
		Vector3 direction = transform.position - cursor;
		direction.z = 0;
		if (direction.magnitude < LookAtSafeDistance) return;

		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90.0f;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	void Fire()
	{
		float currentTime = Time.time;
		if (currentTime < (lastFiredTS + FireRate)) return; // can't fire yet
		lastFiredTS = currentTime;

		Object bullet = Instantiate(BulletPrefab, Gun.position, Gun.rotation);
		// do something to bullet
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!collider.CompareTag("obstacle")) return;

		Debug.LogFormat("Collided with: {0} at {1}", collider.gameObject, collider.transform.position);
		AsteroidsGameManager.instance.GameOver();
	}
}
