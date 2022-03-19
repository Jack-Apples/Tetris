using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTetromino : MonoBehaviour
{
    public GameObject nextTetromino;

    public void destroyNext()
    {
        Destroy(nextTetromino);
        this.enabled = false;
    }
}
