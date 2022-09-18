using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsController : MonoBehaviour
{
	public Vector2 extents = new Vector2(17.5f, 8.0f);

	public void SetExtents(Vector3 extents)
	{
		BoxCollider2D collider = GetComponent<BoxCollider2D>();
		collider.size = extents * 2.0f; // size is double of extents
		this.extents = extents;
	}

	public Vector3 GetRandomPoint()
	{
		return new Vector3(
			Random.Range(-extents.x, extents.x),
			Random.Range(-extents.y, extents.y),
			0.0f
		);
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		GameObject go = collider.gameObject;
		Debug.LogFormat("exiting {0}", go);
		go.transform.position = WrapPosition(go.transform.position);
	}

	public Vector3 WrapPosition(Vector3 position)
	{
		Vector3 limit = extents;
		Vector3 newPosition = new Vector3(
			WrapModulo(position.x, -limit.x, limit.x),
			WrapModulo(position.y, -limit.y, limit.y),
			position.z
		);
		return newPosition;
	}

	float WrapModulo(float value, float min, float max)
	{
		if (value > min)
		{
			return min + ((value - min) % (max - min));
		}
		return max - ((min - value) % (max - min));
	}
}
