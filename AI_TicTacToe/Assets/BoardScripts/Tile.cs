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
        Vector2Int tempPos = new Vector2Int((int)(transform.position.x), (int)(transform.position.y));
        tempPos /= 2;
        theBoard = GetComponentInParent<Board>();
        arrayPos = new Vector2Int((tempPos.y - 1) * -1, tempPos.x + 1);
        thisRenderer = GetComponent<MeshRenderer>();

        onClickEvent.AddListener(SelectTileX);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        onClickEvent.Invoke();
    }

    public void SelectTileX()
    {
        thisRenderer.material = xMat;
        theBoard.SetMatrix(arrayPos.x, arrayPos.y, 1);
    }

    public void SelectTileO()
    {
        thisRenderer.material = oMat;
        theBoard.SetMatrix(arrayPos.x, arrayPos.y, 2);
    }
}
