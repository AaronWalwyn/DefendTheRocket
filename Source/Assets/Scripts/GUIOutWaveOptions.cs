using UnityEngine;
using System.Collections;

public class GUIOutWaveOptions : MonoBehaviour {

	GameObject GM;
	static int sP;
	public Sprite[] planetTextures = new Sprite[6];

	[SerializeField] UnityEngine.CanvasGroup waveOptions;
	[SerializeField] UnityEngine.CanvasGroup research;
	[SerializeField] UnityEngine.CanvasGroup chooseWeapon;
	[SerializeField] UnityEngine.CanvasGroup upgradeRocket;
	[SerializeField] UnityEngine.CanvasGroup recruitCrew;

	[SerializeField] UnityEngine.UI.Image selectedPlanetImage;
	[SerializeField] UnityEngine.UI.Text selectedPlanetDescription;

	[SerializeField] UnityEngine.UI.Button nextWaveButton;

	[SerializeField] UnityEngine.UI.Text stats;

	[SerializeField] UnityEngine.UI.Image fuelBar;
	[SerializeField] UnityEngine.UI.Image fuelLine;
	[SerializeField] UnityEngine.UI.Text fuelNumber;

	[SerializeField] UnityEngine.UI.Image newBlueprintNotification;

	[SerializeField] UnityEngine.UI.Text gameTipBox;

	void Start()
	{
		GM = GameObject.Find ("_GM");
		sP = Application.loadedLevel;
		showWaveOptionsMenu ();
	}

	public void switchPlanet(int p)
	{
		sP = p;
		updateSelectedPlanet (sP);

		if(GameManager.enoughFuel(GameManager.planet, sP) <= GameManager.fuel)
		{
			//GameManager.planet = sP;
			nextWaveButton.interactable = true;
		}
		else
		{
			nextWaveButton.interactable = false;
		}
	}

	void updateSelectedPlanet(int p)
	{
		selectedPlanetImage.sprite = planetTextures [p - 1];
		selectedPlanetDescription.text = GameManager.planets [p - 1].name + " - " + GameManager.planets [p - 1].description;
		updateFuelLine ();
	}

	void updateStats()
	{
		stats.text = "Wave: " + GameManager.wave + "\n" +
			"Forcefield: " + Wall.health + " / " + Wall.maxHealth + "\n" +
			"Credits: " + GameManager.credits + "\n" +
			"Staff: " + Crew.crewNum + "\n" +
			"Wages: " + Crew.totalWage;
	}

	public void nextWaveWrapper()
	{
		GameManager.planet = sP;
		StartCoroutine (GM.GetComponent<GameManager> ().nextWave ());
	}

	public void saveGameWrapper()
	{
		IO.saveGame ();
	}

	void hideSubMenus()
	{
		waveOptions.alpha = 0f;
		waveOptions.interactable = false;
		waveOptions.blocksRaycasts = false;

		research.alpha = 0f;
		research.interactable = false;
		research.blocksRaycasts = false;

		chooseWeapon.alpha = 0f;
		chooseWeapon.interactable = false;
		chooseWeapon.blocksRaycasts = false;

		upgradeRocket.alpha = 0f;
		upgradeRocket.interactable = false;
		upgradeRocket.blocksRaycasts = false;

		recruitCrew.alpha = 0f;
		recruitCrew.interactable = false;
		recruitCrew.blocksRaycasts = false;
	}

	public void showWaveOptionsMenu()
	{
		hideSubMenus ();

		updateSelectedPlanet (sP);
		updateStats ();
		updateFuelLine ();

		newBlueprintNotification.enabled = Research.newBlueprints;

		gameTipBox.text = GameTips.getGameTip ();
		
		waveOptions.alpha = 1f;
		waveOptions.interactable = true;
		waveOptions.blocksRaycasts = true;
	}

	public void showResearchMenu()
	{
		hideSubMenus ();

		research.gameObject.GetComponent<GUIResearch> ().updateButtons ();
		research.gameObject.GetComponent<GUIResearch> ().viewWeapons ();

		Research.newBlueprints = false;

		research.alpha = 1f;
		research.interactable = true;
		research.blocksRaycasts = true;
	}

	public void showChooseWeaponMenu()
	{
		hideSubMenus ();

		chooseWeapon.gameObject.GetComponent<GUIChooseWeapon> ().updateButtons ();

		chooseWeapon.alpha = 1f;
		chooseWeapon.interactable = true;
		chooseWeapon.blocksRaycasts = true;
	}

	public void showUpgradeRocketMenu()
	{
		hideSubMenus ();

		upgradeRocket.gameObject.GetComponent<GUIUpgradeRocket> ().updateButtons ();

		upgradeRocket.alpha = 1f;
		upgradeRocket.interactable = true;
		upgradeRocket.blocksRaycasts = true;
	}

	public void showRecruitCrewMenu()
	{
		hideSubMenus ();

		recruitCrew.gameObject.GetComponent<GUIRecruitCrew> ().updateStaffStats ();
		recruitCrew.alpha = 1f;
		recruitCrew.interactable = true;
		recruitCrew.blocksRaycasts = true;
	}

	public void updateFuelLine()
	{
		float f = GameManager.fuel;
		if(f > 1000f) f = 1000f;

		fuelBar.rectTransform.sizeDelta = new Vector2 ((f / 1000f) * 850f, 35);
		fuelLine.rectTransform.anchoredPosition3D = new Vector3 (43f + ((GameManager.enoughFuel(GameManager.planet, sP) / 1000f) * 850f), -17f, 0f);
		
		fuelNumber.text = GameManager.fuel.ToString();
	}
}
