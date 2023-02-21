using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : PieceController {
    private bool hasMoved = false;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    //check if inputed square is a valid place for the piece to move
    public override bool isValidMove(int column, int row) {
        int colorModifier = (pieceColor == 'W' ? 1 : -1); //decide if look forward or back based on color

        if (row == this.row + (2 * colorModifier) && column == this.column && !hasMoved && gameController.findPiece(this.column, (this.row + (1 * colorModifier))) == null) { return true; } //check 2 spaces ahead when has not moved yet
        if (row == this.row + (1 * colorModifier) && column == this.column && gameController.findPiece(this.column, (this.row + (1 * colorModifier))) == null) { return true; } //check 1 space ahead
        if (row == this.row + (1 * colorModifier) && column == this.column + 1 && gameController.findPiece((this.column + 1), (this.row + (1 * colorModifier))) != null) { return true; } //check 1 space up 1 space left for enemy
        if (row == this.row + (1 * colorModifier) && column == this.column - 1 && gameController.findPiece((this.column - 1), (this.row + (1 * colorModifier))) != null) { return true; } //check 1 space up 1 space right for enemy


        //no valid moves found
        return false;
    }

    public override void moveToPosition(bool setup = false) {
        base.moveToPosition(setup);

        if (!setup && !hasMoved) { hasMoved = true; }
    }

    public override void markDangerSpots() {
        //top right
        markedDangerSpots.Add(new int[] { column - 1, row + (pieceColor == 'W' ? 1 : -1) });
    
        //top left
        markedDangerSpots.Add(new int[] { column + 1, row + (pieceColor == 'W' ? 1 : -1) });


        foreach (int[] cordinates in markedDangerSpots) {
            gameController.raiseSpotDanger(cordinates[0], cordinates[1]);
        }
    }

}
