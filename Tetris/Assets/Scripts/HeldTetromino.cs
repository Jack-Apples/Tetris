using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldTetromino : MonoBehaviour
{
    public GameObject heldTetromino;

    public void destroyHeld()
    {
        Destroy(heldTetromino);
        this.enabled = false;
    }
}
