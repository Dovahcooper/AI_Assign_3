using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int[,] boardMatrix; //values of 0 will be empty spaces, 1 will be X, 2 will be O. This was the easiest way I could think of to make it readable to an AI

    public Tile left, right, top, bot, centre, tLeft, bLeft, tRight, bRight;
    public Tile[,] boardTiles;

    // Start is called before the first frame update
    void Start()
    {
        boardMatrix = new int[3, 3] { { 0, 0, 0 },
            { 0, 0, 0},
            {0, 0, 0 } };

        boardTiles = new Tile[3, 3] { { tLeft, top, tRight },
        { left, centre, right },
        { bLeft, bot, bRight }};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMatrix(int row, int col, int type)
    {
        boardMatrix[row, col] = type;
    }
}
