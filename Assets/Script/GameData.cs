using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int level = 0;

    static int _score = 0;
    public static int score
    {
        get { return _score; }
        set {
            
            _score = value;
            if( _score > highScore )
            {
                highScore = value;
            }

            Messenger.Broadcast( ConstVal.EVENT_REFRESH_SCORE );
        }
    }

    public static int highScore
    {
        get
        {
            return PlayerPrefs.GetInt( ConstVal.PREFS_KEY_HIGHSCORE , 0 );
        }
        set
        {
            PlayerPrefs.SetInt( ConstVal.PREFS_KEY_HIGHSCORE , value );
            Messenger.Broadcast( ConstVal.EVENT_REFRESH_HIGHSCORE );
        }
    }

    static float _second = 0;
    public static float second
    {
        get { return _second; } 
        set
        {    
            _second = value;
            Messenger.Broadcast( ConstVal.EVENT_REFRESH_SECOND );

            if (_second <= 0 )
            {
                GameManager.instance.state = GameManager.State.GameOver;
            }
        }
    }

    public static void StartGame()
    {
        level = 1;
        score = 0;
        second = ConstVal.START_TIME_SECOND;
    }

    public static void AddScore( int blockCount , int currLevel, int combo = 1 )
    {
        int addScore = blockCount * ConstVal.ADD_DEFAULT_SCORE * currLevel * combo;
        score += addScore;
    }

    public static void NextLevel()
    {
        second += ConstVal.CLEAR_ADD_TIME_SECOND;
        level++;
        Messenger.Broadcast( ConstVal.EVENT_NEXT_LEVEL );
    }
}
