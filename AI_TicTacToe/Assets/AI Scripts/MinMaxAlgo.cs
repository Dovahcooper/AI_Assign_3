using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxAlgo : MonoBehaviour
{
    Board theBoard;

    // Start is called before the first frame update
    void Start()
    {
        theBoard = GameObject.Find("GameBoard").GetComponent<Board>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3Int minimax(int[,] logicArray, int playerType)
    {
        int row = -1, col = -1; //we start our row/columns at a value outside the array's bounds, so we can see whether it changes or not

        int otherPlayer; //this part is a lot easier if you use 1 and -1, since you can just multiply playerType by -1 to get otherPlayer
        if (playerType == 2)
        {
            otherPlayer = 1;
        }
        else
        {
            otherPlayer = 2;
        }

        int score = theBoard.CheckWinner(logicArray); //check if anyone has won the game yet this round
        if (score != playerType && score != 0)
        {
            return new Vector3Int(-1, -1, -1); //basically, if the current playertype doesn't win the predicted stage, we send back up a very low number
        }

        score = -2; //we start the score off low so we don't fuck it all up when we do a max

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (logicArray[i, j] == 0) //if the current selection is a blank square, we start running calculations
                {
                    int[,] tempBoard = logicArray;
                    tempBoard[i, j] = playerType; //we set up a temporary board to modify and set the value of the current selection to our current playerType
                    Vector3Int moveScore = minimax(tempBoard, otherPlayer);
                    tempBoard[i, j] = 0; //reset the temporary board's value, or else C# gets stupid
                    moveScore.x *= -1; //gotta multiply by -1 for the minimize portion, idk y it works tho
                    if (moveScore.x > score)
                    {
                        //if the minimax score is higher than the one we got last loop, we replace it
                        score = moveScore.x;
                        row = i;
                        col = j;
                    }
                }
            }
        }

        if (row == -1 || col == -1)
        {
            //if no new row/column can be found, then the game has entered a draw state, since no winner was found at the beginning of the recursion
            return new Vector3Int(0, row, col);
        }
        //I'm returning the score, and the row/column to access my array outside of the recursion
        return new Vector3Int(score, row, col);
    }
}