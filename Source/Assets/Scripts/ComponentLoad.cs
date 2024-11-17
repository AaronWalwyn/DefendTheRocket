using UnityEngine;
using System.Collections;

public class ComponentLoad : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Rocket.createUpgrades ();
		Weapons.createWeapons ();
		Research.createBlueprints ();

		Weapons.weaponNum = 0;
	}

}
