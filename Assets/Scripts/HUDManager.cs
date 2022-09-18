using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
	public TextMeshProUGUI ScoreText;

	public TextMeshProUGUI LevelText;

	public void SetScore(int score)
	{
		ScoreText.text = string.Format("Score: {0}", score);
	}

	public void SetLevel(int level)
	{
		LevelText.text = string.Format("Level: {0}", level);
	}
}
