using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookController : PieceController {
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    //check if inputed square is a valid place for the piece to move
    public override bool isValidMove(int column, int row) {
        //make sure square clicked is not current square
        if (isCurrentSquare(column, row)) { return false; }

        //check in each direction for target square, then check if pieces are in the way (these directions are from white's POV)
        if (column == this.column) {
            //up
            for (int i = (this.row + 1); i < row; i++) {
                if (gameController.findPiece(column, i) != null) { return false; }
            }
            //down
            for (int i = (this.row - 1); i > row; i--) {
                if (gameController.findPiece(column, i) != null) { return false; }
            }
        } else if (row == this.row) {
            //left
            for (int i = (this.column + 1); i < column; i++) {
                if (gameController.findPiece(i, row) != null) { return false; }
            }
            //right
            for (int i = (this.column - 1); i > column; i--) { 
                if (gameController.findPiece(i, row) != null) { return false; }
            }
        } else {
            //not in a valid direction
            return false;
        }

        //make sure target piece is on enemy team
        if (gameController.findPiece(column, row) != null && gameController.findPiece(column, row).GetComponent<PieceController>().pieceColor == pieceColor) { return false; }


        //no faults found, move is valid
        return true;
    }

    public override void markDangerSpots() {
        //Rook
        for (int i = 1; i < 8 - column + 1; i++) { //left
            markedDangerSpots.Add(new int[] { column + i, row });
            if (gameController.findPiece(column + i, row) != null) { break; }
        }
        for (int i = 1; i < column; i++) { //right
            markedDangerSpots.Add(new int[] { column - i, row });
            if (gameController.findPiece(column - i, row) != null) { break; }
        }
        for (int i = 1; i < 8 - row + 1; i++) { //up
            markedDangerSpots.Add(new int[] { column, row + i });
            if (gameController.findPiece(column, row + i) != null) { break; }
        }
        for (int i = 1; i < row; i++) { //down
            markedDangerSpots.Add(new int[] { column, row - i });
            if (gameController.findPiece(column, row - i) != null) { break; }
        }


        foreach (int[] cordinates in markedDangerSpots) {
            gameController.raiseSpotDanger(cordinates[0], cordinates[1]);
        }
    }
}

