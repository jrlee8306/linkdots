using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pnlMain : MonoBehaviour
{
    public UIButton btnStart;

    public UILabel lblScore;
    public UILabel lblHighScore;

    public UISlider sliderSeconds;  
    public UILabel lblSeconds;
    public UILabel lblGameState;

	public UIButton btnGameCenter;
	public UILabel lblGameCenterText;

    private void Awake()
    {
        EventDelegate.Set(btnStart.onClick , OnClickStart);

        Messenger.AddListener( ConstVal.EVENT_REFRESH_SECOND , SetSecond );
        Messenger.AddListener( ConstVal.EVENT_REFRESH_SCORE , SetScore );
        Messenger.AddListener( ConstVal.EVENT_REFRESH_HIGHSCORE , SetHighScore );
        Messenger.AddListener( ConstVal.EVENT_CHANGE_GAME_STATE , SetGameState);
		Messenger.AddListener( ConstVal.EVENT_REFRSH_GAMECENTER_STATE ,SetGameCenterState );

    }

    // Use this for initialization
    void Start ()
    {
		SetDatas();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void SetDatas()
    {
        SetScore();
        SetHighScore();
        SetSecond();
        SetGameState();
		SetGameCenterState();
    }

    public void SetScore()
    {
        lblScore.text = Util.GetCommaNumber( GameData.score ) ;
    }

    public void SetHighScore()
    {
        lblHighScore.text = Util.GetCommaNumber( GameData.highScore);
    }

    public void SetSecond()
    {
        float value = Mathf.Clamp( (float) GameData.second / (float) ConstVal.START_TIME_SECOND , 0 , 1 );
        sliderSeconds.value = value;
        lblSeconds.text = Mathf.Max( 0 , Mathf.FloorToInt( GameData.second) ).ToString();
    }

    public void SetGameState()
    {
        lblGameState.text = GameManager.instance.state.ToString();

        switch( GameManager.instance.state )
        {
            case GameManager.State.Menu:

                btnStart.gameObject.SetActive(true);
				btnGameCenter.gameObject.SetActive(true);
                sliderSeconds.gameObject.SetActive(false);

                break;

            case GameManager.State.GamePlay:

                btnStart.gameObject.SetActive(false);
				btnGameCenter.gameObject.SetActive(false);
                sliderSeconds.gameObject.SetActive(true);

                break;

            case GameManager.State.GameOver:

                btnStart.gameObject.SetActive(false);
				btnGameCenter.gameObject.SetActive(false);
                sliderSeconds.gameObject.SetActive(false);

                break;
        }
    }

	public void SetGameCenterState()
	{
		if( GooglePlayServiceManager.instance.IsAuthenticated() == false  ) 
		{
			lblGameCenterText.text = "Login";

		}else
		{
			lblGameCenterText.text = "ACHIEVEMENTS";
		}
	}

    public void OnClickStart()
    {
        GameManager.instance.state = GameManager.State.GamePlay;
    }

	public void OnClickGameCenter()
	{
		if( GooglePlayServiceManager.instance.IsAuthenticated() == false ) 
		{
			GooglePlayServiceManager.instance.Login();
		}else
		{
			GooglePlayServiceManager.instance.ShowAchievementUI();
		}	
	}

}
