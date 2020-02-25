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
        int row = -1, col = -1;

        int otherPlayer;
        if(playerType == 2)
        {
            otherPlayer = 1;
        }
        else
        {
            otherPlayer = 2;
        }

        int score = theBoard.CheckWinner(logicArray);
        if (score != playerType && score != 0)
        {
            return new Vector3Int(-1, -1, -1);
        }

        score = -2;

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(logicArray[i, j] == 0)
                {
                    int[,] tempBoard = logicArray;
                    tempBoard[i, j] = playerType;
                    Vector3Int moveScore = minimax(tempBoard, otherPlayer);
                    tempBoard[i, j] = 0;
                    moveScore.x *= -1;
                    if(moveScore.x > score)
                    {
                        score = moveScore.x;
                        row = i;
                        col = j;
                    }
                }
            }
        }

        if(row == -1 || col == -1)
        {
            return new Vector3Int(0, row, col);
        }
        
        return new Vector3Int(score, row, col);
    }
}