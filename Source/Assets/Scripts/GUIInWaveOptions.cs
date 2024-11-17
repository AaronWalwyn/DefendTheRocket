using UnityEngine;
using System.Collections;

public class GUIInWaveOptions : MonoBehaviour {

	[SerializeField] UnityEngine.UI.Text waveStat;
	[SerializeField] UnityEngine.UI.Text forcefieldStat;
	[SerializeField] UnityEngine.UI.Text ammoStat;
	[SerializeField] UnityEngine.UI.Text creditStat;

	[SerializeField] UnityEngine.UI.Button devMenuButton;

	void Start()
	{
		if(Debug.isDebugBuild)
		{
			devMenuButton.interactable = true;
		}
	}

	// Update is called once per frame
	void Update () {
		waveStat.text = "Wave: " + GameManager.wave;
		forcefieldStat.text = "Forcefeild: " + Wall.health + " / " + Wall.maxHealth;
		ammoStat.text = "Ammo: " + Weapons.selectedWeapon.ammo + " / " + Weapons.selectedWeapon.clipSize;
		creditStat.text = "Credit: " + GameManager.credits;
	}
}
