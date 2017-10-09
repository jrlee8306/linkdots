using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Coord
{

	public int x;
	public int y;
		
	public Coord(int _x, int _y) {
		x = _x;
		y = _y;
	}
}

public class Block : MonoBehaviour {

    public UISprite sprBody;

    public UISprite sprTopLeft;
    public UISprite sprTop;
    public UISprite sprTopRight;

    public UISprite sprLeft;
    public UISprite sprRight;

    public UISprite sprBottomLeft;
    public UISprite sprBottom;
    public UISprite sprBottomRight;

    public UILabel lblCount;

    bool isOvered = false;

    int _targetCount;
    public int targetCount
    {
        get {
            return _targetCount;
        }

        set
        {
            _targetCount = value;

            if( _targetCount > 0 )
            {
                useBlock =true;
                SetCount();
                SetColor( Color.blue );

            }else
            {
                SetEmptyBlock();
            }
        }
    }

    int _selectCount;
    public int selectCount
    {
        get
        {
            return _selectCount;
        }

        set
        {
            _selectCount = value;
            SetCount();
        }
    }

    public int x
    {
        get {return coord.x; }
    }   

    public int y
    {
        get { return coord.y; }
    }

    public Coord coord;
    public bool useBlock;


    public void InitBlock( int x, int y ,int targetCount )
    {
        coord = new global::Coord( x, y );

        this.targetCount = targetCount;
        selectCount = 0;
        isOvered = false;
        LineClear();
    }

    public void SetEmptyBlock()
    {
        useBlock = false;
        sprBody.enabled =false;
        lblCount.gameObject.SetActive(false);
        LineClear();
    }

    void SetColor( Color color )
    {
        if (useBlock)
        {
            sprBody.enabled = true;
            sprBody.color = color;
        }
    }

    void SetCount()
    {
        if( useBlock )
        {
            lblCount.gameObject.SetActive(true);
            int count = _targetCount - _selectCount;
            lblCount.text = count.ToString();
        }
    }

    void LineClear()
    {
         sprTopLeft.gameObject.SetActive(false);
         sprTop.gameObject.SetActive(false);
         sprTopRight.gameObject.SetActive(false);
        
         sprLeft.gameObject.SetActive(false);
         sprRight.gameObject.SetActive(false);
        
         sprBottomLeft.gameObject.SetActive(false);
         sprBottom.gameObject.SetActive(false);
         sprBottomRight.gameObject.SetActive(false);
    }

    public bool isCorrectTargetCount()
    {
        if( selectCount == targetCount )
        {
            return true;

        }else
        {
            return false;
        }
    }

    public void ResetSelectCount()
    {
        isOvered = false;
        LineClear();
        selectCount = 0;    
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDragOver()
    {
        if( useBlock  )
        {
            if( GameManager.instance.onInput )
            {
                if( isOvered == false && GameManager.instance.IsNeighboarLastBlock( x , y ) )
                {
                    if( targetCount > selectCount )
                    {
                        GameManager.instance.AddLink(this);
                        selectCount++;
                        isOvered = true;
                    }
                }   

            }else
            {
                isOvered = false;
            }
        }
    }

    public void OnDragOut()
    {
        isOvered = false;
    }
}
