using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopController : PieceController {
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    //check if inputed square is a valid place for the piece to move
    public override bool isValidMove(int column, int row) {
        //make sure square clicked is not current square
        if (isCurrentSquare(column, row)) { return false; }

        //check in each direction for target square, then check if pieces are in the way (these directions are from white's POV)
        if (this.column - column == this.row - row) {
            if (row > this.row) { //top left
                for (int i = (this.row + 1); i < row; i++) {
                    if (gameController.findPiece((this.column + i - this.row), i) != null) { return false; }
                }
            } else { //down right
                for (int i = (this.row - 1); i > row; i--) {
                    if (gameController.findPiece((this.column - i), i) != null) { return false; }
                }
            }
        } else if (Mathf.Abs(this.column - column) == this.row - row) { //down left
            for (int i = (this.row - 1); i > row; i--) {
                if (gameController.findPiece((this.column + Mathf.Abs(this.row - i)), i) != null) { return false; }
            }

        } else if (this.column - column == Mathf.Abs(this.row - row)) { //top right
            for (int i = (this.row + 1); i < row; i++) {
                if (gameController.findPiece((this.column - i + this.row), i) != null) { return false; }
            }

        } else { //not in a valid direction
            return false;
        }

        //make sure, if there is a target piece, that the target piece is on enemy team
        if (gameController.findPiece(column, row) != null && gameController.findPiece(column, row).GetComponent<PieceController>().pieceColor == pieceColor) { return false; }


        //no faults found, move is valid
        return true; 
    }

    public override void markDangerSpots() {
        //Bishop
        for (int i = 1; i < 8 - column + 1 || i < 8 - row; i++) { //top left
            markedDangerSpots.Add(new int[] { column + i, row + i });
            if (gameController.findPiece(column + i, row + i) != null) { break; }
        }
        for (int i = 1; i < 8 - column + 1 || i < 8 - row; i++) { //top right
            markedDangerSpots.Add(new int[] { column - i, row + i });
            if (gameController.findPiece(column - i, row + i) != null) { break; }
        }
        for (int i = 1; i < 8 - column + 1 || i < row; i++) { //bottom left
            markedDangerSpots.Add(new int[] { column + i, row - i });
            if (gameController.findPiece(column + i, row - i) != null) { break; }
        }
        for (int i = 1; i < 8 - column + 1 || i < row; i++) { //bottom right
            markedDangerSpots.Add(new int[] { column - i, row - i });
            if (gameController.findPiece(column - i, row - i) != null) { break; }
        }

        foreach (int[] cordinates in markedDangerSpots) {
            gameController.raiseSpotDanger(cordinates[0], cordinates[1], pieceColor);
        }
    }
}

