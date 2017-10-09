using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T[] ShuffleArray<T>(T[] array )
    {
	    for (int i =0; i < array.Length -1; i ++)
        {
		    int randomIndex = Random.Range( i , array.Length);
		    T tempItem = array[randomIndex];
		    array[randomIndex] = array[i];
		    array[i] = tempItem;
	    }

	    return array;
	}

    public static string GetCommaNumber (int number )
    {
        string returnValue = "";
		int leftNumber = number;
		while(leftNumber >= 1000)
        {
			int nowNumber = leftNumber % 1000;
			string nowNumberStr = "";

			if(nowNumber<10)
            {
				nowNumberStr = "00" + nowNumber;
			}else if(nowNumber<100 )
            {
				nowNumberStr = "0" + nowNumber;
			}else
            {
				nowNumberStr = "" + nowNumber;
			}

			leftNumber = leftNumber / 1000;

			if(returnValue == "")
            {
				returnValue = nowNumberStr;
			}else{
				returnValue = nowNumberStr + ","+returnValue;
			}
		}
		
		if(returnValue == "")
        {
			returnValue = leftNumber.ToString();
		}else
        {
			returnValue = leftNumber.ToString()+","+returnValue;
		}
		
		return returnValue;
    }

    public static bool IsAvailableCoord( int x, int y , int width = ConstVal.MAX_BOARD_WIDTH, int height = ConstVal.MAX_BOARD_HEIGHT)
    {
        if( x >= 0 && x < width && y >=0 && y < height )
        {
            return true;
        }else
        {
            return false;
        }
    }
}
