using UnityEngine;
using System.Collections;

public class Achievements : MonoBehaviour {

	public static void travelToPatiess()
	{
		if(Social.localUser.authenticated)
		{
			Social.ReportProgress("CgkIvcDh8OIGEAIQDQ", 100.0f, (bool success) => {
				// handle success or failure
			});
		}
	}

	public static void travelToKellia()
	{
		if(Social.localUser.authenticated)
		{
			Social.ReportProgress("CgkIvcDh8OIGEAIQDg", 100.0f, (bool success) => {
				// handle success or failure
			});
		}
	}

	public static void travelToDune()
	{
		if(Social.localUser.authenticated)
		{
			Social.ReportProgress("CgkIvcDh8OIGEAIQDw", 100.0f, (bool success) => {
				// handle success or failure
			});
		}
	}

	public static void travelToMaia()
	{
		if(Social.localUser.authenticated)
		{
			Social.ReportProgress("CgkIvcDh8OIGEAIQEA", 100.0f, (bool success) => {
				// handle success or failure
			});
		}
	}

	public static void travelToRhea()
	{
		if(Social.localUser.authenticated)
		{
			Social.ReportProgress("CgkIvcDh8OIGEAIQFw", 100.0f, (bool success) => {
				// handle success or failure
			});
		}
	}
}
