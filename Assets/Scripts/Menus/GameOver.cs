using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public TextMeshProUGUI scoreText;
	
	public TextMeshProUGUI highScoreText;

	// Start is called before the first frame update
	void Start()
	{
		scoreText.text = string.Format("Score: {0}", ScoreManager.Score);
		highScoreText.text = string.Format("High Score: {0}", ScoreManager.HighScore);
	}

	public void OnClickRestart()
	{
		SceneManager.LoadScene(Scenes.Game);
	}

	public void OnClickMenu()
	{
		SceneManager.LoadScene(Scenes.MainMenu);
	}

	public void OnClickQuit()
	{
		Application.Quit();
	}
}
