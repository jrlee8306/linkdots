using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GooglePlayServiceManager : MonoBehaviour
{
    public static GooglePlayServiceManager instance;

    void Awake()
    {
        instance = this;


    }

    void Start()
    {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = false;
		PlayGamesPlatform.Activate();
    }

	public void Login()
	{
		Social.localUser.Authenticate(  (bool success) => { 
			if(  success )
			{
				CompleteFirstGame();
				Messenger.Broadcast( ConstVal.EVENT_REFRSH_GAMECENTER_STATE) ;
			}
			else
			{
				Debug.Log("Login Fail"); 
			}
		});
	}

	public bool IsAuthenticated()
	{
		return Social.localUser.authenticated;
	}

	public void ShowAchievementUI()
	{
		Social.ShowAchievementsUI();
	}

	public void CompleteFirstGame()
	{
		if( IsAuthenticated() == false ) 
		{
			return;
		}

		Social.ReportProgress( GPGSIds.achievement, 100.0 , (bool success ) => { 
			if( success == false ) 
			{ 
				Debug.Log("Report Fail"); 
			}
		});
	}

	public void CompleteScore200( int socre )
	{
		if( IsAuthenticated() == false || PlayerPrefs.GetInt( ConstVal.ACHIEVEMENT_200_KEY , 0 ) == 1 ) 
		{
			return;
		}

		double percent = (double ) socre / (double) 200 * 100;

		if( percent > 100.0 ) 
		{
			percent = 100;
		}

		Social.ReportProgress( GPGSIds.achievement__200, percent , (bool success ) => {
			
		 	if( success ) 
			{ 
				PlayerPrefs.SetInt( ConstVal.ACHIEVEMENT_200_KEY , 1 );
			}else
			{
				Debug.Log("Report Fail"); 
			}
		});
	}

}
