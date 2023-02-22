using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : PieceController {
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();

        if (pieceColor == 'W') {
            gameController.kingWhite = gameObject;
        } else {
            gameController.kingBlack = gameObject;
        }
    }

    //check if target move is in surrounding squares, then check if that move would put king in check

    public override bool isValidMove(int column, int row) {
        //make sure square clicked is not current square
        if (isCurrentSquare(column, row)) { return false; }

        //make sure target square is in surrounding squares
        if (Mathf.Abs(this.column - column) > 2 || Mathf.Abs(this.row - row) > 2) { return false; }

        //if there is a piece on the target square, make sure it is on enemy team
        if (gameController.findPiece(column, row) != null && gameController.findPiece(column, row).GetComponent<PieceController>().pieceColor == pieceColor) { return false; }

        //make sure target square would not put king in check
        if (inCheck(column, row)) { return false; }


        //no faults found, move is valid
        return true;
    }

    public bool inCheck(int column = 0, int row = 0) {
        if (column == 0 || row == 0) {
            column = this.column;
            row = this.row;
        }

        return gameController.isDangerSpot(column, row, pieceColor);
    }

    public override void markDangerSpots() {
        markedDangerSpots.Add(new int[] { column, row + 1 }); //up
        markedDangerSpots.Add(new int[] { column, row - 1 }); //down

        for (int i = 0; i < 3; i++) {markedDangerSpots.Add(new int[] { column + 1, row - 1 + i }); } //left side
        for (int i = 0; i < 3; i++) { markedDangerSpots.Add(new int[] { column - 1, row - 1 + i }); } //right side

        foreach (int[] cordinates in markedDangerSpots) {
            gameController.raiseSpotDanger(cordinates[0], cordinates[1], pieceColor);
        }
    }
}

