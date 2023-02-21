using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public bool playerInControl = true; //true for white | false for black
    public GameObject selectedPiece;
    

    public GameObject[,] boardLayout = new GameObject[8, 8];
    public int[,] dangerLayout = new int[8, 8];

    public void initialize() {

    }

    public void swapControl() {
        playerInControl = !playerInControl;
        Debug.Log(playerInControl ? "White's Turn Starts" : "Black's Turn Starts");
        selectedPiece = null;

        if (selectedPiece != null) { selectedPiece.GetComponent<PieceController>().HighlightToggle(); }
    }

    public GameObject findPiece(int column, int row) {
        try {
            return boardLayout[column - 1, row - 1];
        } catch {
            return null;
        }
    }

    public bool isDangerSpot(int column, int row) {
        return dangerLayout[column - 1, row - 1] > 0;
    }

    public void raiseSpotDanger(int column, int row) {
        try {
            dangerLayout[column - 1, row - 1]++;
        }
        catch { return; }
    }

    public void lowerSpotDanger(int column, int row) {
        try {
            dangerLayout[column - 1, row - 1]--;
        } catch { return; }
    }

    public void recordPieceMove(GameObject piece, int oldColumn, int oldRow, int newColumn, int newRow) {
        boardLayout[oldColumn - 1, oldRow - 1] = null;

        piece.GetComponent<PieceController>().column = newColumn;
        piece.GetComponent<PieceController>().row = newRow;

        boardLayout[newColumn - 1, newRow - 1] = piece;
    }
}
