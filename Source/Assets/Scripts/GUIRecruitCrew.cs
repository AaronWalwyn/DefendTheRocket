using UnityEngine;
using System.Collections;

public class GUIRecruitCrew : MonoBehaviour {

	[SerializeField] CanvasGroup mercenaryPane;
	[SerializeField] CanvasGroup engineerPane;
	[SerializeField] CanvasGroup scientistPane;
	[SerializeField] UnityEngine.UI.Text stats;	

	void Start()
	{
		showRecruitMercenary ();
	}

	public void updateStaffStats()
	{
		stats.text = "Mercenaries: " + Crew.mercenary.noOf + "\n" +
			"Engineers: " + Crew.engineer.noOf + "\n" +
			"Scientists: " + Crew.scientist.noOf + "\n\n" +
			"Crew: " + Crew.crewNum + "\n" +
			"Max. Crew: " + Crew.maxCrew + "\n\n" +
			"Wages: " + Crew.totalWage;
	}

	void hidePanes()
	{
		mercenaryPane.alpha = 0f;
		mercenaryPane.interactable = false;
		mercenaryPane.blocksRaycasts = false;

		engineerPane.alpha = 0f;
		engineerPane.interactable = false;
		engineerPane.blocksRaycasts = false;

		scientistPane.alpha = 0f;
		scientistPane.interactable = false;
		scientistPane.blocksRaycasts = false;
		
		updateStaffStats ();
	}

	public void showRecruitMercenary()
	{
		hidePanes ();

		mercenaryPane.alpha = 1f;
		mercenaryPane.interactable = true;
		mercenaryPane.blocksRaycasts = true;
	}

	public void showRecruitEngineer()
	{
		hidePanes ();
		
		engineerPane.alpha = 1f;
		engineerPane.interactable = true;
		engineerPane.blocksRaycasts = true;
	}

	public void showRecruitScientist()
	{
		hidePanes ();
		
		scientistPane.alpha = 1f;
		scientistPane.interactable = true;
		scientistPane.blocksRaycasts = true;
	}

	public void hireMercenary()
	{
		Crew.mercenary.hire ();
		updateStaffStats ();
	}

	public void fireMercenary()
	{
		Crew.mercenary.fire ();
		updateStaffStats ();
	}

	public void hireEngineer ()
	{
		Crew.engineer.hire ();
		updateStaffStats ();
	}

	public void fireEngineer ()
	{
		Crew.engineer.fire ();
		updateStaffStats ();
	}

	public void hireScientist ()
	{
		Crew.scientist.hire ();
		updateStaffStats ();
	}

	public void fireScientist ()
	{
		Crew.scientist.fire ();
		updateStaffStats ();
	}
}

