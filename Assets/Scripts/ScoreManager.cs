using System.Collections;
using UnityEngine;

public class ScoreManager
{
	static private int score = 0;
	static private int highScore = 10;

	static public int Score { get => score; }
	static public int HighScore { get => highScore; }

	static public void UpdateScore(int score)
	{
		ScoreManager.score = score;
		highScore = Mathf.Max(highScore, score);
	}
}
