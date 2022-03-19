using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding : MonoBehaviour
{
    public int heldIndex = -1;
    public bool isHeld = false;

    public void holdTetromino()
    {
        if (!isHeld)
        {
            if (heldIndex == -1)
            {
                heldIndex = FindObjectOfType<TetrominoSpawning>().holdIndex;
                Instantiate(FindObjectOfType<TetrominoSpawning>().held[heldIndex]);
                Destroy(FindObjectOfType<TetrominoMovement>().tetromino);
                Destroy(FindObjectOfType<GhostMovement>().ghost);
                FindObjectOfType<NextTetromino>().destroyNext();
                FindObjectOfType<TetrominoSpawning>().spawnTetromino();
            }
            else
            {
                int temp = heldIndex;
                FindObjectOfType<Scoring>().tetrominoIndex = heldIndex;
                //print(heldIndex);
                heldIndex = FindObjectOfType<TetrominoSpawning>().holdIndex;
                FindObjectOfType<HeldTetromino>().destroyHeld();
                Instantiate(FindObjectOfType<TetrominoSpawning>().held[heldIndex]);
                Destroy(FindObjectOfType<TetrominoMovement>().tetromino);
                Destroy(FindObjectOfType<GhostMovement>().ghost);
                Instantiate(FindObjectOfType<TetrominoSpawning>().tetrominos[temp]);
                Instantiate(FindObjectOfType<TetrominoSpawning>().ghosts[temp]);
                FindObjectOfType<GhostMovement>().updateGhost();
            }
            isHeld = true;
        }
    }
}
