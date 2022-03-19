using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int tetrominoIndex;
    int totalLinesCleared = 0;
    public int score = 0;
    public int combo = 0;
    int level = 1;

    public void calculateClearType(int lineCount)
    {
        switch (lineCount)
        {
            case 1:
                if (isTSpin())
                {
                    score += 800 * level;
                    addCombo();
                    totalLinesCleared += 1;
                    print("TSPIN SINGLE");
                }
                else
                {
                    score += 100 * level;
                    addCombo();
                    totalLinesCleared += 1;
                }
                break;
            case 2:
                if (isTSpin())
                {
                    score += 1200 * level;
                    addCombo();
                    totalLinesCleared += 2;
                    print("TSPIN DOUBLEROONI");
                }
                else
                {
                    score += 300 * level;
                    addCombo();
                    totalLinesCleared += 2;
                }
                break;
            case 3:
                score += 500 * level;
                addCombo();
                totalLinesCleared += 3;
                break;
            case 4:
                score += 800 * level;
                addCombo();
                totalLinesCleared += 4;
                break;
        }
    }

    public bool isTSpin()
    {
        if(tetrominoIndex == 5)
        {
            Debug.Log("Tspin");
            Debug.Break();
            //print("t piece");
            GameObject tetrominoCopy = FindObjectOfType<TetrominoMovement>().tetromino;
            Destroy(tetrominoCopy.GetComponent<TetrominoMovement>());
            //Instantiate(tetrominoCopy);
            tetrominoCopy.transform.position -= new Vector3(1, 0, 0);
            if (FindObjectOfType<Board>().isTetrominoValid(tetrominoCopy.transform))
            {
                print(tetrominoCopy.transform.position);
                print("1st check invalid");
                
                Destroy(tetrominoCopy);
                return false;
            }
            tetrominoCopy.transform.Translate(2, 0, 0);
            if (FindObjectOfType<Board>().isTetrominoValid(tetrominoCopy.transform))
            {
                print(tetrominoCopy.transform.position);
                print("2nd check invalid");
               
                Destroy(tetrominoCopy);
                return false;
            }
            tetrominoCopy.transform.position -= new Vector3(1, 0, 0);
            tetrominoCopy.transform.position += Vector3.up;
            if (FindObjectOfType<Board>().isTetrominoValid(tetrominoCopy.transform))
            {
                print(tetrominoCopy.transform.position);
                print("3rd check invalid");
                
                Destroy(tetrominoCopy);
                return false;
            }
            print("success!");
            
            Destroy(tetrominoCopy);
            return true;
        }
        else
        {
            print(tetrominoIndex);
            print("not t piece");
            return false;
        }
    }

    void addCombo()
    {
        combo++;
        score += 50 * combo * level;
    }
    
}
