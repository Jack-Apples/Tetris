using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoSpawning : MonoBehaviour
{
    public GameObject[] tetrominos;
	public GameObject[] ghosts;
	public GameObject[] held;
	public GameObject[] next;
	public int[] sevenBag = new int[7];
	public int sevenBagIndex = 0;
	public int holdIndex;
	public int nextIndex = 1;
	public int rnd;
	int tempTetrominosIndex = -1;
	public int tempCurrentTetrominoIndex;

	void Start ()  //this is called when the script is first loaded
    {
		generateSevenBag();
		spawnTetromino();  //calls the procedure to spawn a tetromino
	}

    public void spawnTetromino()  //executes everything that is necessary for when a new tetromino needs to be spawned
    {
		if(tempTetrominosIndex == -1)
        {
			Instantiate(tetrominos[sevenBag[sevenBagIndex]]);
			tempCurrentTetrominoIndex = sevenBag[sevenBagIndex];
			Instantiate(ghosts[sevenBag[sevenBagIndex]]);
			FindObjectOfType<GhostMovement>().updateGhost();
			Instantiate(next[sevenBag[nextIndex]]);
			holdIndex = sevenBag[sevenBagIndex];
			sevenBagIndex++;
		}
        else
        {
			Instantiate(tetrominos[tempTetrominosIndex]);
			tempCurrentTetrominoIndex = tempTetrominosIndex;
			Instantiate(ghosts[tempTetrominosIndex]);
			FindObjectOfType<GhostMovement>().updateGhost();
			Instantiate(next[sevenBag[nextIndex]]);
			holdIndex = tempTetrominosIndex;
			tempTetrominosIndex = -1;
			sevenBagIndex = 0;
		}
		FindObjectOfType<GhostMovement>().updateGhost();
		nextIndex = sevenBagIndex + 1;
		if(sevenBagIndex == 6)
        {
			tempCurrentTetrominoIndex = sevenBag[5];
			tempTetrominosIndex = sevenBag[6];
			nextIndex = 0;
			generateSevenBag();
        }
	}

	
	public void generateSevenBag()
    {
        for (int i = 0; i < 7; i++)
        {
			sevenBag[i] = -1;
        }
        for (int i = 0; i < 7; i++)
        {
			sevenBagHelper(i);
        }
    }

	public void sevenBagHelper(int count)
    {
		bool unique = true;
		rnd = Random.Range(0, 7);
		for (int i = 0; i < 7; i++)
		{
			if (rnd == sevenBag[i])
			{
				unique = false;
			}
		}
		if (unique == false)
		{
			sevenBagHelper(count);
		}
		sevenBag[count] = rnd;
	}
}
