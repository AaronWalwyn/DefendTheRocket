using UnityEngine;
using System.Collections;

public class GUIChooseWeapon : MonoBehaviour {

	static int sW;

	[SerializeField] UnityEngine.UI.Image[] buttonImages = new UnityEngine.UI.Image[11];
	[SerializeField] Sprite[] weaponImages = new Sprite[11];

	[SerializeField] UnityEngine.UI.Image image;
	[SerializeField] UnityEngine.UI.Text title;
	[SerializeField] UnityEngine.UI.Text description;
	[SerializeField] UnityEngine.UI.Text cost;

	[SerializeField] UnityEngine.UI.Image damageBar;
	[SerializeField] UnityEngine.UI.Image fireDelayBar;
	[SerializeField] UnityEngine.UI.Image reloadDelayBar;
	[SerializeField] UnityEngine.UI.Image clipSizeBar;

	[SerializeField] UnityEngine.UI.Button equipButton;

	[SerializeField] CanvasGroup selectedWeaponPanel;

	public void viewWeapon(int s)
	{
		if(Weapons.playerWeapons[s].getResearched())
		{
			sW = s;
			image.sprite = weaponImages[sW];
			title.text = Weapons.playerWeapons[sW].getName();
			description.text = Weapons.playerWeapons[sW].getDescription();
			cost.text = Weapons.playerWeapons[sW].cost.ToString();

			selectedWeaponPanel.alpha = 1f;
			selectedWeaponPanel.blocksRaycasts = true;
			selectedWeaponPanel.interactable = true;

			damageBar.rectTransform.sizeDelta = new Vector2 ((Weapons.playerWeapons[sW].baseDamage / 225f) * 250f, 35);
			fireDelayBar.rectTransform.sizeDelta = new Vector2 ((Weapons.playerWeapons[sW].fireRate / 2f) * 250f, 35);
			reloadDelayBar.rectTransform.sizeDelta = new Vector2 ((Weapons.playerWeapons[sW].reloadTime / 7f) * 250, 35);
			clipSizeBar.rectTransform.sizeDelta = new Vector2 ((Weapons.playerWeapons[sW].clipSize / 100f) * 250f, 35);

			updateEquipButton();
		}
	}

	public void buyWeapon()
	{
		Weapons.playerWeapons [sW].purchase ();
		updateEquipButton ();
	}

	void updateEquipButton()
	{
		equipButton.interactable = Weapons.playerWeapons [sW].purchased;
	}

	public void equipWeapon()
	{
		Weapons.weaponNum = sW;
		Weapons.selectedWeapon = Weapons.playerWeapons [Weapons.weaponNum];
	}

	public void updateButtons()
	{
		for(int i=0; i < 11; i++)
		{
			if(Weapons.playerWeapons[i].getResearched())
			{
				buttonImages[i].sprite = weaponImages[i];
			}
		}
	}
}
