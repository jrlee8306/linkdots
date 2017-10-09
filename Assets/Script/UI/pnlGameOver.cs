using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pnlGameOver : MonoBehaviour {

    public UIButton btnClose;

    public UILabel lblScore;
    public UILabel lblHighScore;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {

	}

    private void OnEnable()
    {
        lblScore.text = string.Format("score {0}" , Util.GetCommaNumber( GameData.score) );
        lblHighScore.text = string.Format("highScore {0}" ,Util.GetCommaNumber( GameData.highScore ) );
        EventDelegate.Set(btnClose.onClick ,OnClickClose );
    }

    // Update is called once per frame
    void Update ()
    {
        	
	}

    public void OnClickClose()
    {
        Debug.Log("OnClickClose");
        GameManager.instance.state = GameManager.State.Menu;
        gameObject.SetActive(false);
    }
}
