using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : PieceController {
    public override bool isValidMove(int column, int row) {
        //check if input is possible move
        if ((Mathf.Abs(this.column - column) == 1 && Mathf.Abs(this.row - row) == 2) || (Mathf.Abs(this.column - column) == 2 && Mathf.Abs(this.row - row) == 1)) {
            //check, if there is a piece, that the piece is an enemy
            if (gameController.findPiece(column, row) != null && gameController.findPiece(column, row).GetComponent<PieceController>().pieceColor == pieceColor) { return false; }
        } else {
            return false;
        }

        //no faults found
        return true;
    }

    public override void markDangerSpots() {
        //top left
        markedDangerSpots.Add(new int[] { column + 1, row + 2 }); // up
        markedDangerSpots.Add(new int[] { column + 2, row + 1 }); // left

        //top right
        markedDangerSpots.Add(new int[] { column - 1, row + 2 }); // up
        markedDangerSpots.Add(new int[] { column - 2, row + 1 }); // right

        //bottom left
        markedDangerSpots.Add(new int[] { column + 1, row - 2 }); // down
        markedDangerSpots.Add(new int[] { column + 2, row - 1 }); // left

        //bottom right
        markedDangerSpots.Add(new int[] { column - 1, row - 2 }); // down
        markedDangerSpots.Add(new int[] { column - 2, row - 1 }); // right


        foreach (int[] cordinates in markedDangerSpots) {
            gameController.raiseSpotDanger(cordinates[0], cordinates[1], pieceColor);
        }
    }


    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }
}

