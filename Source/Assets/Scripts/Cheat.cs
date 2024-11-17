using UnityEngine;
using System.Collections;

public class Cheat {

	public static bool cheated = false;

	private static string[] cheatCodes = {
		"THANKS FOR ALL THE FISH",		//Doesn't do anything
		"UNLOCK GODMODE",				//God mode (unlimited health)
		"I DONT WANT TO GO",			//Research all blueprints
		"MO MONEY MO PROBLEMS"			//9999999 Money
	};

	private static void activateCode(int c)
	{
		cheated = true;
		switch (c)
		{
			case 0: 
				//Do stuff
				break;
			case 1:
				GameManager.godMode = true;
				break;
			case 2:
				GameManager.unlockAll();
				break;
			case 3:
				MainMenu.newGameCredits = 99999999;
				break;
		}
	}

	public static bool checkCode(string codeToCheck)
	{
		for(int i=0; i<cheatCodes.Length; i++)
		{
			if(codeToCheck.Equals(cheatCodes[i]))
			{
				activateCode(i);
				return true;
			}
		}

		return false;
	}
}
