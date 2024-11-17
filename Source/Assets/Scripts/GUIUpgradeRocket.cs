using UnityEngine;
using System.Collections;

public class GUIUpgradeRocket : MonoBehaviour {

	[SerializeField] UnityEngine.UI.Text[] buttonTexts = new UnityEngine.UI.Text[10];
	[SerializeField] Sprite[] upgradeImages = new Sprite[10];

	[SerializeField] CanvasGroup selectedUpgradeGroup;

	[SerializeField] UnityEngine.UI.Image image;
	[SerializeField] UnityEngine.UI.Text title;
	[SerializeField] UnityEngine.UI.Text description;
	[SerializeField] UnityEngine.UI.Text cost;

	static int sU;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateButtons()
	{
		for(int i=0; i < 10; i++)
		{
			if(Rocket.shipUpgrades[i].getResearched())
			{
				buttonTexts[i].text = Rocket.shipUpgrades[i].getName();
			}
			else
			{
				buttonTexts[i].text = "????";
			}
		}
	}

	public void selectUpgrade(int i)
	{
		if(Rocket.shipUpgrades[i].getResearched())
		{
			sU = i;

			selectedUpgradeGroup.alpha = 1f;
			selectedUpgradeGroup.interactable = true;
			selectedUpgradeGroup.blocksRaycasts = true;

			image.sprite = upgradeImages[sU];
			title.text = Rocket.shipUpgrades[sU].getName();
			description.text = Rocket.shipUpgrades[sU].getDescription();
			cost.text = "Cost: " +  Rocket.shipUpgrades[sU].cost;
		}
	}

	public void buyUpgrade()
	{
		Rocket.shipUpgrades [sU].purchase ();
	}
}
