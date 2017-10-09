using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Generater  {

    public static Block [][] GenerateLevel()
    {
        int width = ConstVal.MAX_BOARD_WIDTH;
        int height = ConstVal.MAX_BOARD_HEIGHT;
        int linkCount = ConstVal.START_TARGET_LINK + GameData.level;
        return Generate( width, height, linkCount );
    }

    public static Block [][] Generate( int width, int height , int linkCount )
    {
        int startPosX = -1 * ( width / 2 * ConstVal.TILE_SIZE ) + ConstVal.TILE_SIZE / 2;
        int startPosY = height /2 * ConstVal.TILE_SIZE - ConstVal.TILE_SIZE / 2;

        Transform transBoard = GameManager.instance.board.transform;
        var blocks = new Block[height][];

        for (int y = 0; y < height; y++)
        {
            blocks[y] = new Block[width];

            for (int x = 0; x < width; x++)
            {
                var blockObj = ResourceManager.instance.LoadBlockObject( transBoard );
                blocks[y][x] = blockObj.GetComponent<Block>();
                blocks[y][x].InitBlock(x, y, 0);

                int posX = startPosX + ConstVal.TILE_SIZE * x;
                int posY = startPosY - ConstVal.TILE_SIZE * y;
                blocks[y][x].transform.localPosition = new Vector3(posX, posY, 0);
            }
        }

        int selectX = Random.Range(0, width);
        int selectY = Random.Range(0, height);

        blocks[selectY][selectX].targetCount++;
        blocks[selectY][selectX].useBlock = true;
        linkCount--;

        List<Block> tempBlocks =  new List<Block>();

        while ( linkCount > 0)
        {
            tempBlocks.Clear();

            for( int i = 0; i < 8;i++)
            {
                int nextX = selectX;
                int nextY = selectY;

                if( i == 0 )
                {
                    //topLeft
                    nextX = selectX -1;
                    nextY = selectY -1;

                }else if( i== 1 )
                {
                    //top 
                    nextX = selectX;
                    nextY = selectY -1;
                }else if( i== 2 )
                {
                    //top right
                    nextX = selectX +1;
                    nextY = selectY -1;
                }else if( i== 3 )
                {
                    //left
                    nextX = selectX -1;
                    nextY = selectY;
                }else if( i== 4 )
                {
                    //right
                    nextX = selectX +1;
                    nextY = selectY;
                }else if( i== 5 )
                {
                    //bottomLeft
                    nextX = selectX -1;
                    nextY = selectY +1;
                }else if( i== 6 )
                {
                    //bottom
                    nextX = selectX ;
                    nextY = selectY +1;

                }else if( i== 7 )
                {
                    //bottomRight
                    nextX = selectX + 1;
                    nextY = selectY + 1;
                }

                if( Util.IsAvailableCoord( nextX, nextY) ) // && blocks[nextY][nextX].targetCount <= ConstVal.MAX_TARGET_LINK_PER_BLOCK )
                {
                    tempBlocks.Add( blocks[nextY][nextX]);
                }
            }   
           
            int randomSelect = Random.Range(0, tempBlocks.Count);

            Block selectBlock = tempBlocks[randomSelect];
            selectX = selectBlock.x;
            selectY = selectBlock.y;

            selectBlock.useBlock = true;
            selectBlock.targetCount++;
                
            linkCount--;
        }

        return blocks;
    }
}
