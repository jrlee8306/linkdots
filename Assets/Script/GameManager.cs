using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public pnlGameOver pnlGameOver;

    public enum State
    {
        Menu,
        GamePlay,
        GameOver,
    }

    State _state = State.Menu;
    public State state
    {
        get { return _state; }

        set
        {
            if( value != _state)
            {
                _state = value;
                ChangeState();
                Messenger.Broadcast(ConstVal.EVENT_CHANGE_GAME_STATE );
            }
        }
    }

    public bool onInput;

    public GameObject blockPrefab;
    public GameObject board;

    Block [][] _blocks;
    List<Block> linkBlockList = new List<Block>();

    private void Awake()
    {
        instance = this;
    }

    void Start () {

        DestroyBlocks();	
	}

    public void DestroyBlocks()
    {
        _blocks = null;
        board.transform.DestroyChildren();
    }

    public void GenerateMap()
    {
        DestroyBlocks();
        _blocks = Generater.GenerateLevel();
    }

    void ChangeState()
    {
        if( state == State.GamePlay )
        {
            GameData.StartGame();
            GenerateMap();

        }else if( state == State.GameOver )
        {
            onInput = false;
            ResetSelectCount();
            GameOver();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if( state == State.GamePlay )
        {
            UpdateGamePlay();
        }
	}

    void UpdateGamePlay()
    {
        GameData.second -= Time.deltaTime;

        if( Input.GetMouseButtonDown(0))
        {
            onInput = true;
        }

        if( Input.GetMouseButtonUp(0))
        {
            onInput = false;

            if( IsGameWin() )
            {
                NextLevel();

            }else
            { 
                ResetSelectCount();
            }
        }
    }


    public bool IsGameWin()
    {
        if( _blocks == null )
        {
            return false;
        }

        bool isGameWin = true;

        for( int y = 0; y < ConstVal.MAX_BOARD_HEIGHT;y++)
        {
            for ( int x = 0; x < ConstVal.MAX_BOARD_WIDTH;x++)
            {
                if( _blocks[y][x].isCorrectTargetCount() == false )
                {
                    isGameWin = false;
                    break;
                }
            }

            if( isGameWin == false )
            {
                break;
            }
        }

        return isGameWin;
    }

    public void ResetSelectCount()
    {
        if( _blocks == null )
        {
            return;
        }

        for( int y = 0; y < ConstVal.MAX_BOARD_HEIGHT ;y++)
        {
            for (int x = 0; x < ConstVal.MAX_BOARD_WIDTH;x++)
            {
                _blocks[y][x].ResetSelectCount();
            }
        }

        linkBlockList.Clear();
    }

    public bool IsNeighboarLastBlock( int checkX , int checkY)
    {
        if( linkBlockList.Count == 0 )
        {
            return true;
        }else
        {
            Block lastBlock = linkBlockList[linkBlockList.Count -1];

            for( int y = lastBlock.y - 1;  y <= lastBlock.y + 1; y++ )
            {
                for( int x = lastBlock.x - 1; x<= lastBlock.x + 1; x++)
                {
                    if( x == lastBlock.x && y == lastBlock.y )
                    {
                        continue;
                    }

                    if( checkX == x && checkY == y )
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public void AddLink( Block block )
    {
        if( linkBlockList.Count > 0 )
        {
            Block lastBlock = linkBlockList[ linkBlockList.Count - 1];

            if( lastBlock.x -1 == block.x && lastBlock.y - 1 == block.y )
            {
                //top Left
                lastBlock.sprTopLeft.gameObject.SetActive(true);
                block.sprBottomRight.gameObject.SetActive(true);

            }else if( lastBlock.x == block.x && lastBlock.y -1 == block.y )
            {
                //top
                lastBlock.sprTop.gameObject.SetActive(true);
                block.sprBottom.gameObject.SetActive(true);
            }else if( lastBlock.x +1 == block.x && lastBlock.y - 1 == block.y )
            {
                //top Right
                lastBlock.sprTopRight.gameObject.SetActive(true);
                block.sprBottomLeft.gameObject.SetActive(true);
            }else if( lastBlock.x -1 == block.x && lastBlock.y  == block.y )
            {
                //left
                lastBlock.sprLeft.gameObject.SetActive(true);
                block.sprRight.gameObject.SetActive(true);
            }else if( lastBlock.x + 1 == block.x && lastBlock.y  == block.y )
            {
                //right
                lastBlock.sprRight.gameObject.SetActive(true);
                block.sprLeft.gameObject.SetActive(true);
            }else if( lastBlock.x -1 == block.x && lastBlock.y + 1 == block.y )
            {
                //bottomLeft
                lastBlock.sprBottomLeft.gameObject.SetActive(true);
                block.sprTopRight.gameObject.SetActive(true);

            }else if( lastBlock.x  == block.x && lastBlock.y + 1 == block.y )
            {
                //bottom
                lastBlock.sprBottom.gameObject.SetActive(true);
                block.sprTop.gameObject.SetActive(true);

            }else if( lastBlock.x + 1 == block.x && lastBlock.y + 1 == block.y )
            {
                //bottomRight
                lastBlock.sprBottomRight.gameObject.SetActive(true);
                block.sprTopLeft.gameObject.SetActive(true);
            }
        }

        linkBlockList.Add (block);
    }

    void NextLevel()
    {
       StartCoroutine( _nextLevelProcess());
    }

    IEnumerator _nextLevelProcess()
    {
        GameData.AddScore( linkBlockList.Count , GameData.level , 1 ) ;
        ResetSelectCount();
        DestroyBlocks();
        yield return new WaitForSeconds( 0.2f );
        GameData.NextLevel();
        yield return new WaitForSeconds( 0.2f );
        GenerateMap();
    }

    #region Game Over Process

    public void GameOver()
    {
        DestroyBlocks();
        StartCoroutine( _gameOverProcess()) ;
    }

    IEnumerator _gameOverProcess()
    {
        yield return new WaitForSeconds( 0.5f );
        pnlGameOver.gameObject.SetActive(true);
    }

    #endregion
}
