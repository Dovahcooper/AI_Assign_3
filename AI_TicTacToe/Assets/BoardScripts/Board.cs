using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int[,] boardMatrix; //values of 0 will be empty spaces, 1 will be X, 2 will be O. This was the easiest way I could think of to make it readable to an AI

    public Tile left, right, top, bot, centre, tLeft, bLeft, tRight, bRight;
    public Tile[,] boardTiles;
    public MinMaxAlgo AIPlayer;

    private bool winner;
    public bool Winner
    {
        get { return winner; }
    }

    // Start is called before the first frame update
    void Start()
    {
        boardMatrix = new int[3, 3] { { 0, 0, 0 },
            { 0, 0, 0},
            {0, 0, 0 } };

        boardTiles = new Tile[3, 3] { { tLeft, top, tRight },
        { left, centre, right },
        { bLeft, bot, bRight }};

        winner = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!Winner)
        {
            if (CheckWinner(boardMatrix) == 0)
            {
                //no winner yet
                winner = false;
            }
            else if (CheckWinner(boardMatrix) == 1)
            {
                //player wins
                winner = true;
                Debug.Log("Player Wins");
            }
            else if (CheckWinner(boardMatrix) == 2)
            {
                //AI wins
                winner = true;
                Debug.Log("Computer Wins");
            }
            else
            {
                //some sorta error has occured somehow
            }
        }
    }

    public void SetMatrix(int row, int col, int type)
    {
        if (boardMatrix[row, col] == 0)
        {
            boardMatrix[row, col] = type;
            if (type == 1 && CheckWinner(boardMatrix) == 0)
            {
                Vector3Int info = AIPlayer.minimax(boardMatrix, 2);
                //print("Row " + info.y + " Column " + info.z);
                if(info.y >= 0)
                    boardTiles[info.y, info.z].SelectTileO();
            }
        }
        //print(string.Format(" ", boardMatrix));
    }

    public int CheckWinner(int[,] boardArr, int _size = 3)
    {
        int[] tempArr = new int[3];
        int[] playerArr = new int[3] { 1, 1, 1 };
        int[] compArr = new int[3] { 2, 2, 2 };

        for (int i = 0; i < _size; i++) //check horizontals
        {
            for(int j = 0; j < _size; j++)
            {
                tempArr[j] = boardArr[i, j];
            }
            if(CompareArray(tempArr, playerArr, 3) || CompareArray(tempArr, compArr, 3))
            {
                return tempArr[1]; //return the first value of the temp Array (which corresponds to the winning value
            }
        }

        for(int j = 0; j < _size; j++) //check vertical for winners
        {
            for(int i = 0; i < _size; i++)
            {
                tempArr[i] = boardArr[i, j];
            }
            if (CompareArray(tempArr, playerArr, 3) || CompareArray(tempArr, compArr, 3))
            {
                return tempArr[1]; //return the first value of the temp Array (which corresponds to the winning value
            }
        }

        for(int i = 0; i < _size; i++) //check first diagonal
        {
            tempArr[i] = boardArr[i, i];
        }
        if(CompareArray(tempArr, playerArr, 3) || CompareArray(tempArr, compArr, 3))
        {
            return tempArr[1]; //return the first value of the temp Array (which corresponds to the winning value
        }

        for(int i = 0; i < _size; i++) //check second diagonal
        {
            int row = Mathf.Abs(i - 2);
            tempArr[i] = boardArr[row, i];
        }
        if (CompareArray(tempArr, playerArr, 3) || CompareArray(tempArr, compArr, 3))
        {
            return tempArr[1]; //return the first value of the temp Array (which corresponds to the winning value
        }

        return 0; //returns zero to indicate no one has won yet
    }
    public bool CompareArray(int[] arr1, int[] arr2, int size)
    {
        int count = 0;
        for(int i = 0; i < size; i++)
        {
            if(arr1[i] == arr2[i])
            {
                count++;
            }
        }
        if (count == size)
            return true;
        else
            return false;
    }
}