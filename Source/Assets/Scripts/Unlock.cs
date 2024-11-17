using UnityEngine;
using System.Collections;

public abstract class Unlock {

	string name;
	bool reseached = false;
	public string description;
	public bool purchased = false;
	public int cost;

	public void research()
	{
		this.reseached = true;
	}

	public string getName()
	{
		return name;
	}

	public void setName(string s)
	{
		this.name = s;
	}

	public string getDescription()
	{
		return description;
	}

	public bool getResearched()
	{
		return reseached;
	}

	public void setResearched(bool b)
	{
		reseached = b;
	}

	public virtual void purchase()
	{
		if(cost <= GameManager.credits && !purchased)
		{
			GameManager.credits -= cost;
			this.purchased = true;
		}
	}
}
