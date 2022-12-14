using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidsGameManager : MonoBehaviour
{
	public static AsteroidsGameManager instance;

	public Camera cam;

	public GameObject obstaclePrefab;

	public BoundsController bounds;

	public ShipController ship;

	public HUDManager hud;

	public float SpawnSafeDistance = 5.0f;

	private int score = 0;
	private int level = 1;

	private int remainingObstacles = 0;

	private void Awake()
	{
		//AsteroidsGameManager[] gameManagers = GameObject.FindObjectsOfType<AsteroidsGameManager>();
		//if (gameManagers.Length > 1)
		//{
		//	DestroyImmediate(this.gameObject);
		//	return;
		//}

		//DontDestroyOnLoad(this.gameObject);
		instance = this;
	}

	public Vector3 ScreenToWorld(Vector3 screenPoint)
	{
		return cam.ScreenToWorldPoint(screenPoint);
	}

	void RecalculateBounds()
	{
		Vector3 corner = ScreenToWorld(new Vector3(Screen.width, Screen.height, 0));
		corner.z = 0;
		bounds.SetExtents(corner);
	}

	void Start()
	{
		StartGame();
	}

	public void StartGame()
	{
		level = 0;
		score = 0;
		RecalculateBounds();
		NextLevel();
	}

	void NextLevel()
	{
		++level;
		SpawnObstacles(level);
		hud.SetLevel(level);
		hud.SetScore(score);
	}

	public void GameOver()
	{
		Debug.LogFormat("Game Over: {0}", score);

		ScoreManager.UpdateScore(score);
		SceneManager.LoadScene(Scenes.GameOver);
	}

	void SpawnObstacles(int count)
	{
		for (int i = 0; i < count; ++i)
		{
			Vector3 position = bounds.GetRandomPoint();

			if (Vector3.Distance(position, ship.transform.position) < SpawnSafeDistance)
			{
				Vector3 shiftDirection = new Vector3(
					Random.Range(0.1f, 1) * Random.Range(0.0f, 1.0f) < 0.5f ? -1 : 1,
					Random.Range(0.1f, 1) * Random.Range(0.0f, 1.0f) < 0.5f ? -1 : 1,
					0
				);
				shiftDirection.Normalize();
				position = ship.transform.position + shiftDirection * SpawnSafeDistance;
				position = bounds.WrapPosition(position); // shift could go out of bounds
				Debug.LogFormat("new position: {0}", position);
			}

			GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
			if (null == obstacle)
			{
				continue;
			}
			ObstacleController oc = obstacle.GetComponent<ObstacleController>();
			oc.SetDirectionAngle(Random.Range(0, 360));

			++remainingObstacles;
		}
	}

	public void OnObstacleDestroyed(GameObject obstacle)
	{
		++score;
		--remainingObstacles;

		hud.SetScore(score);

		if (remainingObstacles <= 0)
		{
			NextLevel();
		}
	}
}
