using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		Vector2 touchPosition;
		if(GameManager.allWaveOptions == false)
		{
			if(Input.GetMouseButton(0))
			{
				touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Weapons.fire (touchPosition);
				if(Weapons.selectedWeapon.gunType == Weapons.GunType.singleShot)
					Weapons.selectedWeapon.canFire = false;
				else
					Weapons.selectedWeapon.canFire = true;
			}
			else
			{
				Weapons.selectedWeapon.canFire = true;
			}
		}
	}
}
