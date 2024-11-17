using UnityEngine;
using System.Collections;

public class GameTips : MonoBehaviour {

	static string[] tips = { 
		//"This is a test of how long the tip excerpt can be, and how many lines it can occupy, hopefully it can\n occupy two lines in the game. THis is now on the second line of the tip box"
		"Kellia is inhabited by exiled AIs from Rhea when they lost their war against them.",
		"Rhea created the AIs of Kellia, but exiled them when they revolted.",
		"Finding the enemies to hard? Try hring some mercenaries to help you out.",
		"Is your forcefield taking hits? Hire some engineers to keep your forcefield at 100%",
		"Enemies may drop blueprints that you can use to increase your firepower.",
		"Ship upgrade blueprints can be found by killing enemies.",
		"Scientists can help you speed up the process of researching new weapons & upgrades.",
		"Are your wages getting to high? You can fire some of your staff to reduce them and hire more when you have more credits.",
		"Some planets are tougher than others, make sure you're prepared.",
		"A towel is about the most massively useful thing an interstellar hitchhiker can have.",
		"Before your appearance, Astaria had never met extra-terrestrial life and had assumed that they were the only life in the vast universe.",
		"Before it was inhabited, Maia was a bountiful land of vast forests and plantlife.",
		"Missiles and grenades can be shot down mid-flight if your aim is good enough!",
		"Different planets have different technologies. Explore more planets to unlock new weapons and ship upgrades.",
		"Sign in to Google Play Services to post your scores to the global leaderboards and earn achievments.",
		"Killing enemies will increase the amount of fuel you have.",
		"Upgrading your ships weapons allows your hired mercenaries to fire at a fater rate.",
		"Upgrading your crew quaters allows you to hire more staff to help you fight and survive",
		"Surviving each wave will give you a research bonus",
		"Follow @Aaron_Walwyn on twitter for the latest development news.",
		"Like us on Facebook, just search 'Defend The Rocket'.",
		"Want to help test the latest version? Sign-up on our website.",
		"Enjoyed the game, consider leaving a review/rating.",
		"Engineers will brave the battlefield to fix your forcefield during each attack.",
		"Hire mercenaries to utilise your ships onboard weapons.",
		"01110100 01101000 01100101 00100000 01110011 01110100 01101111 01110010 01101101",
		"The refinery upgrade will allow you to create fuel faster, allowing you to travel more frequently.",
		"The laboratory will increase the efficiency of your scientists, allowing you to research blueprints faster.",
		"Trapped in a hostile system you must defend your rocket and crew against hoards of aliens.",
		"Through a wormhole you fell into this accursed star system.",
		"You are the first sign of extra terrestrial life the people of Astaria have seen.",
		"The people of Rhea are now more man then machine, synthetically enhanced human beings, they were built for war.",
		"Maia was once a beautiful planet, now it is home to outlaws fleeing from Rhea and resembles an industrial swampland."
	};

	public static string getGameTip()
	{
		int value = Random.Range (0, tips.Length);
		return tips[value];
	}
}
