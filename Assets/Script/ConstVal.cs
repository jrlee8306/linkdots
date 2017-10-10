using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstVal 
{
    public const int MAX_BOARD_WIDTH = 6;
    public const int MAX_BOARD_HEIGHT = 8;
        
    public const int BLOCK_SIZE = 60;
    public const int TILE_SIZE = 100;

    public const int MAX_TARGET_LINK_PER_BLOCK = 5;
    public const int START_TARGET_LINK = 5;
    public const int MAX_TARGET_LINK = MAX_BOARD_HEIGHT * MAX_BOARD_WIDTH * MAX_TARGET_LINK_PER_BLOCK;

    public const int START_TIME_SECOND = 45;
    public const int CLEAR_ADD_TIME_SECOND = 2;

    public const int ADD_DEFAULT_SCORE= 10;

    //Messenger Event
    public const string EVENT_CHANGE_GAME_STATE = "EVNET_CHANGE_GAME_STATE";
    public const string EVENT_NEXT_LEVEL = "EVENT_NEXT_LEVEL";
    public const string EVENT_REFRESH_SCORE = "EVENT_REFRESH_SCORE";
    public const string EVENT_REFRESH_HIGHSCORE = "EVENT_REFRESH_HIGHSCORE";
    public const string EVENT_REFRESH_SECOND = "EVENT_REFRESH_SECOND";
	public const string EVENT_REFRSH_GAMECENTER_STATE = "EVENT_REFRSH_GAMECENTER_STATE";

    //PlayerPrfs Key
    public const string PREFS_KEY_HIGHSCORE = "PREFS_KEY_HIGHSCORE";
	public const string ACHIEVEMENT_200_KEY = "Achievement_200";
}
