using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public GameObject ghost;
    public void updateGhost() //called once per frame
    {
        ghost.transform.position = FindObjectOfType<TetrominoMovement>().tetromino.transform.position;
        ghost.transform.rotation = FindObjectOfType<TetrominoMovement>().tetromino.transform.rotation;
        while (FindObjectOfType<Board>().isTetrominoValidGhost(ghost.transform))
        {
            ghost.transform.position -= new Vector3(0, 1, 0);
        }
        ghost.transform.position += new Vector3(0, 1, 0);
    }

    public void destroyGhost()
    {
        Destroy(ghost);
        this.enabled = false;
    }
}
