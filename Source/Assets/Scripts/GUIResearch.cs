using UnityEngine;
using System.Collections;

public class GUIResearch : MonoBehaviour {

	static int sB = -1;

	[SerializeField] UnityEngine.UI.Text[] buttonText = new UnityEngine.UI.Text[20];

	[SerializeField] CanvasGroup weaponPanel;
	[SerializeField] CanvasGroup shipUpgradesPanel;

	[SerializeField] CanvasGroup viewBlueprintPanel;
	[SerializeField] CanvasGroup currentBlueprintPanel;

	[SerializeField] UnityEngine.UI.Text viewBlueprintName;
	[SerializeField] UnityEngine.UI.Text viewBlueprintDescription;
	[SerializeField] RectTransform viewBlueprintProgress;
	[SerializeField] UnityEngine.UI.Image viewBlueprintImage;

	[SerializeField] UnityEngine.UI.Text currentReseachName;
	[SerializeField] UnityEngine.UI.Text currentReseachDescription;
	[SerializeField] RectTransform currentResearchProgress;
	[SerializeField] UnityEngine.UI.Image currentBlueprintImage;

	[SerializeField] Sprite weaponSprite;
	[SerializeField] Sprite shipUpgradeSprite;

	public void updateButtons()
	{
		for(int i=0; i<20; i++)
		{
			if(Research.blueprints[i].dropped)
			{
				buttonText[i].text = Research.blueprints[i].unlock.getName();
			}
			else
			{
				buttonText[i].text = "????";
			}
		}
	}

	public void viewWeapons()
	{
		weaponPanel.alpha = 1f;
		weaponPanel.blocksRaycasts = true;
		weaponPanel.interactable = true;

		shipUpgradesPanel.alpha = 0f;
		shipUpgradesPanel.blocksRaycasts = false;
		shipUpgradesPanel.interactable = false;
	}

	public void viewShipUpgrades()
	{
		weaponPanel.alpha = 0f;
		weaponPanel.blocksRaycasts = false;
		weaponPanel.interactable = false;
		
		shipUpgradesPanel.alpha = 1f;
		shipUpgradesPanel.blocksRaycasts = true;
		shipUpgradesPanel.interactable = true;
	}

	public void viewBlueprint(int b)
	{
		if(Research.blueprints [b].dropped)
		{
			viewBlueprintPanel.alpha = 1f;
			viewBlueprintPanel.blocksRaycasts = true;
			viewBlueprintPanel.interactable = true;

			sB = b;

			viewBlueprintName.text = Research.blueprints [sB].unlock.getName ();
			viewBlueprintDescription.text = Research.blueprints [sB].unlock.getDescription ();

			float r = Research.blueprints [sB].researched / Research.blueprints [sB].researchCost;
			r = Mathf.Clamp01(r);
			viewBlueprintProgress.sizeDelta = new Vector2 (r * 250f, 100f);

			if(Research.blueprints [sB].unlock.GetType() == typeof (Weapons.Gun))
			{
				viewBlueprintImage.sprite = weaponSprite;
			}
			else
			{
				viewBlueprintImage.sprite = shipUpgradeSprite;
			}
		}

	}

	public void switchCurrentResearch()
	{
		Research.currentResearch = sB;
		updateCurrentResearchPanel ();
	}

	public void updateCurrentResearchPanel()
	{
		if (Research.currentResearch != -1) 
		{
			currentBlueprintPanel.alpha = 1f;
			currentBlueprintPanel.interactable = true;
			currentBlueprintPanel.blocksRaycasts = true;

			currentReseachName.text = Research.blueprints [Research.currentResearch].unlock.getName ();
			currentReseachDescription.text = Research.blueprints [Research.currentResearch].unlock.getDescription ();

			float r = Research.blueprints [Research.currentResearch].researched / Research.blueprints [Research.currentResearch].researchCost;
			r = Mathf.Clamp01(r);
			currentResearchProgress.sizeDelta = new Vector2 (r * 465f, 100f);

			if(Research.blueprints [Research.currentResearch].unlock.GetType() == typeof (Weapons.Gun))
			{
				currentBlueprintImage.sprite = weaponSprite;
			}
			else
			{
				currentBlueprintImage.sprite = shipUpgradeSprite;
			}

		}
	}
}
