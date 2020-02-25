using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [SerializeField] UnityEvent onClickEvent;

    Board theBoard;
    public Vector2Int arrayPos;

    MeshRenderer thisRenderer;
    public Material xMat, oMat;

    // Start is called before the first frame update
    void Start()
    {
        Vector2Int tempPos = new Vector2Int((int)(transform.position.x), (int)(transform.position.y)); //truncate the position values by casting to integers
        tempPos /= 2; //divide them by 2, since they will be 2
        arrayPos = new Vector2Int((tempPos.y - 1) * -1, tempPos.x + 1); //using the modified values, put the y in the x and the x in the y, caus rows and columns are backwards to XYZ coordinate space
        theBoard = GetComponentInParent<Board>(); //grabbing the board script, so I can use some of its juicy functions and variables
        thisRenderer = GetComponent<MeshRenderer>(); //grab the renderer so we can change the material on the mesh

        onClickEvent.AddListener(SelectTileX); //set up the player's selection as an event
    }

    // Update is called once per frame
    void Update()
    {
        //called once per frame, not doing anything tho
    }

    private void OnMouseDown()
    {
        if(!theBoard.Winner)
            onClickEvent.Invoke(); //invoke the event if clickedo n and no winners have been declared
    }

    public void SelectTileX()
    {
        if(theBoard.boardMatrix[arrayPos.x, arrayPos.y] == 0) //since the player canreasonable click on an owned space, we're doing logic against that right here, so no cheating
            thisRenderer.material = xMat;
        theBoard.SetMatrix(arrayPos.x, arrayPos.y, 1); //sets the array values to the human player's
    }

    public void SelectTileO()
    {
        thisRenderer.material = oMat; //the AI can only select empty squares, so I'm not worried about checks rn
        theBoard.SetMatrix(arrayPos.x, arrayPos.y, 2); //sets the array values to the AI player's
    }
}
