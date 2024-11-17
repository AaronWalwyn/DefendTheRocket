using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public const int MAX_ENEMIES = 150;

	public static bool tutorial = true;

	public string planetName;

	AudioSource introAudio;

	GameObject GM;
	public GameObject wall;
	public GameObject player;
	public static GameObject rocket;

	public int spawnTier = 0;
	public static int enemiesRemaining = 0;
	public static bool spawningFinished;
	public static bool levelComplete;
	bool waveOptions;

	public static float userDifficulty = 1f;
	public float difficultyCoefficent;
	public static float latestSpawnDelay;

	GameObject[] Enemy = new GameObject[5];

	public float waveStart;
	public float waveEnd;
	public float levelDuration = 30f;

	public Color startColor;
	public Color endColor;

	public GameObject stars;
	float alpha;
	public float offset;

	// Use this for initialization
	void Awake () {
		Enemy[0] = (GameObject)Resources.Load (planetName + "/Enemy_1"); // Melee Enemy
		Enemy[1] = (GameObject)Resources.Load (planetName + "/Enemy_2"); // Gunman Rifle Enemy
		Enemy[2] = (GameObject)Resources.Load (planetName + "/Enemy_3"); // Grenadier
		Enemy[3] = (GameObject)Resources.Load (planetName + "/Enemy_4"); // Vehicle
		Enemy[4] = (GameObject)Resources.Load (planetName + "/Enemy_5"); // Vehicle

		GM = GameObject.Find ("_GM");
		rocket = GameObject.Find ("Rocket");

		introAudio = this.gameObject.GetComponent<AudioSource> ();
	}

	void Start()
	{
		newWave();
		GameManager.planet = Application.loadedLevel;
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.backgroundColor = Color.Lerp (Camera.main.backgroundColor, endColor, Time.deltaTime / levelDuration);

		float currentT = Time.time - waveStart - offset;
		float endT = waveEnd - waveStart;
		alpha = currentT / endT;
		
		stars.GetComponent<Renderer>().material.color = new Color (stars.GetComponent<Renderer>().material.color.r, stars.GetComponent<Renderer>().material.color.g, stars.GetComponent<Renderer>().material.color.b, alpha);

		if(Time.time >= waveEnd)
			spawningFinished = true;

		if (checkWaveComplete ())
			endWave ();
	}


	/**
	 * Spawns a single enemies, if there are still enemies to spawn it then calls itself.
	 */
	IEnumerator spawnEnemies()
	{
		Vector2 spawnPoint = new Vector2 (-15f, Random.Range (0f, -7f));

		if(GameManager.wave <= 2 + (Application.loadedLevel * 2)) spawnTier = 0; //4
		else if (GameManager.wave <= 4 + (Application.loadedLevel * 2)) spawnTier = 1; //6
		else if (GameManager.wave <= 7 + (Application.loadedLevel * 2)) spawnTier = 2; //9
		else if (GameManager.wave <= 12 + (Application.loadedLevel * 2)) spawnTier = 3; //14
		else spawnTier = 4;

		int value = Random.Range(0, spawnTier + 1);

		if (enemiesRemaining < MAX_ENEMIES)
		{
			GameObject newEnemy =  Instantiate (Enemy[value], spawnPoint, Quaternion.identity) as GameObject;
			newEnemy.gameObject.GetComponent<Renderer>().sortingOrder =  Mathf.CeilToInt(this.transform.position.y * 1000) * -1;
		}

		float spawnDelay = difficultyCoefficent * userDifficulty * (Random.Range (2f, 4f) / (GameManager.wave + 10));
		latestSpawnDelay = spawnDelay;
		yield return new WaitForSeconds (spawnDelay);

		if(!spawningFinished)
			StartCoroutine( spawnEnemies ());
	}

	/**
	 * Check whether the level has been completed based on whether the enemy spawning
	 * has and all remining enemies have been killed.
	 */
	bool checkWaveComplete()
	{
		if(spawningFinished && enemiesRemaining == 0 && waveOptions == false)
			return true;
		else
			return false;
	}

	void endWave()
	{
		waveOptions = true;
		levelComplete = true;
		GM.GetComponent<GameManager>().waveOptions();
		GameManager.saveGame ();
		//GM.GetComponent<GameManager> ().devMenu ();
	}

	/**
	 * Starts a new wave of enemy spawning, resetting the required values
	 */
	public void newWave()
	{
		//Reset & update vaules
		waveOptions = false;
		GameManager.wave++;
		spawningFinished = false;
		levelComplete = false;
		Camera.main.backgroundColor = startColor;

		GameManager.allWaveOptions = false;

		//GM.GetComponent<GameManager> ().devMenu ();

		waveStart = Time.time;
		levelDuration *= 1.15f;
		waveEnd = waveStart + levelDuration;

		rocket.GetComponent<Rocket> ().renderCannons ();

		StartCoroutine (playIntroAudio ());

		if(tutorial)
			StartCoroutine (this.gameObject.GetComponent<Tutorial>().beginTutorial());

		//Begin spawning
		StartCoroutine( spawnEnemies ());

		GM.GetComponent<Crew> ().startCrew ();
	}

	IEnumerator playIntroAudio()
	{
		introAudio.loop = false;
		introAudio.clip = this.gameObject.GetComponent<LevelAudio> ().introAudio;


		if(introAudio.clip.loadState == AudioDataLoadState.Loaded)
		{
			introAudio.Play ();
			yield return new WaitForSeconds (introAudio.clip.length);
		}

		playBackgroundAudio ();
	}

	void playBackgroundAudio()
	{
		if(introAudio.isPlaying)
			introAudio.Stop ();

		introAudio.loop = true;

		introAudio.clip = this.gameObject.GetComponent<LevelAudio> ().backgroundAudio;

		if (introAudio.clip.loadState == AudioDataLoadState.Loaded)
			introAudio.Play ();

	}
}
