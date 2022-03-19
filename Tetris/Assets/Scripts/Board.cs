using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform[,] board = new Transform[10, 21];
    public int lineCount = 0;

    public void addTetrominoToBoard(Transform tetromino)
    {
        foreach(Transform mino in tetromino)
        {
            addMinoToBoard(mino);
        }
    }

    public void addMinoToBoard(Transform mino)
    {
        board[(int)mino.position.x, (int)mino.position.y] = mino;
    }

    public bool isTetrominoValid(Transform tetromino)
    {
        foreach (Transform mino in tetromino)
        {
            if (!isMinoValid(mino))
                return false;
        }
        return true;
    }

    public bool isTetrominoValidGhost(Transform tetromino)
    {
        foreach (Transform mino in tetromino)
        {
            if (!isMinoValidGhost(mino))
                return false;
        }
        return true;
    }

    public bool isMinoValid(Transform mino)
    {
        //print(mino.position);
        //print(Math.Floor(mino.position.x));
        //print(Math.Floor(mino.position.y));
        int tempx = Convert.ToInt16(Math.Floor(mino.position.x));
        int tempy = Convert.ToInt16(Math.Floor(mino.position.y));
 
        if (tempx < 0)
            return false;
        else if (tempx > 9)
            return false;
        else if (tempy < 0)
            return false;
        if (board[tempx, tempy] != null)
        {
            //print("overlap");
            return false;
        }
        //print("no overlap");
        return true;

    }

    public bool isMinoValidGhost(Transform mino)
    {
        //print(mino.position);
        //print(Math.Floor(mino.position.x));
        //print(Math.Floor(mino.position.y));
        int tempx = Convert.ToInt16(Math.Floor(mino.position.x));
        int tempy = Convert.ToInt16(Math.Floor(mino.position.y));

        if (tempx < 0)
            return false;
        else if (tempx > 9)
            return false;
        else if (tempy < 0)
            return false;
        if (board[tempx, tempy] != null)
        {
            //print("overlap");
            return false;
        }
        //print("no overlap");
        return true;

    }

    public void lineClearing()
    {
        for (int y = 0; y < 19; y++)
        {
            while (isLineFull(y))
            {
                //FindObjectOfType<Scoring>().isTSpin();
                clearLine(y);
                lineCount++;
            }
        }
        if(lineCount != 0)
            FindObjectOfType<Scoring>().calculateClearType(lineCount);
        lineCount = 0;
    }

    public bool isLineFull(int y)
    {
        for (int i = 0; i < 10; i++)
        {
            if (board[i, y] == null)
                return false;
        }
        return true;
    }

    public void clearLine(int y)
    {
        for (int x = 0; x < 10; x++)
        {
            Destroy(board[x, y].gameObject);
            board[x, y] = null;
        }
        moveLinesDown(y + 1);
    }

    public void moveLinesDown(int y)
    {
        while (moveLineDown(y))
        {
            y++;   
        }
    }

    public bool moveLineDown(int y)
    {
        bool moved = false;
        for (int x = 0; x < 10; x++)
        {
            if (board[x, y] != null)
            {
                board[x, y - 1] = board[x, y];
                board[x, y].position -= new Vector3(0, 1, 0);
                board[x, y] = null;
                moved = true;
            }
        }
        if (moved)
            return true;
        return false;
    }
}
