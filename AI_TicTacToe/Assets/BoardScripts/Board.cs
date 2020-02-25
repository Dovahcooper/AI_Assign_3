using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int[,] boardMatrix; //values of 0 will be empty spaces, 1 will be X, 2 will be O. This was the easiest way I could think of to make it readable to an AI

    public Tile left, right, top, bot, centre, tLeft, bLeft, tRight, bRight; //grab all of the tiles, so we can put them into a 2D array, cause Unity is dumb about 2D arrays
    public Tile[,] boardTiles; //aforementioned 2D array
    public MinMaxAlgo AIPlayer; //the AI player, cause we needed access to her/him in this script

    private bool winner;
    public bool Winner //a way to check if there's been a winner or not in the other scripts
    {
        get { return winner; }
    }

    // Start is called before the first frame update
    void Start()
    {
        boardMatrix = new int[3, 3] { { 0, 0, 0 },
            { 0, 0, 0},
            {0, 0, 0 } }; //assigning the default values to the logic "matrix"

        boardTiles = new Tile[3, 3] { { tLeft, top, tRight },
        { left, centre, right },
        { bLeft, bot, bRight }}; //assigning the values given in Unity to an Array

        winner = false; //defaulting to no winner, though it would've found that just after the start anyways
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); //to make it easier to leave the program when finished looking at it at any point
        }

        if (!Winner)
        {
            if (CheckWinner(boardMatrix) == 0) //if the check winner returns a 0, no row of 3 has been filled by a single type
            {
                //no winner yet
                winner = false;
            }
            else if (CheckWinner(boardMatrix) == 1) //if it returns one, one or more rows of 3 have been filled by Xs
            {
                //player wins
                winner = true;
                Debug.Log("Player Wins");
            }
            else if (CheckWinner(boardMatrix) == 2) //if it returns two, it means one or more rows have been filled by Os
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
                Vector3Int info = AIPlayer.minimax(boardMatrix, 2); //get the vec3 returned by the minimax operation
                if(info.y >= 0) //if the rows value isn't negative (like the default value)
                    boardTiles[info.y, info.z].SelectTileO(); //we access the array to set the value of the returned tile
            }
        }
        //print(string.Format(" ", boardMatrix));
    }

    public int CheckWinner(int[,] boardArr, int _size = 3)
    {
        int[] tempArr = new int[3]; // "temporary" array to store the values of each row/column to check for stuff
        int[] playerArr = new int[3] { 1, 1, 1 }; //an array t ocompare to, if they're equal, the player has won, huzzah!
        int[] compArr = new int[3] { 2, 2, 2 }; //another array to compare to, if they're equal, the computer has won, boooooo!

        for (int i = 0; i < _size; i++) //check horizontals
        {
            for(int j = 0; j < _size; j++)
            {
                tempArr[j] = boardArr[i, j];
            }
            if(CompareArray(tempArr, playerArr, 3) || CompareArray(tempArr, compArr, 3))
            {
                return tempArr[1]; //return the middle value of the temp Array (which corresponds to the winning value
            }
        }

        for(int j = 0; j < _size; j++) //check verticals for winners
        {
            for(int i = 0; i < _size; i++) //I swapped the order of the variables around so it would still read [i, j] cause that's the "proper" notation I learned in HS
            {
                tempArr[i] = boardArr[i, j];
            }
            if (CompareArray(tempArr, playerArr, 3) || CompareArray(tempArr, compArr, 3))
            {
                return tempArr[1]; //return the middle value of the temp Array (which corresponds to the winning value
            }
        }

        for(int i = 0; i < _size; i++) //check first diagonal
        {
            tempArr[i] = boardArr[i, i];
        }
        if(CompareArray(tempArr, playerArr, 3) || CompareArray(tempArr, compArr, 3))
        {
            return tempArr[1]; //return the middle value of the temp Array (which corresponds to the winning value
        }

        for(int i = 0; i < _size; i++) //check second diagonal
        {
            int row = Mathf.Abs(i - 2); //we're goingfrom the bottom left corner [2, 0] up to [0, 2], so I figured I'd do some Abs math to get the row value
            tempArr[i] = boardArr[row, i];
        }
        if (CompareArray(tempArr, playerArr, 3) || CompareArray(tempArr, compArr, 3))
        {
            return tempArr[1]; //return the middle value of the temp Array (which corresponds to the winning value
        }

        return 0; //returns zero to indicate no one has won yet
    }
    public bool CompareArray(int[] arr1, int[] arr2, int size)
    {
        int count = 0; //create a count for how many times the arrays match elements
        for(int i = 0; i < size; i++)
        {
            if(arr1[i] == arr2[i])
            {
                count++; //if the elements match, increment
            }
        }
        if (count == size) //if the matching elements equals the sie of the array
            return true; //the arrays must be equal
        else
            return false;
    }


}