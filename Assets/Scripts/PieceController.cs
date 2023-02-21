using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceController : MonoBehaviour {
    protected GameObject board; //board square size 3.12 (half size: 1.56)
    protected GameController gameController;
    
    public int column;
    public int row;
    private float _y = 0.3f; // height to appear on top of board, different for each piece
    private const float boardSquareSize = 3.12f;

    public char pieceColor;

    private Material defaultMaterial;

    protected List<int[]> markedDangerSpots;

    // Start is called before the first frame update
    protected virtual void Start() {
        //variable setup
        if (board == null) {
            board = GameObject.Find("Board");
            gameController = board.GetComponent<GameController>();
        }

        _y = gameObject.GetComponent<MeshFilter>().mesh.bounds.size.y; //get height of mesh
        _y += board.GetComponent<MeshFilter>().mesh.bounds.size.y; //add height of board | TODO - better method of getting board height

        string meshName = gameObject.GetComponent<MeshFilter>().mesh.name;
        pieceColor = meshName[meshName.IndexOf('.') + 1];

        defaultMaterial = gameObject.GetComponent<Renderer>().material;

        gameController.boardLayout[column - 1, row - 1] = gameObject;

        markedDangerSpots = new List<int[]>();

        //scene setup
        moveToPosition(setup: true);
    }

    public void HighlightToggle() {
        if (gameObject.GetComponent<Renderer>().material == defaultMaterial) {
            gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/HighlightLight");
        } else {
            gameObject.GetComponent<Renderer>().material = defaultMaterial;
        }
    }

    public virtual void moveToPosition(bool setup = false) {
        

        transform.position = new Vector3(
            board.transform.position.x + (boardSquareSize * 4) - (boardSquareSize * column),
            _y,
            board.transform.position.y - (boardSquareSize * 5) + (boardSquareSize * row)
        );

        if (!setup) {
            unmarkDangerSpots();
            markDangerSpots();

            Debug.Log("----------------");
            for (int x = 7; x >= 0; x--) {
                string temp = "";
                for (int y = 7; y >= 0; y--) {
                    temp += gameController.dangerLayout[y, x] + " ";
                }
                Debug.Log(temp);
            }

            Debug.Log(gameObject);
            
        }
    }


    //check if inputed square is a valid place for the piece to move
    public abstract bool isValidMove(int column, int row);

    //marks all spaces the piece has influence over using gameController.raiseSpotDanger(); and adds them to the markedDangerSpots array
    public abstract void markDangerSpots();

    //unmarks all spaces the piece has influence over using gameController.lowerSpotDanger();
    public void unmarkDangerSpots() {
        foreach (int[] cordinates in markedDangerSpots) {
            gameController.lowerSpotDanger(cordinates[0], cordinates[1]);
        }

        markedDangerSpots.Clear();

    }

    
    
    

    //tests if the given cordinates lead to the square the piece is currently on
    protected bool isCurrentSquare(int column, int row) {
        return (column == this.column && row == this.row);
    }
}