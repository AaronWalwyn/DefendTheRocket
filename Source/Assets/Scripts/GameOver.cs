using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.Advertisements;


public class GameOver : MonoBehaviour {

	[SerializeField] CanvasGroup gameOverMenu;
	[SerializeField] UnityEngine.UI.Text waveScore;
	[SerializeField] UnityEngine.UI.Button leaderboardButton;

	public static bool currentState;
	static bool displayGameOver;

	void Start()
	{
		gameOverMenu.alpha = 0f;
		gameOverMenu.interactable = false;
		gameOverMenu.blocksRaycasts = false;
	}

	public GameOver(bool t)
	{
		endGame ();
	}


	public void endGame()
	{
		currentState = true;
		LevelManager.spawningFinished = true;
		Crew.clearCrew ();
		IO.deleteSave ();

		waveScore.text = "Wave: " + GameManager.wave + "\nScore: " + GameManager.score;

#if UNITY_ANDROID
		leaderboardButton.interactable = true;
#else
		leaderboardButton.interactable = false;
#endif

		gameOverMenu.alpha = 1f;
		gameOverMenu.interactable = true;
		gameOverMenu.blocksRaycasts = true;

		shareScore ();

		GameManager.allWaveOptions = true;
	}

	public void newGameWrapper()
	{
		StartCoroutine (newGame ());
	}

	IEnumerator newGame() 
	{
		float fadeTime = this.gameObject.GetComponent<Fading> ().beginFade (1);
		yield return new WaitForSeconds (fadeTime);

		if (UnityEngine.Advertisements.Advertisement.IsReady()) 
		{
			UnityEngine.Advertisements.Advertisement.IsReady();
		}

		GameManager.planet = 1;
		GameManager.score = 0;
		GameManager.credits = 0;
		GameManager.wave = 0;
		GameManager.fuel = 0;

		LevelManager.enemiesRemaining = 0;
		
		Wall.maxHealth = 100;
		Wall.health = Wall.maxHealth;
		
		Crew.clearCrew ();
		
		LevelManager.tutorial = true;

		IO.deleteSave ();
		GameOver.currentState = false;
		GameManager.allWaveOptions = false;

		Application.LoadLevel (GameManager.planet);
	}

	public void viewLeaderboards()
	{
		if(Social.localUser.authenticated || loginGPG())
		{
			Social.ShowLeaderboardUI();
		}
	}

	void shareScore()
	{
		if(Social.localUser.authenticated)
		{
			//Report Score
			Social.ReportScore(GameManager.score, "CgkIvcDh8OIGEAIQAg", (bool success) => {
				// handle success or failure
			});
			//Report Wave
			Social.ReportScore(GameManager.wave, "CgkIvcDh8OIGEAIQGA", (bool success) => {
				// handle success or failure
			});
		}

	}
	
	bool loginGPG()
	{
		bool status = true;
		
		Social.localUser.Authenticate((bool success) => {
			if(success) 
			{
				shareScore();
			}
			else
			{
				status = false;
			}
		});
		
		return status;
	}

	public void mainMenuWrapper()
	{
		StartCoroutine (MainMenu ());
	}

	IEnumerator MainMenu()
	{
		float fadeTime = this.gameObject.GetComponent<Fading> ().beginFade (1);
		yield return new WaitForSeconds (fadeTime);

		if (Advertisement.IsReady()) 
		{
			Advertisement.Show();
		}

		Application.LoadLevel (0);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
