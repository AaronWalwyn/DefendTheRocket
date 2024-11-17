using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Advertisements;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class MainMenu : MonoBehaviour {

	[SerializeField] bool cursorBuild;

	public AudioSource backgroundAudio;

	[SerializeField] UnityEngine.UI.Image aboutPanel;
	[SerializeField] UnityEngine.UI.Text aboutText;

	[SerializeField] UnityEngine.UI.Button gpgButton;
	[SerializeField] UnityEngine.UI.Button leaderboardButton;

	[SerializeField] Texture2D cursorImage;

	GameObject starfield;

	bool fadeAudio;

	//Game options & default values
	public static bool gameAudio = true;
	bool hardMode = false;
	public static bool autoSave = true;

	bool validSave;

	public static int newGameCredits = 0;

	float upY = 670;
	float downY = -450;
	float targetY;
	int levelLoad = 1;

	void Awake()
	{
		Application.targetFrameRate = 30;
		QualitySettings.vSyncCount = 2;
#if UNITY_IOS
		Advertisement.Initialize ("51672");
#elif UNITY_ANDROID
		Advertisement.Initialize ("51677");
#endif
		backgroundAudio = this.gameObject.GetComponent<AudioSource> ();
		starfield = transform.FindChild ("starfield").gameObject;

		IO.dataPath = Application.persistentDataPath + "/saveData.dat";

		PlayGamesPlatform.Activate();

		if(File.Exists(IO.dataPath) && IO.checkSaveVersion())
			validSave = true;

		targetY = downY;
		aboutPanel.rectTransform.anchoredPosition = new Vector3 (0, targetY);

		aboutText.text = aboutText.text + "\n\nVersion: " + GameManager.version;

		if (Social.localUser.authenticated)
			leaderboardButton.interactable = true;
		else
			leaderboardButton.interactable = false;

		fadeAudio = false;


		if (cursorBuild)
		{
			int x = Screen.width / 10;
			Cursor.SetCursor (cursorImage, new Vector2 (cursorImage.width / 2f, cursorImage.height / 2f), CursorMode.Auto); 
		}

#if UNITY_ANDROID
		gpgButton.interactable = true;
#else
		gpgButton.interactable = false;
#endif
	}

	void Update()
	{
		starfield.transform.Rotate (Vector3.forward, Time.deltaTime);

		aboutPanel.rectTransform.anchoredPosition = Vector2.Lerp (aboutPanel.rectTransform.anchoredPosition, new Vector3 (0, targetY), Time.deltaTime * 1.5f);

		if(fadeAudio)
			backgroundAudio.volume += -1 * Time.deltaTime;

		Camera.main.GetComponent<AudioListener> ().enabled = gameAudio;
	}

	void OnGUI()
	{
		Vector2 ratio = new Vector2(Screen.width / 2048f , Screen.height / 1536f );
		Matrix4x4 guiMatrix = Matrix4x4.identity;
		guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
		GUI.matrix = guiMatrix;
		
		GUI.depth = -1000;
		if(Debug.isDebugBuild)
		{
			GUI.Label(new Rect(1500, 0, 120, 120), levelLoad.ToString());
			if (GUI.Button(new Rect(1600, 0, 100, 100), "-")) levelLoad--;
			if (GUI.Button(new Rect(1700, 0, 100, 100), "+")) levelLoad++;
		}
		GUI.matrix = Matrix4x4.identity;
	}

	public void NewGameWrapper()
	{
		StartCoroutine (NewGame (levelLoad));
	}

	public IEnumerator NewGame(int startPlanet)
	{

		fadeAudio = true;
		float fadeTime = this.gameObject.GetComponent<Fading> ().beginFade (1);
		yield return new WaitForSeconds (fadeTime);

		IO.deleteSave ();

		GameOver.currentState = false;
		GameManager.allWaveOptions = false;

		GameManager.planet = startPlanet;
		GameManager.score = 0;
		GameManager.credits = newGameCredits;
		GameManager.wave = 0;
		GameManager.fuel = 0;

		if (hardMode)
			LevelManager.userDifficulty = 0.5f;
		else
			LevelManager.userDifficulty = 1f;

		LevelManager.enemiesRemaining = 0;

		Wall.maxHealth = 100;
		Wall.health = Wall.maxHealth;

		Crew.clearCrew ();

		LevelManager.tutorial = false;

		Rocket.createUpgrades ();
		Weapons.createWeapons ();
		Research.createBlueprints ();
		Research.points = 0;

		Weapons.setFirstWepon (0);

		Application.LoadLevel (GameManager.planet);
	}

	public void continueGameWrapper()
	{
		StartCoroutine (Continue ());
	}

	IEnumerator Continue()
	{
		fadeAudio = true;
		float fadeTime = this.gameObject.GetComponent<Fading> ().beginFade (1);
		yield return new WaitForSeconds (fadeTime);

		IO.loadGame ();
		Application.LoadLevel (GameManager.planet);

	}

	public void viewLeaderboards()
	{
		if(Social.localUser.authenticated || loginGPG())
		{
			Social.ShowLeaderboardUI();
		}
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

	public void loginGPGwrapper()
	{
		loginGPG ();
	}

	bool loginGPG()
	{
		bool status = true;
		
		Social.localUser.Authenticate((bool success) => 
		{
			if(success) 
			{
				leaderboardButton.interactable = true;
				status = true;
			}
			else
			{
				status = false;
			}
		});
		
		return status;
	}

	public void About()
	{
		targetY = upY;
	}

	public void closeAboutPanel()
	{
		targetY = downY;
	}
}
