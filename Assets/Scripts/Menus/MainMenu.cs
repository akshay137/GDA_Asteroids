using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public TextMeshProUGUI HighScoreText;

	private void Start()
	{
		HighScoreText.text = string.Format("High Score: {0}", ScoreManager.HighScore);
	}

	public void OnClickPlay()
	{
		SceneManager.LoadScene(Scenes.Game);
	}

	public void OnClickQuit()
	{
		Application.Quit();
	}
}
